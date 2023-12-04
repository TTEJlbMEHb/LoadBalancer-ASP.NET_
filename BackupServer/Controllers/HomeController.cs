using LoadBalancer.BLL.Interfaces;
using LoadBalancer.Domain.Entity;
using LoadBalancer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoadBalancer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMatrixService _matrixService;

        public HomeController(ILogger<HomeController> logger, IMatrixService matrixService)
        {
            _logger = logger;
            _matrixService = matrixService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string UserEmail = GetUserUserEmail();
            User user = new User { Email = UserEmail };

            var response = await _matrixService.GetTasks(user);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }
        private string GetUserUserEmail()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var UserEmail = User.Identity.Name;

                if (UserEmail != null)
                {
                    return UserEmail;
                }
            }

            return 0.ToString();
        }

        public async Task<IActionResult> Delete(Guid guid)
        {
            var response = await _matrixService.DeleteTask(guid);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> TaskResult(Guid guid)
        {
            var response = await _matrixService.GetTask(guid);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}