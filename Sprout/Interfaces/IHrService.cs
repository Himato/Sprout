using System.Threading.Tasks;
using Sprout.Models.Dtos;

namespace Sprout.Interfaces
{
    public interface IHrService
    {
        Task<float> CalculateSalary(int employeeId, SalaryDto salaryDto);
        Task<bool> ValidateTin(int? id, string tin);
    }
}
