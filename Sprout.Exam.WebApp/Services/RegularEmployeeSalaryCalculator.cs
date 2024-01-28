using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Interfaces;

namespace Sprout.Exam.WebApp.Services
{
    public class RegularEmployeeSalaryCalculator : SalaryCalculator
    {
        public decimal CalculateSalary(SalaryCalculationDto request)
        {
            decimal basicSalary = 20000;
            decimal tax = 0.12m;
            decimal absentDeduction = basicSalary / 22 * request.AbsentDays;
            return basicSalary - absentDeduction - (basicSalary * tax);
        }
    }
}