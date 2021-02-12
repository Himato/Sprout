using System;
using NUnit.Framework;
using Sprout.Models;
using Sprout.Models.Dtos;
using Sprout.Services;

namespace UnitTests
{
    public class FactoryTests
    {
        [Test]
        public void RegularTypeEmployeeFactoryTest()
        {
            const float expected = 16_690.91f;

            var actual = CalculateSalary(new SalaryDto
            {
                EmployeeType = EmployeeType.Regular,
                Salary       = 20_000,
                Days         = 1,
                Tax          = 12
            });

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContractualTypeEmployeeFactoryTest()
        {
            const float expected = 7_750f;

            var actual = CalculateSalary(new SalaryDto
            {
                EmployeeType = EmployeeType.Contractual,
                Salary       = 500,
                Days         = 15.5f
            });

            Assert.AreEqual(expected, actual);
        }

        private static float CalculateSalary(SalaryDto salaryDto)
        {
            SalaryFactory factory = salaryDto.EmployeeType switch
            {
                EmployeeType.Regular     => new RegularEmployeeSalaryFactory(),
                EmployeeType.Contractual => new ContractualEmployeeSalaryFactory(),
                _                        => throw new ArgumentOutOfRangeException()
            };

            return factory.CalculateSalary(salaryDto);
        }
    }
}
