using System.ComponentModel.DataAnnotations;

namespace Sprout.Models.Dtos
{
    public class SalaryDto
    {
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public int? Salary { get; set; }

        [Required]
        [Range(0, 22)]
        public float? Days { get; set; }

        [Required]
        [Range(0, 99)]
        public float? Tax { get; set; }
    }
}
