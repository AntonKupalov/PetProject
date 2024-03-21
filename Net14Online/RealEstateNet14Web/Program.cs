using Microsoft.EntityFrameworkCore;
using RealEstateNet14Web.BuisnessService;
using RealEstateNet14Web.CustomMiddlewares;
using RealEstateNet14Web.Controllers.Auth;
using RealEstateNet14Web.DbStuff;
using RealEstateNet14Web.DbStuff.Repositories;
using RealEstateNet14Web.Services;
using RealEstateNet14Web.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(AuthRealEstateController.AUTH_KEY_REAL_ESTATE)
    .AddCookie(AuthRealEstateController.AUTH_KEY_REAL_ESTATE, option =>
    {
        option.LoginPath = "/AuthRealEstate/RealEstateLogin";
    });

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.SetIsOriginAllowed(url => true);
        policy.AllowCredentials();
    });
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionStringRealEsate = builder.Configuration.GetConnectionString("Net14WebRE");
builder.Services.AddDbContext<WebRealEstateDbContext>(x => x.UseSqlServer(connectionStringRealEsate));

// Repositories
builder.Services.AddScoped<ApartmentOwnerRepository>();
builder.Services.AddScoped<ApartamentRepository>();


// Services
builder.Services.AddScoped<DeleteUser>();
builder.Services.AddScoped<UpdateUser>();
builder.Services.AddScoped<RealEstateAuthService>();
builder.Services.AddScoped<ApartmentOwnerBuisnessService>();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<CustomLocalizationMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Who I am?
app.UseAuthorization(); // May I?

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();