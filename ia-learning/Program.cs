using ia_learning.Data;
using ia_learning.Configurations;
using ia_learning.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLoggingSetup();

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddSwaggerSetup();
builder.Services.AddApiVersioningSetup();
builder.Services.AddHealthCheckSetup();

var openAiKey = Environment.GetEnvironmentVariable(
    builder.Configuration["OpenAI:ApiKey"]
);

builder.Services.AddSingleton(new OpenAIService(openAiKey));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwaggerSetup();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); 

app.Run();
