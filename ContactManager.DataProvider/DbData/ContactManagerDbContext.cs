using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<Contact> Contacts { get; set; }

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasKey(x => x.ContactId);
                
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

            modelBuilder.Entity<Contact>().ToTable("Contacts");
        }
    }
}
