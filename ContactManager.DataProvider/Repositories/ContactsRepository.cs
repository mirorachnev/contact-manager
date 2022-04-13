﻿using ContactManager.DataProvider.DbData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactManager.DataProvider.Repositories
{
    /// inheritdoc
    internal class ContactsRepository : IContactsRepository
    {
        private readonly ContactManagerDbContext _context;
        private readonly ILogger<ContactsRepository> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="contactManagerDbContext">Db context</param>
        public ContactsRepository(ILogger<ContactsRepository> logger, ContactManagerDbContext contactManagerDbContext)
        {
            _logger = logger;
            _context = contactManagerDbContext;
        }

        /// inheritdoc
        public Task Create(Contact contact)
        {
            throw new NotImplementedException();
        }

        /// inheritdoc
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// inheritdoc
        public async Task<Contact?> GetAsync(Guid id)
        {
            return await _context.Contacts.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// inheritdoc
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            return await _context.Contacts.AsQueryable().ToListAsync();
        }

        /// inheritdoc
        public Task Update(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
