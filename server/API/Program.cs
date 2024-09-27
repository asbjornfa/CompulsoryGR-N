using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Request;
using Service.Interfaces;
using Service.Services;
using Service.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")); //The connection string is in the appsettings file
});

builder.Services.AddControllers();
builder.Services.AddTransient<IValidator<RequestCreatePaperDTO>, CreatePaperValidator>(); // Tilføj din validator her
builder.Services.AddTransient<IValidator<RequestCreateCustomerDTO>, CreateCustomerValidator>();

builder.Services.AddScoped<IPaper, PaperService>();
builder.Services.AddScoped<ICustomer, CustomerService>();

builder.Services.AddOpenApiDocument();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<MyDbContext>()
        .Database.EnsureCreated();
}


app.UseOpenApi();
app.UseSwaggerUi();

app.MapControllers();

app.UseCors(config =>
{
    config.AllowAnyHeader();
    config.AllowAnyMethod();
    config.AllowAnyOrigin();
});

app.Run();