using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Services
{
    /// <summary>
    /// Application service for invoice-related use cases.
    /// Responsible for creating, retrieving, updating and deleting invoices,
    /// and for enforcing invoice-specific business rules.
    /// </summary>
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        /// <summary>
        /// Returns an existing draft invoice for the given customer and period,
        /// or creates a new draft invoice if none exists.
        /// This makes the operation safe to call multiple times for the same period.
        /// </summary>
        public async Task<Invoice> GetOrCreateDraftInvoice(Guid customerId, DateOnly periodStart, DateOnly periodEnd, string currency)
        {
            // Try to reuse an existing draft invoice for this period
            var existing = await _invoiceRepository.FindDraftAsync(customerId, periodStart, periodEnd);
            if (existing is not null) return existing;

            // Create a new draft invoice
            var invoice = new Invoice(customerId, periodStart, periodEnd, currency, InvoiceStatus.Draft);

            // Generate a readable but unique invoice number
            var invoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

            invoice.AssignInvoiceNumber(invoiceNumber);
            invoice.AssignIssuedAt(DateTime.UtcNow);

            await _invoiceRepository.AddAsync(invoice);
            return invoice;
        }

        /// <summary>
        /// Returns an invoice by id, or null if it does not exist.
        /// </summary>
        public Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId)
        {
            return _invoiceRepository.GetByIdAsync(invoiceId);
        }

        /// <summary>
        /// Returns all invoices.
        /// </summary>
        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllAsync();
        }

        /// <summary>
        /// Deletes an invoice by id.
        /// Returns true if the invoice existed and was deleted.
        /// </summary>
        public async Task<bool> DeleteInvoiceAsync(Guid invoiceId)
        {
            return await _invoiceRepository.DeleteAsync(invoiceId);
        }


        /// <summary>
        /// Persists changes to an existing invoice.
        /// </summary>
        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            await _invoiceRepository.UpdateAsync(invoice);
        }

        /// <summary>
        /// Adds a single invoice line to an invoice.
        /// </summary>
        public Task AddLineAsync(InvoiceLine line) => _invoiceRepository.AddLineAsync(line);
    }
}
