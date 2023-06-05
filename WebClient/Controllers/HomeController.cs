using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerRepository _customerRepository;
        public HomeController(ILogger<HomeController> logger, CustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            string token = User.FindFirst("AccessToken").Value;
            return View(_customerRepository.GetAllCustomer(token));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            string token = User.FindFirst("AccessToken").Value;
            _customerRepository.AddCustomer(customer , token);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            string token = User.FindFirst("AccessToken").Value;
            var customer = _customerRepository.GetCustomerById(id , token);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            string token = User.FindFirst("AccessToken").Value;
            _customerRepository.UpdateCustomer(customer, token);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            string token = User.FindFirst("AccessToken").Value;
            _customerRepository.DeleteCustomer(id, token);
            return RedirectToAction("Index");
        }

    }
}