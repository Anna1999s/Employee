using Microsoft.AspNetCore.Mvc;
using Employees.Abstractions;
using Employees.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Employees.Services;
using System.Security.Claims;

namespace Employees.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        public DepartmentController(IDepartmentService departmentService, IUserService userService)
        {
            _departmentService = departmentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.Get());
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetById(id);

            var user = User.FindFirstValue("name");
            if (user != null)
                await _userService.UpdateAction(user);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);

                var user = User.FindFirstValue("name");
                if (user != null)
                    await _userService.UpdateAction(user);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Update(model);
                

                var user = User.FindFirstValue("name");
                if (user != null)
                    await _userService.UpdateAction(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.Delete(id);
            var user = User.FindFirstValue("name");
            if (user != null)
                await _userService.UpdateAction(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
