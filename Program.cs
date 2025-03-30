using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseSession();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

app.UseMiddleware<StudentManagement.Middlewares.RoleAuthorizationMiddleware>();
