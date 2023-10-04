using Employees.Data.Enums;

namespace Employees.Data.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
