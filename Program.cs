using Orleans.Runtime;
using OrleansURLShortener.Router;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseOrleans(static siloBuilder =>
{
    siloBuilder.UseLocalhostClustering(); // <-- Scalable clusters for production app could be configured here
    siloBuilder.AddMemoryGrainStorage("urls");
});

using var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var router = new Router(app);

// Map the endpoints
router.MapEndpoints();

app.Run();
