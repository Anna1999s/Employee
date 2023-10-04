
namespace Employees.Shared.Models
{
    public class ExperienceDto
    {
        public int Id { get; set; }
        public EmployeeDto? Employee { get; set; }
        public int EmployeeId { get; set; }
        public LanguageDto? Language { get; set; }
        public int LanguageId { get; set; }

        public List<LanguageDto> Languages { get; set; } = new List<LanguageDto>();
        public List<EmployeeDto> Employees { get; set; }= new List<EmployeeDto>();
    }
}
