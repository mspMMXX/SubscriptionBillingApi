using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    public class EfInvoiceRepository : IInvoiceRepository
    {
        private readonly BillingDbContext _db;
        public EfInvoiceRepository(BillingDbContext db) => _db = db;

        public async Task AddAsync(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _db.Invoices.Attach(invoice);
            _db.Entry(invoice).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


        public Task<Invoice?> GetByIdAsync(Guid invoiceId)
            => _db.Invoices
                .Include("_lines")
                .FirstOrDefaultAsync(x => x.Id == invoiceId);

        public Task<List<Invoice>> GetAllAsync()
            => _db.Invoices
                .Include("_lines")
                .ToListAsync();

        public async Task<bool> DeleteAsync(Guid invoiceId)
        {
            var entity = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
            if (entity is null) return false;

            _db.Invoices.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<Invoice?> FindDraftAsync(Guid customerId, DateOnly periodStart, DateOnly periodEnd)
            => _db.Invoices
                .Include("_lines")
                .FirstOrDefaultAsync(x =>
                    x.CustomerId == customerId &&
                    x.PeriodStart == periodStart &&
                    x.PeriodEnd == periodEnd &&
                    x.Status == InvoiceStatus.Draft);

        public async Task AddLineAsync(InvoiceLine line)
        {
            _db.InvoiceLines.Add(line);
            await _db.SaveChangesAsync();
        }

    }
}
