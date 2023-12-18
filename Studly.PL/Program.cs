using Studly.BLL.Infrastructure;
using Studly.PL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

ConfigurationService.ConfigureSwagger(builder.Services);

ConfigurationService.ConfigureJwtAuthentication(builder.Services, builder.Configuration);

ConfigurationService.ConfigureDbContext(builder.Services, builder.Configuration);

ConfigurationService.ConfigureServices(builder.Services);

ConfigurationService.ConfigureCors(builder.Services);

ConfigurationService.ConfigureAutoMapper(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();