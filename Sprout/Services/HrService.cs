using System;
using System.Linq;
using System.Threading.Tasks;
using Sprout.Interfaces;
using Sprout.Models;
using Sprout.Models.Dtos;

namespace Sprout.Services
{
    public class HrService : IHrService
    {
        private readonly DbContext _context;

        public HrService(DbContext context) => _context = context;

        public Task<float> CalculateSalary(int employeeId, SalaryDto salaryDto)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee is null) throw new ArgumentException("Employee not found");

            SalaryFactory factory = employee.EmployeeType switch
            {
                EmployeeType.Regular     => new RegularEmployeeSalaryFactory(),
                EmployeeType.Contractual => new ContractualEmployeeSalaryFactory(),
                _                        => throw new ArgumentOutOfRangeException()
            };

            return Task.FromResult(factory.CalculateSalary(salaryDto));
        }

        public Task<bool> ValidateTin(int? id, string tin)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Tin == tin);

            // Validate when either the employee doesn't exist, or if it does, it's the same employee
            return Task.FromResult(employee is null || (id is not null && employee.Id == (int) id));
        }
    }

    public abstract class SalaryFactory
    {
        public abstract float CalculateSalary(SalaryDto salaryDto);
    }

    public class RegularEmployeeSalaryFactory : SalaryFactory
    {
        public override float CalculateSalary(SalaryDto salaryDto)
        {
            var salary                = (float) salaryDto.Salary!;
            var perDaySalary          = salary / 22;
            var totalAbsenceDeduction = perDaySalary * salaryDto.Days;
            var totalTaxes            = salary * (salaryDto.Tax / 100);
            var totalDeduction        = totalAbsenceDeduction + totalTaxes;
            return (float) Math.Round(salary - (float) totalDeduction!, 2);
        }
    }

    public class ContractualEmployeeSalaryFactory : SalaryFactory
    {
        public override float CalculateSalary(SalaryDto salaryDto)
        {
            var perDaySalary = (float) salaryDto.Salary!;
            var workingDays  = (float) salaryDto.Days!;
            return (float) Math.Round(perDaySalary * workingDays, 2);
        }
    }
}
