var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // here in then first cause it catch any error after allo MW write on response -- ayman
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // get any static file (html, css, js, v , a, img )

app.UseRouting();   // try rout url 

app.UseAuthorization(); // chk be4 rout 

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Instructor}/{action=Index}/{id?}");

app.Run();