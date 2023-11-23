using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Mail;
using shoes_final_exam.Models;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;
using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace shoes_final_exam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Connect database
            var connectionString = builder.Configuration.GetConnectionString("MyDb");
            builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}"
                       );
            });

            app.Run();
        }
    }
}