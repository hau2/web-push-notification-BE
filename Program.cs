using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Thêm CORS vào DI container
// Cấu hình CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin", policy =>
	{
		policy.WithOrigins("https://example.com", "https://another-domain.com") // Thay bằng tên miền bạn muốn cho phép
					.AllowAnyHeader()
					.AllowAnyMethod();
	});

	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin() // Cho phép mọi tên miền
					.AllowAnyHeader()
					.AllowAnyMethod();
	});
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Kích hoạt CORS
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
