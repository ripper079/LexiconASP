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
    pattern: "test",
    defaults: new {controller = "Home", action = "Test"});

//Pattern property: How URL should look in the browsern



app.Run();
