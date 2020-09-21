using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisitorRegistration.Data.Mappers;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Visitor> Visitors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new VisitorConfiguration());
        }
    }
}
