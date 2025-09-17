using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Posexpress.Api.Extensions;                              
using Posexpress.Infrastructure.Persistence.EntityFramework; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
})

.AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";         
    o.SubstituteApiVersionInUrl = true;   
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Posexpress API", Version = "v1" });
});

builder.Services.AddPosexpressServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CustomDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Posexpress API v1");
});

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();
