namespace ContactManager.Api.Contracts
{
    /// <summary>
    /// Contact
    /// </summary>
    public class Contact : ContactBase
    {
        /// <summary>
        /// Id to be exposed
        /// </summary>
        public Guid Id { get; }        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id to be exposed</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="dateOfBirth">Date of Birth</param>
        /// <param name="address">Address</param>
        /// <param name="iban">IBAN</param>
        public Contact(Guid id, string firstName, string lastName, string email,
            string phoneNumber, DateTime dateOfBirth, string address, string iban)
            : base(firstName, lastName, email, phoneNumber, dateOfBirth, address, iban)
        {
            Id = id;
        }
    }
}
