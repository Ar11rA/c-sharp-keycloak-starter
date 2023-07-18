using FluentValidation.AspNetCore;
using Sample.Api.Config;
using Sample.Api.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.RegisterWebServices();
builder.Services.RegisterDataServices(builder.Configuration);

WebApplication app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<AuthMiddleware>();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<HttpLoggingMiddleware>();

app.Run();
