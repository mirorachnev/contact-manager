using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ContactManager.DataProvider.DbData
{
    /// <summary>
    /// Contact
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Contact id, primary key
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Id to be exposed
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// IBAN
        /// </summary>
        public string Iban { get; set; }
    }
}
