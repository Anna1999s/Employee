using System.ComponentModel.DataAnnotations;

namespace Employees.Data.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "Женский")]
        Female = 0,
        [Display(Name = "Мужской")]
        Male = 1
    }
}
