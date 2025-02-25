using Microsoft.EntityFrameworkCore;
using tickter.Core.AutoMapperConfig;
using tickter.Core.Context;
using AutoMapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// create a database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("local");
    options.UseSqlServer(connectionString);
});



// config AutoMapper 
var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperConfigProfile()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
