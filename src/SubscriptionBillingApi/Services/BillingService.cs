using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Services
{
    /// <summary>
    /// Orchestrates a billing run for a given period.
    /// It determines which subscriptions are billable, ensures a draft invoice exists,
    /// and adds missing invoice lines based on the subscription plan.
    /// </summary>
    public class BillingService
    {
        private readonly ISubscriptionPlanRepository _planRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly InvoiceService _invoiceService;

        public BillingService(ISubscriptionPlanRepository planRepository, ISubscriptionRepository subscriptionRepository, InvoiceService invoiceService)
        {
            _planRepository = planRepository;
            _subscriptionRepository = subscriptionRepository;
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Runs billing for the given period and returns the affected invoices.
        /// The method is idempotent for a period: it reuses draft invoices and avoids
        /// adding duplicate invoice lines for the same subscription.
        /// </summary>
        public async Task<List<Invoice>> RunBilling(DateOnly periodStart, DateOnly periodEnd)
        {
            // 1) Find all active subscriptions that overlap with the billing period
            var dueSubscriptions = await _subscriptionRepository.GetDueSubscriptionsAsync(periodStart, periodEnd);

            // Collect invoices touched/created during this billing run
            var invoices = new List<Invoice>();

            foreach (var sub in dueSubscriptions)
            {
                // 2) Load the plan to get price/currency/interval information
                var plan = await _planRepository.GetByIdAsync(sub.PlanId);
                if (plan is null) continue;

                // 3) Ensure a draft invoice exists for this customer and billing period
                var invoice = await _invoiceService.GetOrCreateDraftInvoice(
                    sub.CustomerId, periodStart, periodEnd, plan.Currency);

                // 4) Add an invoice line for this subscription only if it doesn't exist yet
                //    (prevents duplicates when billing is executed multiple times)
                if (!invoice.Lines.Any(l => l.SubscriptionId == sub.Id))
                {
                    var description = $"{plan.Name} ({periodStart} - {periodEnd})";

                    // Quantity depends on the billing interval (monthly/yearly)
                    var qty = CalculateQuantity(plan.BillingInterval, periodStart, periodEnd);
                    if (qty == 0) continue;

                    // Create and add the line to the aggregate, then persist it
                    var line = new InvoiceLine(invoice.Id, sub.Id, description, qty, plan.Price);
                    invoice.AddLine(line);
                    await _invoiceService.AddLineAsync(line);
                }

                // 5) Add the invoice to the result list only once
                if (!invoices.Any(i => i.Id == invoice.Id))
                    invoices.Add(invoice);
            }
            return invoices;
        }

        /// <summary>
        /// Calculates how many billing units (months/years) are covered by the given period.
        /// Returns 0 for invalid periods (end before start).
        /// </summary>
        private static int CalculateQuantity(BillingInterval interval, DateOnly start, DateOnly end)
        {
            if (end < start) return 0;

            return interval switch
            {
                BillingInterval.Monthly => CountMonthsInclusive(start, end),
                BillingInterval.Yearly => CountYearsInclusive(start, end),
                _ => 1
            };
        }

        /// <summary>
        /// Counts months inclusively. Example: 2025-01..2025-01 => 1 month.
        /// </summary>
        private static int CountMonthsInclusive(DateOnly start, DateOnly end)
        {
            var months = (end.Year - start.Year) * 12 + (end.Month - start.Month) + 1;
            return Math.Max(0, months);
        }

        /// <summary>
        /// Counts years inclusively. Example: 2025..2025 => 1 year.
        /// </summary>
        private static int CountYearsInclusive(DateOnly start, DateOnly end)
        {
            var years = (end.Year - start.Year) + 1;
            return Math.Max(0, years);
        }
    }
}
