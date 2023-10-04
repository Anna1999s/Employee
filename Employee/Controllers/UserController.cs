using Employees.Abstractions;
using Employees.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.Get();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserDto model)
        {
            if (ModelState.IsValid)
            {
                var users = await _userService.Get();
                if (users.Any(_ => _.Login == model.Login))
                {
                    ModelState.AddModelError("Login", "Пользователь с таким логином уже харегистрирован!");
                }
                else
                {
                    await _userService.Add(model);

                    var user = User.Claims.FirstOrDefault()?.Value;
                    if (user != null)
                        await _userService.UpdateAction(user);

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto model)
        {
            if (ModelState.IsValid)
            {
                await _userService.Update(model);

                var user = User.Claims.FirstOrDefault()?.Value;
                if (user != null)
                    await _userService.UpdateAction(user);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);

            var user = User.Claims.FirstOrDefault()?.Value;
            if (user != null)
                await _userService.UpdateAction(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
