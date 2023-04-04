using Microsoft.EntityFrameworkCore;
using Jolib.Data;
using Jolib.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>((options => {
    options.EnableSensitiveDataLogging();
    options.UseNpgsql("name=Default");
}));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<PublisherRepository, PublisherRepository>();
builder.Services.AddScoped<AuthorRepository, AuthorRepository>();
builder.Services.AddScoped<BookRepository, BookRepository>();
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
