using RedisExchangeAPI.Web.Services;

var builder = WebApplication.CreateBuilder(args);
RedisServices redisServices = new RedisServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RedisServices>();//1 tane nesne �rne�i ald�m 

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

app.UseAuthorization();
redisServices.Connect();//Uygulama aya�a kalkt��� zaman ba�lant�y� ba�latt�m
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
