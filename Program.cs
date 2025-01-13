using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Thêm CORS vào DI container
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp", policy =>
	{
		policy.WithOrigins("http://localhost:3000") // Địa chỉ React App
					.AllowAnyHeader()
					.AllowAnyMethod();
	});
});

// Thêm CORS vào DI container
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactRealApp", policy =>
	{
		policy.WithOrigins("https://6bfxmc9p-3000.asse.devtunnels.ms:3000") // Địa chỉ React App
					.AllowAnyHeader()
					.AllowAnyMethod();
	});
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Kích hoạt CORS
app.UseCors("AllowReactApp");
app.UseCors("AllowReactRealApp");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
