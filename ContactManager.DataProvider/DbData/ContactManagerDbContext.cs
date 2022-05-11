using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable

namespace ContactManager.DataProvider.DbData
{
    /// <summary>
    /// Data context
    /// </summary>
    public class ContactManagerDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextOptions">Context options</param>
        public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        /// <summary>
        /// Contacts
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary key
            modelBuilder.Entity<Contact>()
                .HasKey(x => x.ContactId);
                
            // Unique indeces
            modelBuilder.Entity<Contact>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(x => x.Iban)
                .IsUnique();

            // Required
            modelBuilder.Entity<Contact>()
                .Property(x => x.Id)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.PhoneNumber)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.Address)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(x => x.Iban)
                .HasMaxLength(34)
                .IsRequired();

            modelBuilder.Entity<Contact>().ToTable("Contacts");
        }
    }
}
