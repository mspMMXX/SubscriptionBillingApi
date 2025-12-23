using SubscriptionBillingApi.Services;
using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Repositories.InMemory;
using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Repositories.EfCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

//builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
//builder.Services.AddSingleton<ISubscriptionPlanRepository, InMemorySubscriptionPlanRepository>();
//builder.Services.AddSingleton<ISubscriptionRepository, InMemorySubscriptionRepository>();
//builder.Services.AddSingleton<IInvoiceRepository, InMemoryInvoiceRepository>();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<SubscriptionPlanService>();
builder.Services.AddScoped<BillingService>();

builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, EfSubscriptionPlanRepository>();
builder.Services.AddScoped<ISubscriptionRepository, EfSubscriptionRepository>();
builder.Services.AddScoped<IInvoiceRepository, EfInvoiceRepository>();



// Learn more about configuring Swagger/OpenAPI at Ok https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
