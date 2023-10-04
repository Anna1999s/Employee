using Employees.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Shared.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public DepartmentDto? Department { get; set; }
        public int DepartmentId { get; set; }

        [NotMapped]
        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();
    }
}
