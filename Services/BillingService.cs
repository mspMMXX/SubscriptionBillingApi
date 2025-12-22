using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Services
{
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

        public async Task<List<Invoice>> RunBilling(DateOnly periodStart, DateOnly periodEnd)
        {
            var dueSubscriptions = await _subscriptionRepository.GetDueSubscriptionsAsync(periodStart, periodEnd);

            var invoices = new List<Invoice>();

            foreach (var sub in dueSubscriptions)
            {
                var plan = await _planRepository.GetByIdAsync(sub.PlanId);
                if (plan is null) continue;

                var invoice = await _invoiceService.GetOrCreateDraftInvoice(
                    sub.CustomerId, periodStart, periodEnd, plan.Currency);

                if (!invoice.Lines.Any(l => l.SubscriptionId == sub.Id))
                {
                    var description = $"{plan.Name} ({periodStart} - {periodEnd})";

                    var qty = CalculateQuantity(plan.BillingInterval, periodStart, periodEnd);
                    if (qty == 0) continue;

                    var line = new InvoiceLine(invoice.Id, sub.Id, description, qty, plan.Price);
                    invoice.AddLine(line);

                }

                if (!invoices.Any(i => i.Id == invoice.Id))
                    invoices.Add(invoice);
            }

            return invoices;
        }

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

        private static int CountMonthsInclusive(DateOnly start, DateOnly end)
        {
            var months = (end.Year - start.Year) * 12 + (end.Month - start.Month) + 1;
            return Math.Max(0, months);
        }

        private static int CountYearsInclusive(DateOnly start, DateOnly end)
        {
            var years = (end.Year - start.Year) + 1;
            return Math.Max(0, years);
        }

    }
}
