using RockBank.Infra.Data;
using RockBank.Services;
using RockBank.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDBContext>(builder.Configuration["ConnectionString:RockBankDb"]);

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
