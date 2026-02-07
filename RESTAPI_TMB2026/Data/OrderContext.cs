using Microsoft.EntityFrameworkCore;
using RESTAPI_TMB2026.Models;

namespace RESTAPI_TMB2026.Data
{
    public class OrderContext : DbContext

    {
        public DbSet<OrderModel> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=orders.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
