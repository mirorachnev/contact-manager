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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ContactId { get; set; }

        /// <summary>
        /// Id to be exposed
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [MaxLength(25)]
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// IBAN
        /// </summary>
        [MaxLength(34)]
        [Required]
        public string Iban { get; set; }
    }
}
