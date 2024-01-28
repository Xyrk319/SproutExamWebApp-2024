using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Interfaces;

namespace Sprout.Exam.WebApp.Services
{
    public class ContractualEmployeeSalaryCalculator : SalaryCalculator
    {
        public decimal CalculateSalary(SalaryCalculationDto request)
        {
            decimal rate = 500;
            return rate * request.WorkedDays;
        }
    }
}