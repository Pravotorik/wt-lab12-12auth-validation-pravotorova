using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_pravotorova.Data;

var builder = WebApplication.CreateBuilder(args);

// === 1. Õ¿—“–Œ… ¿ œŒƒ Àﬁ◊≈Õ»ﬂ   ¡ƒ ===
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ≈ÒÎË Ï˚ Ì‡ Render, ËÒÔÓÎ¸ÁÛÂÏ DATABASE_URL
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    int dbPort = uri.Port > 0 ? uri.Port : 5432;

    connectionString = $"Host={uri.Host};Port={dbPort};Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={Uri.UnescapeDataString(userInfo[0])};Password={Uri.UnescapeDataString(userInfo[1])};" +
                       $"SSL Mode=Require;Trust Server Certificate=true";
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// === 2. –≈√»—“–¿÷»ﬂ —≈–¬»—Œ¬ (—“–Œ√Œ ƒŒ BUILD) ===
builder.Services.AddSession();
builder.Services.AddRazorPages();

var app = builder.Build();

// === 3. Ã»√–¿÷»» ===
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// === 4. MIDDLEWARE (—“–Œ√Œ œŒ œŒ–ﬂƒ ”) ===
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // œÓÒÎÂ UseRouting, ‰Ó Authorization

app.UseAuthorization();
app.MapRazorPages();

// === 5. «¿œ”—  ===
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");