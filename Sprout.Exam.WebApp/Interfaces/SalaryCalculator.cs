

using Sprout.Exam.Business.DataTransferObjects;

namespace Sprout.Exam.WebApp.Interfaces{
    public interface SalaryCalculator
    {
        decimal CalculateSalary(SalaryCalculationDto request);
    }
}
