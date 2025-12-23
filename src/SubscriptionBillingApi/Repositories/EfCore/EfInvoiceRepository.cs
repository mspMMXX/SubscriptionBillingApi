using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    /// <summary>
    /// EF Core–based repository implementation for Invoice entities.
    /// Handles persistence and retrieval of invoices and their lines.
    /// </summary>
    public class EfInvoiceRepository : IInvoiceRepository
    {
        private readonly BillingDbContext _db;
        public EfInvoiceRepository(BillingDbContext db) => _db = db;

        /// <summary>
        /// Adds a new invoice to the database.
        /// </summary>
        public async Task AddAsync(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing invoice.
        /// Uses explicit attach + Modified state to persist changes
        /// on a detached aggregate.
        /// </summary>
        public async Task UpdateAsync(Invoice invoice)
        {
            _db.Invoices.Attach(invoice);
            _db.Entry(invoice).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a single invoice by id including its invoice lines.
        /// </summary>
        public Task<Invoice?> GetByIdAsync(Guid invoiceId)
            => _db.Invoices
                .Include("_lines")
                .FirstOrDefaultAsync(x => x.Id == invoiceId);

        /// <summary>
        /// Returns all invoices including their invoice lines.
        /// </summary>
        public Task<List<Invoice>> GetAllAsync()
            => _db.Invoices
                .Include("_lines")
                .ToListAsync();

        /// <summary>
        /// Deletes an invoice by id.
        /// Returns true if the invoice existed and was deleted.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid invoiceId)
        {
            var entity = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
            if (entity is null) return false;

            _db.Invoices.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Finds an existing draft invoice for a customer and billing period.
        /// Used to prevent creating duplicate draft invoices during billing.
        /// </summary>
        public Task<Invoice?> FindDraftAsync(Guid customerId, DateOnly periodStart, DateOnly periodEnd)
            => _db.Invoices
                .Include("_lines")
                .FirstOrDefaultAsync(x =>
                    x.CustomerId == customerId &&
                    x.PeriodStart == periodStart &&
                    x.PeriodEnd == periodEnd &&
                    x.Status == InvoiceStatus.Draft);

        /// <summary>
        /// Adds a single invoice line to an existing invoice.
        /// </summary>
        public async Task AddLineAsync(InvoiceLine line)
        {
            _db.InvoiceLines.Add(line);
            await _db.SaveChangesAsync();
        }

    }
}
