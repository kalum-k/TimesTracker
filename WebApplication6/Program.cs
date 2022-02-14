using Microsoft.EntityFrameworkCore;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Data;
using Microsoft.AspNetCore.Identity;
using TimeTracker.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TimeSystemDbContextConnection");    
builder.Services.AddDbContext<TimeTrackerContext>(options =>
    options.UseSqlite(connectionString));builder.Services.AddDbContext<TimeTrackerDbContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddDefaultIdentity<TimeSystemUser>(options =>
 {
     options.SignIn.RequireConfirmedAccount = false;
     options.Password.RequireLowercase = false;
     options.Password.RequireLowercase = false;
 })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TimeTrackerDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePages();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
app.Run();
