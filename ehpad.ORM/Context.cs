using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ehpad.ORM
{
    public class Context : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Injection> Injections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Injection>()
                .HasKey(i => new { i.PeopleId, i.VaccineId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=ephad.db");
            //options.UseSqlite(ConfigurationManager.ConnectionStrings["SqLiteDatabase"].ConnectionString);
        }
    }
}
