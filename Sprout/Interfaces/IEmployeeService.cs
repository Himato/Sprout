using System.Collections.Generic;
using System.Threading.Tasks;
using Sprout.Models;
using Sprout.Models.Dtos;

namespace Sprout.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task AddEmployee(EmployeeDto employeeDto);
        Task UpdateEmployee(int id, EmployeeDto employeeDto);
        Task DeleteEmployee(int id);
    }
}
