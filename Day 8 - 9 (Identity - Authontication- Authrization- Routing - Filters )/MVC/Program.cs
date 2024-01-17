using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC.Entities;
using MVC.Models;
using MVC.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // antother built in services but neet to register (DBContext)

            builder.Services.AddDbContext<ITIContext>(OptionBuilder =>
            {
                OptionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });

            builder.Services.AddControllersWithViews();

            // AddIdentity Work With IdentityRole,UserManager,SignInManager  based on ApplicationUser
            // (userManager and signInManager are generic)
            // for this . it ask to Know what it will work with
            // and it ask to know It the user Manager will Work with usre stor that deal with => IdentityDBContext or ITIContext
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>options.Password.RequireDigit=true
            ).AddEntityFrameworkStores<ITIContext>();
           // builder.Services.AddIdentityCore<ApplicationUser>(); work with identityUser Only

            //Custom Services register
            // add - register
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();  // create object by request / Client
            //builder.Services.AddSingleton<ICourseRepository, CourseRepository>(); // create object by server
            //builder.Services.AddTransient<ICourseRepository, CourseRepository>(); create object by inject
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Inline (Lambda Expression) Middelwares Test
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello From First Middelware \n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync("Welcome Back From First Middelware \n");

            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello From Second Middelware \n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync("Welcome Back Hello From Second Middelware \n");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello From Last Middelware \n");
            //});

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // understand the attribute =>[authorize] how to check cookie

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "ITI/{id : int : max(50)}",
            //    defaults: new { controller = "Instructor", action = "instructorDetails" }
            //);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}