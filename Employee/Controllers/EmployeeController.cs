using Employees.Abstractions;
using Employees.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILanguageService _languageService;
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;

        public EmployeeController(IDepartmentService departmentService, ILanguageService languageService, IEmployeeService employeeService, IUserService userService)
        {
            _departmentService = departmentService;
            _languageService = languageService;
            _employeeService = employeeService;
            _userService = userService;
        }
        public async Task<IActionResult> Index(string filter)
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            TempData["Filter"] = filter;
            var res = await _employeeService.Get(filter);
            if (user != null)
                await _userService.UpdateAction(user);
            return View(res);

        }

        public async Task<IActionResult> GetNames(string term)
        {
            var result = await _employeeService.Get();
            var names = result.Where(_ => _.Name.Contains(term)).Select(_ => _.Name).Distinct().ToList();
            return Json(names);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            if (user != null)
                await _userService.UpdateAction(user);
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> Add()
        {
            return View(new EmployeeDto
            {
                Departments = await _departmentService.Get()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Add(model);
                return RedirectToAction(nameof(Index));
            }
            var user = User.Claims.FirstOrDefault()?.Value;
            if (user != null)
                await _userService.UpdateAction(user);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Departments = await _departmentService.Get();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Update(model);
                return RedirectToAction(nameof(Index));
            }

            var user = User.Claims.FirstOrDefault()?.Value;
            if (user != null)
                await _userService.UpdateAction(user);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.Delete(id);

            var user = User.Claims.FirstOrDefault()?.Value;
            if (user != null)
                await _userService.UpdateAction(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
