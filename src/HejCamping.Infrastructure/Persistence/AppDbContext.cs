using System;
using System.Collections.Generic;
using HejCamping.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HejCamping.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HejCamping.Domain.Review> Reviews { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasKey(b => b.OrderNumber);
            modelBuilder.Entity<HejCamping.Domain.Review>().HasKey(r => r.OrderNumber);
        }
    }
}

