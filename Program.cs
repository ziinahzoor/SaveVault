using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SaveVault.Repositories;
using SaveVault.Repositories.Implementation;
using SaveVault.Services;
using SaveVault.Services.Implementation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddControllers()
	.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(logging =>
{
	logging.AddConsole();
	logging.AddDebug();
});

builder.Services.AddDbContext<SaveVaultDbContext>(options =>
	options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IDownloadService, DownloadService>();

builder.Services.AddScoped<IUploadRepository, UploadRepository>();
builder.Services.AddScoped<IDownloadRepository, DownloadRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
