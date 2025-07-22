using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Data;
using Alnudaar2.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=../Alnudaar2_database/alnudaar_database.db"));

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IScreenTimeScheduleService, ScreenTimeScheduleService>();
builder.Services.AddScoped<IBlockRulesService, BlockRulesService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://0.0.0.0:5173") // Add your frontend's origin here
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(); // Enable CORS

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();