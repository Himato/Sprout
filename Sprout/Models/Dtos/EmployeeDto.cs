using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Sprout.Models.Dtos
{
    public class EmployeeDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }

        [Required]
        [StringLength(9)]
        [Remote("ValidateTin", "Employees", ErrorMessage = "This tin already exists", AdditionalFields = nameof(Id))]
        public string Tin { get; set; }

        [DisplayName("Employee Type")]
        public EmployeeType EmployeeType { get; set; }

        public static EmployeeDto Create(Employee employee)
            => new()
            {
                Id           = employee.Id,
                Name         = employee.Name,
                Birthdate    = employee.Birthdate,
                Tin          = employee.Tin,
                EmployeeType = employee.EmployeeType
            };
    }
}
