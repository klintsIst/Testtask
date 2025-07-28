using AGSRTestTask.WebAPI;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Patient API",
        Description = "An ASP.NET Core Web API for managing Patient items",
        TermsOfService = new Uri("https://test.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Test Contact",
            Url = new Uri("https://test.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Test License",
            Url = new Uri("https://test.com/license")
        }
    });
    string? xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddServiceConfigs();

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
