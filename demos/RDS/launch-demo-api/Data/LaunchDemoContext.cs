using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using launch_demo_api.Models;

    public class LaunchDemoContext : DbContext
    {
        public LaunchDemoContext (DbContextOptions<LaunchDemoContext> options)
            : base(options)
        {
        }

        public DbSet<launch_demo_api.Models.Product> Product { get; set; }
    }
