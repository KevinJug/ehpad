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

        private string connectionString = "Data Source=ephad.db";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Injection>()
                .HasKey(i => new { i.PeopleId, i.VaccineId });
        }

        public Context(string cs)
        {
            connectionString = cs;
        }

        public Context() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(connectionString);
        }
    }
}
