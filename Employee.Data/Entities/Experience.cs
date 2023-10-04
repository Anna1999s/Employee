
namespace Employees.Data.Entities
{
    public class Experience : BaseEntity
    {
        public int EmployeeId { get; set; } 
        public virtual Employee Employee { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
