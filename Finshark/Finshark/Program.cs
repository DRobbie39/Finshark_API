using Finshark.Data;
using Finshark.Interfaces;
using Finshark.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Kết nối CSDL vào web
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    //Cho biết kết nối với CSDL nào
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Kết nối các dịch vụ
builder.Services.AddScoped<IStockRepository, StockRepository>();

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
