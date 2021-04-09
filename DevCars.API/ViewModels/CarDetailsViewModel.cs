using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class CarDetailsViewModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VinCode { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; }

        public CarDetailsViewModel(string brand, string model, string vinCode, int year, string color, decimal price, DateTime productionDate)
        {
            Brand = brand;
            Model = model;
            VinCode = vinCode;
            Year = year;
            Color = color;
            Price = price;
            ProductionDate = productionDate;
        }
    }
}
