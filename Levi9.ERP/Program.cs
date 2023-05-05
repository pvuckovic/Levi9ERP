using Levi9.ERP.Domain;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Repository;
using Levi9.ERP.Domain.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataBaseContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("ErpDatabase")));
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPriceListRepository, PriceListRepository>();
builder.Services.AddScoped<IPriceListService, PriceListService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof())

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
