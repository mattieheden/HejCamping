﻿using System;
using System.Collections.Generic;
using HejCamping.Domain;
using HejCamping.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HejCamping.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }

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
        modelBuilder.Entity<Review>().HasKey(r => r.OrderNumber);
    }
}
