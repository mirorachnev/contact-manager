using ContactManager.DataProvider.DbData;
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
        public async Task CreateAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
        }

        /// inheritdoc
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

                if (contact != null)
                {
                    _context.Contacts.Remove(contact);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
        }

        /// inheritdoc
        public async Task<Contact?> GetAsync(Guid id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// inheritdoc
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        /// inheritdoc
        public async Task<bool> UpdateAsync(Contact contact)
        {
            try
            {
                var dbContact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);

                if (dbContact != null)
                {
                    dbContact.FirstName = contact.FirstName;
                    dbContact.LastName = contact.LastName;
                    dbContact.Email = contact.Email;
                    dbContact.PhoneNumber = contact.PhoneNumber;
                    dbContact.DateOfBirth = contact.DateOfBirth;
                    dbContact.Address = contact.Address;
                    dbContact.Iban = contact.Iban;

                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
        }
    }
}
