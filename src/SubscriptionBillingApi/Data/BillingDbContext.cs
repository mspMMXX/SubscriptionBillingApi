using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Data
{
    /// <summary>
    /// Entity Framework Core DbContext for the Subscription & Billing domain.
    /// Defines DbSets and configures persistence mappings for domain entities.
    /// </summary>
    public class BillingDbContext : DbContext
    {
        /// <summary>
        /// DbContext is configured via dependency injection (connection string/provider in Program.cs).
        /// </summary>
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        // Aggregate roots / entities exposed to EF Core
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EF Core (especially with relational providers) stores DateOnly as DateTime by default,
            // therefore we explicitly define conversions for DateOnly and nullable DateOnly.
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
                // Primary key
                b.HasKey(i => i.Id);

                // Domain model exposes Lines (often read-only) while EF persists the backing field.
                // We ignore the public property and map the private field collection instead.
                b.Ignore(i => i.Lines);

                // Field-based mapping for the invoice lines collection ("_lines").
                // Cascade delete ensures invoice lines are removed when the invoice is deleted.
                b.HasMany<InvoiceLine>("_lines")
                 .WithOne(l => l.Invoice)
                 .HasForeignKey(l => l.InvoiceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InvoiceLine>(b =>
            {
                // Primary key
                b.HasKey(l => l.Id);
            });
        }
    }
}
