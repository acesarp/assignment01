using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment01_ASP.Models {
    public class AppUser : IdentityUser {

        public AppUser() { }
        public AppUser(string userName, 
                        string email, 
                        long mobileNumber, 
                        string password, 
                        string firstName, 
                        string lastName, 
                        string street, 
                        string city, 
                        string province, 
                        string postalCode,
                        string country) : base() {

            UserName = userName;
            base.Email = email;
            MobileNumber = mobileNumber;     
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Country = country;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override string UserName { get; set; }

        [Range(1111111111, 9999999999)]
        public long MobileNumber { get; set; }

        [MinLength(8)]
        [ProtectedPersonalData]
        [Required]
        public string Password { get; set; }

        [MinLength(2)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(2)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [MinLength(4)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string Province { get; set; }

        [StringLength(6)]
        public string PostalCode { get; set; }

        [Required]
        [MinLength(3)]
        public string Country { get; set; }

        public bool IsAdmin { get; set; }
    }
}
