using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TMB_REST.Models;

namespace TMB_REST.Data
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
