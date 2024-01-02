using DBHosting.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string DEVELOPERDASHBOARD_DBCONNECTIONSTRING = $"Server={Environment.GetEnvironmentVariable("PGHOST")};Database={Environment.GetEnvironmentVariable("DATABASE_URL")};Port={Environment.GetEnvironmentVariable("PGPORT")};Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("PGPASSWORD")}";
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DBDeployContext>(
    options =>
        options
            .UseNpgsql(
                DEVELOPERDASHBOARD_DBCONNECTIONSTRING
                    ?? throw new InvalidOperationException(
                        "Connection string 'DBHostingDB' not found."
                    )
            )
            .UseSnakeCaseNamingConvention()
);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DBDeployContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
