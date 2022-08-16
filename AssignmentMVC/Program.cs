var builder = WebApplication.CreateBuilder(args);

//Service mvc working
builder.Services.AddMvc();

//The app
var app = builder.Build();

//Get access to static files
app.UseStaticFiles();

//Enable routing
app.UseRouting();

//Define some routings
//Nr 1
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Nr 2
app.MapControllerRoute(
    name: "test",
    pattern: "test123",    
    defaults: new {controller = "Home", action = "Test"});

// About Me route
app.MapControllerRoute(
    name: "aboutme",
    pattern: "aboutme",
    defaults: new { controller = "Home", action = "Aboutmeperson" });

//Contact route
app.MapControllerRoute(
    name: "contact",
    pattern: "contact",
    defaults: new { controller = "Home", action = "ContactMe" });

//Project route
app.MapControllerRoute(
    name: "project",
    pattern: "project",
    defaults: new { controller = "Home", action = "MyProjects" });

//Project route


//Pattern property: How URL should look in the browsern
//action = the value MUST match method name in HomeController


app.Run();
