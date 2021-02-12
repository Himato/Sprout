using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sprout.Interfaces;
using Sprout.Models.Dtos;
using Sprout.Models.ViewModels;

namespace Sprout.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHrService       _hrService;

        public EmployeesController(IEmployeeService employeeService, IHrService hrService)
        {
            _employeeService = employeeService;
            _hrService       = hrService;
        }

        public async Task<IActionResult> Index()
            => View(new EmployeesViewModel
            {
                Employees = await _employeeService.GetEmployees()
            });

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeDto employeeDto)
        {
            var result = await Execute(async () => await _employeeService.AddEmployee(employeeDto));

            if (result.Failed) return View();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await Execute(async () => await _employeeService.GetEmployeeById(id));

            if (!result.Failed) return View(EmployeeDto.Create(result.Output));

            TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeDto employeeDto)
        {
            var result = await Execute(async () => await _employeeService.UpdateEmployee(id, employeeDto));

            if (result.Failed) return View();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CalculateSalary(int id)
        {
            var result = await Execute(async () => await _employeeService.GetEmployeeById(id));

            if (!result.Failed)
            {
                var model = new SalaryDto
                {
                    EmployeeType = result.Output.EmployeeType
                };

                return View(model);
            }

            TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CalculateSalary(int id, SalaryDto salaryDto)
        {
            var result = await Execute(async () => await _hrService.CalculateSalary(id, salaryDto));

            if (result.Failed) return View(salaryDto);

            ViewData["TotalSalary"] = result.Output;

            return View(salaryDto);
        }

        [HttpGet]
        public async Task<IActionResult> ValidateTin(int? id, string tin) => Json(await _hrService.ValidateTin(id, tin));

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Execute(async () => await _employeeService.DeleteEmployee(id));

            return result.ToJsonResult();
        }
    }
}
