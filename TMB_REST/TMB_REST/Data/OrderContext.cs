using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMB_REST.Models;

namespace TMB_REST.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext (DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<TMB_REST.Models.OrderModel> OrderModel { get; set; } = default!;
    }
}
