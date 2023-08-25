using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Education
    {

        public int Id { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Invalid passing year.")]
        public int PassingYear { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public float Percentage { get; set; }

        // Foreign key
        public int EmployeeId { get; set; }

        // Navigation property for Employee
        public virtual Employee Employee { get; set; }
    }
}