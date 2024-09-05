using FJCO20240905.API.EndPoints;
using FJCO20240905.API.Models.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FJCOContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cnn"));
});

builder.Services.AddScoped<FJCOProductDAL>();

var app = builder.Build();

app.AddProductEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
