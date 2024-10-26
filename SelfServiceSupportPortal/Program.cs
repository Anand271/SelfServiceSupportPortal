using AadharVerify.Data.Core.DataRepository.RepositoryWrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SelfServiceSupportPortal.Data.Core.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SelfServiceSupportPortalDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SelfServiceSupportPortalDbContextConnection' not found.");

builder.Services.AddDbContext<SelfServiceSupportPortalDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SelfServiceSupportPortalDbContext>();

builder.Services.AddScoped<IDataRepositoryWrapper, DataRepositoryWrapper>();// Add services to the container.


builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Customer" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    //var serviceProvide = builder.Services.BuildServiceProvider();
    //var userManager = serviceProvide.GetRequiredService<UserManager<ApplicationUser>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string email = "admin@test.com";
    string pass = "Pass@1234";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ApplicationUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, pass);
        await userManager.AddToRoleAsync(user, "Admin");
    }
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.MapRazorPages();
app.Run();
