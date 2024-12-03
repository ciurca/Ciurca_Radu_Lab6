using Grpc.Net.Client;
using GrpcCustomersService;
using LibraryModel.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ciurca_Radu_Lab2.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public CustomersGrpcController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:7204");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            CustomerList cust = client.GetAll(new Empty());
            return View(cust);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(GrpcCustomersService.Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new
                CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Insert(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public IActionResult Edit(int id)
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            GrpcCustomersService.Customer customer = client.Get(new CustomerId() { Id = id });
            var mappedCustomer = new LibraryModel.Models.Customer
            {
                CustomerID = customer.CustomerId,
                Name = customer.Name,
                Adress = customer.Adress,
                BirthDate = DateTime.Parse(customer.Birthdate)

            };
            return View(mappedCustomer);
        }
        [HttpPost]
        public IActionResult Edit(GrpcCustomersService.Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new
                CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Update(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new CustomerService.CustomerServiceClient(channel);
            client.Delete(new CustomerId() { Id = id});
            return View();
        }
    }
    }
