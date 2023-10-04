using Employees.Abstractions;
using Employees.Data;
using Employees.Data.Entities;
using Employees.Infrastructure;
using Employees.Middlewares;
using Employees.Repositories.Base;
using Employees.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionString = config.GetConnectionString("DefaultConnection");

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies();
});

services.AddControllersWithViews();
//services.AddEndpointsApiExplorer();
//services.AddSwaggerGen();

builder.Services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddTransient<IBaseRepository<Department>, BaseRepository<Department>>();
builder.Services.AddTransient<IBaseRepository<Employee>, BaseRepository<Employee>>();
builder.Services.AddTransient<IBaseRepository<Experience>, BaseRepository<Experience>>();
builder.Services.AddTransient<IBaseRepository<Language>, BaseRepository<Language>>();

services.AddTransient<IUserService, UserService>();
services.AddTransient<ILanguageService, LanguageService>();
services.AddTransient<IDepartmentService, DepartmentService>();
services.AddTransient<IEmployeeService, EmployeeService>();
services.AddTransient<IExperienceService, ExperienceService>();
services.AddRazorPages();

services.AddAutoMapper(typeof(AutoMapping));

services.AddAuthentication("BasicAuthentication").
            AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
            ("BasicAuthentication", null, null);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servicesProvider = scope.ServiceProvider;

    SeedData.Initialize(servicesProvider);
}

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
