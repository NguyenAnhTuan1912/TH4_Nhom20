﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BrandModel> BRAND { get; set; }
        public DbSet<CameraModel> CAMERA { get; set; }
    }
}