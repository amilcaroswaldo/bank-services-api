using AutoMapper;
using creditcard.application;
using creditcard.Infraestructure;
using creditcard.webapi.Mapping;
using creditcard.webapi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ServicesCreditCard.WebApi",
        Version = "v1"
    });
});
// Add services to the container.
// Agregar el filtro global
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
#region mapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ApplicationMapping());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

//add persistence
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddValidator();
builder.Services.AddHealthCheck();

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
    }
    else
    {
        await next();
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseValidationMiddleware();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "creditcard.webapi v1"));

app.UseHttpsRedirection();
//app.UseHeaderPropagation();
app.UseRouting();
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();
