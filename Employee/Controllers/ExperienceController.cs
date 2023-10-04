using Employees.Abstractions;
using Employees.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Employees.Controllers
{
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly IExperienceService _experienceService;
        private readonly ILanguageService _languageService;
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;

        public ExperienceController(IExperienceService experienceService, ILanguageService languageService, IEmployeeService employeeService, IUserService userService)
        {
            _experienceService = experienceService;
            _languageService = languageService;
            _employeeService = employeeService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _experienceService.Get());
        }

        public async Task<IActionResult> Details(int id)
        {
            var experience = await _experienceService.GetById(id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        public async Task<IActionResult> Add()
        {
            return View(new ExperienceDto
            {
                Languages = await _languageService.Get(),
                Employees = await _employeeService.Get()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ExperienceDto model)
        {
            if (ModelState.IsValid)
            {
                await _experienceService.Add(model);
                var user = User.FindFirstValue("name");
                if (user != null)
                    await _userService.UpdateAction(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var experience = await _experienceService.GetById(id);
            if (experience == null)
            {
                return NotFound();
            }
            experience.Employees = await _employeeService.Get();
            experience.Languages = await _languageService.Get();
            return View(experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExperienceDto model)
        {
            if (ModelState.IsValid)
            {
                await _experienceService.Update(model);
                var user = User.FindFirstValue("name");
                if (user != null)
                    await _userService.UpdateAction(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _experienceService.Delete(id);
            var user = User.FindFirstValue("name");
            if (user != null)
                await _userService.UpdateAction(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
