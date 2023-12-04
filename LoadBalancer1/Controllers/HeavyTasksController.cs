using Hangfire;
using LoadBalancer.BLL.Interfaces;
using LoadBalancer.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoadBalancer.Controllers
{
    public class HeavyTasksController : Controller
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IMatrixService _matrixService;

        public HeavyTasksController(ICalculatorService calculatorService, IMatrixService matrixService)
        {
            _calculatorService = calculatorService;
            _matrixService = matrixService;
        }

        public IActionResult Calculator()
        {
            return View(new Calculator());
        }

        [HttpPost]
        public async Task<IActionResult> Calculator(Calculator calculator, string action)
        {
            var response = await _calculatorService.PerformCalculation(calculator, action);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                calculator.Result = response.Data.Result;
                return View("Calculator", calculator);
            }
            return RedirectToAction("Error");
            
        }

        public IActionResult Matrix()
        {
            return View(new Matrix());
        }

        [HttpPost]
        public async Task<IActionResult> Matrix(Matrix matrix)
        {
            string UserEmail = GetUserUserEmail();
            User user = new User { Email = UserEmail };

            BackgroundJob.Enqueue(() => _matrixService.GenerateRandomMatrix(matrix, user));
            return RedirectToAction("Index", "Home");

            //var response = await _matrixService.GenerateRandomMatrix(matrix, user);           
            //if (response.StatusCode == Domain.Enum.StatusCode.OK)
            //{
            //    return View("Matrix", response.Data);
            //}

            //return RedirectToAction("Error");
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


    }
}
