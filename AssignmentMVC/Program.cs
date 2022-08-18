var builder = WebApplication.CreateBuilder(args);

//Service mvc working
builder.Services.AddMvc();

//Enable Session support - Step 1
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});


//The app
var app = builder.Build();

//Enable session - Step 2
app.UseSession();

//Get access to static files
app.UseStaticFiles();

//Enable routing
app.UseRouting();

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
