using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC.Entities;
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


            // add - register
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();  // create object by request / Client
            //builder.Services.AddSingleton<ICourseRepository, CourseRepository>(); // create object by server
            //builder.Services.AddTransient<ICourseRepository, CourseRepository>(); create object by inject
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}