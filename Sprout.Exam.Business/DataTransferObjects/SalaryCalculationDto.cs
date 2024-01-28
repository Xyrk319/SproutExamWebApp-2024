using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class SalaryCalculationDto
    {
        public int Id { get;}
        public decimal AbsentDays { get; set; }
        public decimal WorkedDays { get; set; }
    }
}


