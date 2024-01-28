using System;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Interfaces;
using Sprout.Exam.WebApp.Services;

namespace Sprout.Exam.WebApp.Factories
{
    public class SalaryCalculatorFactory
    {
        public SalaryCalculator GetCalculator(EmployeeType type)
        {
            return type switch
            {
                EmployeeType.Regular => new RegularEmployeeSalaryCalculator(),
                EmployeeType.Contractual => new ContractualEmployeeSalaryCalculator(),
                _ => throw new ArgumentException("Invalid employee type"),
            };
        }
    }
}