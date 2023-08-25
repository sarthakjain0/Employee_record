using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Employee
    {
        public int Id { get; set; }


        [RegularExpression(@"(.*\.png|.*\.jpg|.*\.jpeg)$", ErrorMessage = "Profile image must be in JPG ,JPEG or PNG format.")]
        public byte[] ProfileImage { get; set; }
        //[Required]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only alphabetic characters.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string LastName { get; set; }

        //[Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits.")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "invalid email format.")]
        public string Email { get; set; }

        [ReadOnly(true)]
        public int Age { get; set; }
        public List<EducationDetails> EducationDetails { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    // Custom validation attribute for DateOfBirth
    public class CustomDateOfBirthAttribute : RangeAttribute
    {
        public CustomDateOfBirthAttribute() : base(typeof(DateTime), DateTime.Now.AddYears(-100).ToShortDateString(), DateTime.Now.AddYears(-18).ToShortDateString())
        {
        }
    }
    public class EducationDetails
    {
        public int Id { get; set; } // Primary key
        public int EmployeeId { get; set; } // Foreign key to Employee
        public string Degree { get; set; }
        public int PassingYear { get; set; }
        public decimal Percentage { get; set; }

    }

}
