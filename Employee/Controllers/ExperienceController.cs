using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employees.Data;
using Employees.Data.Entities;
using Employees.Abstractions;
using Employees.Shared.Models;
using Employees.Services;
using Microsoft.AspNetCore.Authorization;

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
                Employees = await _employeeService.GetNames()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ExperienceDto model)
        {
            if (ModelState.IsValid)
            {
                await _experienceService.Add(model);
                var user = User.Claims.FirstOrDefault()?.Value;
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
            experience.Employees = await _employeeService.GetNames();
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
                var user = User.Claims.FirstOrDefault()?.Value;
                if (user != null)
                    await _userService.UpdateAction(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _experienceService.Delete(id);
            var user = User.Claims.FirstOrDefault()?.Value;
            if (user != null)
                await _userService.UpdateAction(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
