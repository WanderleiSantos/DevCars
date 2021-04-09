using Dapper;
using DevCars.API.Entities;
using DevCars.API.Entities.Enums;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionString;

        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevCarsCs");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cars = _dbContext.Cars;
            /*var carsViewModel = cars
                .Where(c => c.Status == CarStatusEnum.Available)
                .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
                .ToList();*/
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Brand, Model, Price FROM Cars WHERE Status = 0";
                var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);

                return Ok(carsViewModel);
            }            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            var carDetailsViewModel = new CarDetailsViewModel(car.Brand, car.Model, car.VinCode, car.Year, car.Color, car.Price, car.ProductionDate);
            return Ok(carDetailsViewModel);
        }

        /// <summary>
        /// Cadastrar um Carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo
        /// {
        ///     "vinCode": "H1452020",
        ///     "brand": "Honda",
        ///     "model": "Civic",
        ///     "year": 2020,
        ///     "price": 98000,
        ///     "color": "Azul",
        ///     "productionDate": "2020-01-01"
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo carro</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50)
            {
                return BadRequest("Modelo não pode ter mais de 50 caracteres");
            }
            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            car.Update(model.Color, model.Price);

            //_dbContext.SaveChanges();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";
                sqlConnection.Execute(query, new { color = car.Color, price = car.Price, id = car.Id });                
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            car.SetAsSuspended();
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
