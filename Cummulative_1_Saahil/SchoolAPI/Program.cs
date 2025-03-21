using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add Database Context
var connectionString = "server=localhost;database=school_db;user=root;password=root";
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31))));

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TeacherPage}/{action=List}/{id?}"
);
app.Run();
