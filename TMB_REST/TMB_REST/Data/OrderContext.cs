using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TMB_REST.Models;

namespace TMB_REST.Data
{
    public class OrderContext : DbContext

    {
        
        public DbSet<OrderModel> Orders { get; set; }


        public OrderContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 
        
        }

        public DbSet<OrderModel> DBOrder {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=order.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
