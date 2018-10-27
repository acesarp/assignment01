using System;
using System.Collections.Generic;
using System.Text;
using Assignment01_ASP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment01_ASP.Data;

namespace Assignment01_ASP.Data {
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
    }
}
