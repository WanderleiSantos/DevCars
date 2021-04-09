using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {

        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList();
            
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);
            
            var order = new Order( model.IdCar, model.IdCustomer, car.Price, extraItems);

            //var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCustomer);

            //customer.Purchase(order);
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.IdCustomer, orderId = order.Id}, model);
        }

        [HttpGet("{id}/orders/{orderId}")]
        public IActionResult GetOrder(int id, int orderId)
        {            
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var extraItems = order.ExtraItems
                .Select(e => e.Description)
                .ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            return Ok();

        }
    }
}
