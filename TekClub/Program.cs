using Microsoft.EntityFrameworkCore;
using TekClub.Models.Data;
using Microsoft.AspNetCore.Identity;
using TekClub.Models;
using TekClub.Models.IRespositories;
using TekClub.Models.Repositories;
using System;
using TekClub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped<IClubRepository,ClubRepository>();
builder.Services.AddScoped<IActivité,ActivitéRepository>();
builder.Services.AddScoped<IEvenement,EvenementRepository>();

builder.Services.AddDbContext<ClubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TekupClub")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
     .AddRoles<IdentityRole>()
     .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ClubDbContext>()
  
    .AddEntityFrameworkStores<ClubDbContext>();




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


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapRazorPages();
app.MapHub<ChatHub>("/ChatHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
