using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Models;
using shoes_final_exam.Models.AuthenticationModels;
using shoes_final_exam.Models.MailModels;
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

           

            // Connect database
            var connectionString = builder.Configuration.GetConnectionString("MyDb");
            builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;

                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<MyDbContext>()
			.AddDefaultTokenProviders();

			builder.Services.Configure<DataProtectionTokenProviderOptions>
            (opt => opt.TokenLifespan = TimeSpan.FromHours(2));

			builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/Account/Login");

            builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsFactory>();

			var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
                                                .Get<EmailConfiguration>();
			builder.Services.AddSingleton(emailConfig);

			builder.Services.AddScoped<shoes_final_exam.Repositories.IEmailSender, EmailSender>();

			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ISizeRepository, SizeRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromDays(30);
                option.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );


            // Seeding Data
            var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<MyDbContext>();
            var mapper = app.Services.CreateScope().ServiceProvider.GetRequiredService<IMapper>();
            var userManager = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            SeedData.SeedingData( context, mapper, userManager);

            app.Run();
        }
    }
}