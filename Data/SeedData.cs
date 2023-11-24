using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Models;

namespace shoes_final_exam.Data
{
    public class SeedData
    {
        public static void SeedingData(MyDbContext _context)
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
        }
    }
}
