WebApplicationBuilder brenda = WebApplication.CreateBuilder(args);

brenda.Services.AddInfraestruturaEmJson();
brenda.Services.AddControllersWithViews();

WebApplication app = brenda.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();

