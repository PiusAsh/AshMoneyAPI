using AshMoneyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AshMoneyAPI.Data
{
    public class AshDBContext : DbContext
    {
        public AshDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}
