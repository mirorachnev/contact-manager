namespace ContactManager.Api.Contracts
{
    /// <summary>
    /// Contact without id
    /// </summary>
    public class ContactBase
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime DateOfBirth { get; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// IBAN
        /// </summary>
        public string Iban { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="dateOfBirth">Date of Birth</param>
        /// <param name="address">Address</param>
        /// <param name="iban">IBAN</param>
        public ContactBase(string firstName, string lastName, string email,
            string phoneNumber, DateTime dateOfBirth, string address, string iban)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Address = address;
            Iban = iban;
        }
    }
}
