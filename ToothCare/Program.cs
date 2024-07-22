using Microsoft.AspNetCore.Authentication.Cookies;
using ToothCare;
using ToothCare.Services;
using ToothCare.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//MAPPER
builder.Services.AddAutoMapper(typeof(MappingConfig));

//AUTH SERVICE
builder.Services.AddHttpClient<IAuthServices,AuthServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();

//DOCTORS SERVICE
builder.Services.AddHttpClient<IDoctorsServices, DoctorsServices>();
builder.Services.AddScoped<IDoctorsServices, DoctorsServices>();

//CLIENTS SERVICE
builder.Services.AddHttpClient<IClientsServices, ClientsServices>();
builder.Services.AddScoped<IClientsServices, ClientsServices>();

//APPOINTMENTS SERVICE
builder.Services.AddHttpClient<IAppointmentsServices, AppointmentsServices>();
builder.Services.AddScoped<IAppointmentsServices, AppointmentsServices>();

//PROCEDUREES SERVICE
builder.Services.AddHttpClient<IProceduresServices, ProceduresServices>();
builder.Services.AddScoped<IProceduresServices, ProceduresServices>();

//HTTP CONTEXT ACCESOR
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                  options.LoginPath = "/Auth/Login";
                  options.AccessDeniedPath = "/Auth/AccessDenied";
                  options.SlidingExpiration = true;
              });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
