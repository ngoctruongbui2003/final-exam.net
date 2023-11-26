using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data.Configure;
using shoes_final_exam.Models;
using System.Reflection.Emit;

namespace shoes_final_exam.Data
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Size> Sizes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
		{

			base.OnModelCreating(builder);

			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            builder.ApplyConfiguration(new RoleConfiguration());
        }
	}
}
