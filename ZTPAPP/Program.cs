using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using projekt.Models;


var conStrings = ConnectionStrings.GetInstance();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WDbContext>(options =>
{
    options.UseSqlServer(conStrings.GetDefault());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
    });


var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "User", action = "Register" });

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "User", action = "Login" });
app.MapControllerRoute(
    name: "logout",
    pattern: "logout",
    defaults: new { controller = "User", action = "Logout" });
app.MapControllerRoute(
    name: "addflashcard",
    pattern: "addflashcard",
    defaults: new { controller = "Flashcard", action = "AddFlashcard" });
app.MapControllerRoute(
    name: "allflashcards",
    pattern: "allflashcards",
    defaults: new { controller = "Flashcard", action = "AllFlashcards" });
app.MapControllerRoute(
    name: "edit",
    pattern: "edit/{id?}",
    defaults: new { controller = "Flashcard", action = "Edit" });
app.MapControllerRoute(
    name: "delete",
    pattern: "delete/{id?}",
    defaults: new { controller = "Flashcard", action = "Delete" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
