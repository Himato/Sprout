using System.Collections.Generic;
using System.Threading.Tasks;
using Sprout.Models;

namespace Sprout.Services
{
    public class DbContext
    {
        public List<Employee> Employees { get; } = new List<Employee>();

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}
