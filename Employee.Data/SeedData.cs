using Employees.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<AppDbContext>();

            if (!context.Departments.Any())
                context.Departments.AddRange(
                    new Department
                    {
                        Name = "Отдел разработки",
                        Floor = 1
                    },
                    new Department
                    {
                        Name = "Отдел маркетинга",
                        Floor = 2
                    },
                    new Department
                    {
                        Name = "Отдел технической поддержки",
                        Floor = 3
                    },
                    new Department
                    {
                        Name = "Отдел продаж",
                        Floor = 4
                    },
                    new Department
                    {
                        Name = "Отдел финансов ",
                        Floor = 5
                    },
                    new Department
                    {
                        Name = "Отдел тестирования ",
                        Floor = 6
                    },
                    new Department
                    {
                        Name = "Отдел кадров ",
                        Floor = 7
                    }
                );

            if (!context.Languages.Any())
                context.Languages.AddRange(
                    new Language
                    {
                        Name = "C#",
                    },
                    new Language
                    {
                        Name = "JavaScript",
                    },
                    new Language
                    {
                        Name = "C++",
                    }
                );
            context.SaveChanges();
        }        
    }
}
