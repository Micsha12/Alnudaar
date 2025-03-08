using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Data; // Add this line if ApplicationDbContext is defined in this namespace
using Alnudaar2.Server.Services; // Add this line if IScreenTimeScheduleService is defined in this namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=../Alnudaar2_database/alnudaar_database.db"));

// Register services
builder.Services.AddScoped<IScreenTimeScheduleService, ScreenTimeScheduleService>();

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

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();