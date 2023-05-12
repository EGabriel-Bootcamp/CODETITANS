using Mauser;
using Mauser.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;
using Services;
using Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMauserService();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
