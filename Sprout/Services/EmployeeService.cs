using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sprout.Common;
using Sprout.Interfaces;
using Sprout.Models;
using Sprout.Models.Dtos;

namespace Sprout.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DbContext _context;

        public EmployeeService(DbContext context) => _context = context;

        public Task<List<Employee>> GetEmployees() => Task.FromResult(_context.Employees.AsNoTracking().ToList());

        public Task<Employee> GetEmployeeById(int id)
        {
            var employee = _context.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (employee is null) throw new ArgumentException("Employee not found");

            return Task.FromResult(employee);
        }

        public async Task AddEmployee(EmployeeDto employeeDto)
        {
            var count = _context.Employees.Count;

            var employee = new Employee
            {
                Id           = count + 1,
                Name         = employeeDto.Name,
                Birthdate    = (DateTime) employeeDto.Birthdate!,
                Tin          = employeeDto.Tin,
                EmployeeType = employeeDto.EmployeeType
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee is null) throw new ArgumentException("Employee not found");

            employee.Name         = employeeDto.Name;
            employee.Birthdate    = (DateTime) employeeDto.Birthdate!;
            employee.Tin          = employeeDto.Tin;
            employee.EmployeeType = employeeDto.EmployeeType;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee is null) throw new ArgumentException("Employee not found");

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
        }
    }
}
