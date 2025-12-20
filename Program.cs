using SubscriptionBillingApi.Services;
using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Repositories.InMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, InMemorySubscriptionPlanRepository>();
builder.Services.AddScoped<ISubscriptionRepository, InMemorySubscriptionRepository>();
builder.Services.AddScoped<IInvoiceRepository, InMemoryInvoiceRepository>();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<SubscriptionPlanService>();

// Learn more about configuring Swagger/OpenAPI at Ok https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
