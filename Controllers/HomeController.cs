using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Odev1_Northwnd_Project.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Odev1_Northwnd_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwndContext _northwndContext;


        public HomeController(ILogger<HomeController> logger, NorthwndContext northwndContext)
        {
            _logger = logger;
            _northwndContext = northwndContext;
        }

        public IActionResult Index()
        {


            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _northwndContext.Employees
                .OrderBy(e=>e.EmployeeId)
    .Select(e => new
    {
        EmployeeId = e.EmployeeId,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Title = e.Title
    })
    .ToListAsync();

            return Json(employees);
        }

        [HttpPost]
        public async Task<IActionResult> GetOrdersByEmployeeId(int employeeId)
        {
            var orders = await _northwndContext.Orders
                .Where(o=>o.EmployeeId == employeeId)
    .Select(e => new
    {

        OrderId= e.OrderId,
        ShippedDate = e.ShippedDate,
        ShipCountry= e.ShipCountry,
    })
    .ToListAsync();

            return Json(orders);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult NewEmployee()
        {
            return View();
        }
            [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                _northwndContext.Add(employee);
                _northwndContext.SaveChanges();

                var response = new
                {
                    success = true,
                    message = "Veri başarıyla eklendi.",
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    message = "Veri eklerken hata oluştu: " + ex.Message
                };

                return Json(response);
            }
        }

    }
}