using DataAccess;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

// Fix.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Missing DefaultConnection connection string");
}

// Register ObjectSimulatorDbContext
builder.Services.AddDbContext<ObjectSimulatorDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline. Ah
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
