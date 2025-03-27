﻿using Microsoft.EntityFrameworkCore;
using RTAUI.Models;
using System.Collections.Generic;

namespace RTAUI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<AIInstruction> AIInstructions { get; set; }
    }
}
