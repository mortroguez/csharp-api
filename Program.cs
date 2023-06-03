using csharp_api.DatabaseModels;
using csharp_api.Interfaces;
using csharp_api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(
    (options) =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
    }
);

builder.Services.AddScoped<ICustomer, CustomerRepository>();

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
