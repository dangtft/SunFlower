using Microsoft.EntityFrameworkCore;
using WebSunFlower.Data;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.Models.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository,ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(ShoppingCartRepository.GetCart);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IProductComment, ProductCommentRepository>();

builder.Services.AddDbContext<SunFlowerDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("SunFlowerDbContextConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SunFlowerDbContext>();


//Session
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
//app.Use(async (context, next) =>
//{
//    var user = context.User;

//    if (user.Identity.IsAuthenticated)
//    {
//        if (user.IsInRole("Admin"))
//        {
//            context.Response.Redirect("/Admin/AdminDashboard");
//        }
//        else
//        {
//            context.Response.Redirect("/User/UserDashboard");
//        }
//    }

//    await next();
//});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
