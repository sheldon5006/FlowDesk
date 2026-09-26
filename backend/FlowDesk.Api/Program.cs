using FlowDesk.Api.Data;
using FlowDesk.Api.Extensions;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
}); 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<FlowDeskDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );

builder.Services.AddFlowDeskServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FlowDeskFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("FlowDeskFrontend");

app.MapControllers();

app.Run();
