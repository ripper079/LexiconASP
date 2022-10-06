using AssignmentMVC.Data;
using Microsoft.EntityFrameworkCore;
using AssignmentMVC.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Marko
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(
        policy =>
        {
            //policy.WithOrigins("*");
            //.AllowAnyHeader()
            //.AllowAnyMethod();
            policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});



//Service mvc working
builder.Services.AddMvc();




//Enable Session support - Step 1
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

//Add entity framework service support, configure/setup for Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options => {                                 //What db to use
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));       //Use DefaultConnection, must match with appsettings.json
});


//Identity config - Use ApplicationUser for Identity[Customizing identity] (insteed of the built in IdentityUser)
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                                                    .AddRoles<IdentityRole>()
                                                    .AddEntityFrameworkStores<ApplicationDbContext>();



//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//                                .AddDefaultTokenProviders()
//                                .AddEntityFrameworkStores<ApplicationDbContext>();



//Custom config for identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase  = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
});

//For identity support
builder.Services.AddRazorPages();



//The app
var app = builder.Build();

//Enable session - Step 2
app.UseSession();

//Get access to static files
app.UseStaticFiles();

//Enable routing
app.UseRouting();

//Cors - MArko
app.UseCors();


//Authentication support - This code line BEFORE Authorization
app.UseAuthentication();
//Authorization support
app.UseAuthorization();
//For identity
app.MapRazorPages();



//Define some routings
//Nr 1
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Route to doctor
app.MapControllerRoute(
    name: "FeverCheck",
    pattern: "FeverCheck",
    defaults: new { controller = "Doctor", action = "FeverCheck" });

//Route to Guessing game
app.MapControllerRoute(
    name: "GuessingGame",
    pattern: "GuessingGame",
    defaults: new { controller = "Home", action = "GuessingGame" });

app.Run();
