using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Data
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>()
                .Property(x => x.StartDate)
                .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

            modelBuilder.Entity<Subscription>()
                .Property(x => x.NextBillingDate)
                .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

            modelBuilder.Entity<Subscription>()
                .Property(x => x.EndDate)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                    v => v.HasValue ? DateOnly.FromDateTime(v.Value) : (DateOnly?)null);

            modelBuilder.Entity<Subscription>()
                .Property(x => x.CancelDate)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                    v => v.HasValue ? DateOnly.FromDateTime(v.Value) : (DateOnly?)null);

            modelBuilder.Entity<Invoice>()
                .Property(x => x.PeriodStart)
                .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

            modelBuilder.Entity<Invoice>()
                .Property(x => x.PeriodEnd)
                .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

            modelBuilder.Entity<Invoice>(b =>
            {
                b.HasKey(i => i.Id);

                b.Ignore(i => i.Lines);

                b.HasMany<InvoiceLine>("_lines")
                 .WithOne(l => l.Invoice)
                 .HasForeignKey(l => l.InvoiceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InvoiceLine>(b =>
            {
                b.HasKey(l => l.Id);
            });
        }
    }
}
