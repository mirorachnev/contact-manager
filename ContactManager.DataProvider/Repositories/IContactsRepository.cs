using ContactManager.DataProvider.DbData;

namespace ContactManager.DataProvider.Repositories
{
    /// <summary>
    /// Contacts repository
    /// </summary>
    internal interface IContactsRepository
    {
        /// <summary>
        /// Get By id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<Contact?> GetAsync(Guid id);

        /// <summary>
        /// Get by query
        /// </summary>
        /// <param name="query">query</param>
        /// <returns></returns>
        Task<IEnumerable<Contact>> GetAsync(string? query);

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <param name="contact">Contact entity</param>
        /// <returns></returns>
        Task Create(Contact contact);

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task Delete(Guid id);

        /// <summary>
        /// Update contact entity
        /// </summary>
        /// <param name="contact">Contact entity</param>
        /// <returns></returns>
        Task Update(Contact contact);
    }
}
