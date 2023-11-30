using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Models;
using shoes_final_exam.Models.AuthenticationModels;

namespace shoes_final_exam.Data
{
    public class SeedData
    {
        public static async Task SeedingData(
            MyDbContext _context,
            IMapper _mapper,
            UserManager<AppUser> _userManager
            )
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
                Category nike = new Category
                {
                    Name = "Nike"
                };
                Category balenciaga = new Category
                {
                    Name = "Balenciaga"
                };
                Category converse = new Category
                {
                    Name = "Converse"
                };

                Size size44 = new Size
                {
                    SizeNumber = 44
                };

                Size size39 = new Size
                {
                    SizeNumber = 39
                };

                Size size42 = new Size
                {
                    SizeNumber = 42
                };


                 _context.Products.AddRange(
                    new Product
                    {
                        Image = "../images/1.jpg",
                        Name = "Dunk Low Off-White 'Pine Green'",
                        Description = "Dunk Low Off-White 'Pine Green' là giày đỉnh cao nhất thời hiện đại",
                        Price = 6500000,
                        Quantity = 3,
                        IsNew = false,
                        CreatedDate = DateTime.Now,
                        Category = nike,
                        Size = size44,
                    },
                    new Product
                    {
                        Image = "../images/2.jpg",
                        Name = "Ba lèn si a ga Triple S 'Clear Sor Beige'",
                        Description = "Ba lèn si a ga Triple S 'Clear Sor Beige' là giày sang nhất thời hiện đại",
                        Price = 6999000,
                        Quantity = 1,
                        IsNew = false,
                        CreatedDate = DateTime.Now,
                        Category = balenciaga,
                        Size = size39,
                    },
                    new Product
                    {
                        Image = "../images/3.jpg",
                        Name = "Converse x FOG",
                        Description = "Converse x FOG là giày đen nhất hiện tại",
                        Price = 2000000,
                        Quantity = 3,
                        IsNew = false,
                        CreatedDate = DateTime.Now,
                        Category = converse,
                        Size = size42,
                    }
                );

                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                UserRegistrationModel userModel = new UserRegistrationModel()
                {
                    FullName = "Admin",
                    Address = "Admin o dau?",
                    Birthday = DateTime.Now,
                    Email = "ngoctruongbui2003@gmail.com",
                    Password = "admin123@",
                    ConfirmPassword = "admin123@"
                };

                var user = _mapper.Map<AppUser>(userModel);
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                
            }
        }
    }
}
