using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<Student>, StudentRepository>();

builder.Services.AddEndpointsApiExplorer();
object value = builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UniversityContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.Configure<UniversityMongoDbSettings>(
    builder.Configuration.GetSection("MongoDbConnection"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
