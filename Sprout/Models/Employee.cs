using System;

namespace Sprout.Models
{
    public enum EmployeeType
    {
        Regular,
        Contractual
    }

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string Tin { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
