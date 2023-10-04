using Microsoft.AspNetCore.Mvc;
using Employees.Abstractions;
using Employees.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Employees.Controllers
{
    [Authorize]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly IUserService _userService;

        public LanguageController(ILanguageService languageService, IUserService userService)
        {
            _languageService = languageService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _languageService.Get());
        }

        public async Task<IActionResult> Details(int id)
        {
            var language = await _languageService.GetById(id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(LanguageDto model)
        {
            if (ModelState.IsValid)
            {
                await _languageService.Add(model);
                var user = User.FindFirstValue("name");
                if (user != null)
                    await _userService.UpdateAction(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var language = await _languageService.GetById(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LanguageDto model)
        {
            if (ModelState.IsValid)
            {
                await _languageService.Update(model);
                var user = User.FindFirstValue("name");
                if (user != null)
                    await _userService.UpdateAction(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _languageService.Delete(id);
            var user = User.FindFirstValue("name");
            if (user != null)
                await _userService.UpdateAction(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
