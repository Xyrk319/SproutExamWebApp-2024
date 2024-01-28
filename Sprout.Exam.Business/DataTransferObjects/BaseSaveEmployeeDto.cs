using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public abstract class BaseSaveEmployeeDto
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FullName { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}-\d{3}$", ErrorMessage = "Invalid TIN format. Format should be XXX-XXX-XXX-XXX.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime? Birthdate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int EmployeeTypeId { get; set; }
    }
}
