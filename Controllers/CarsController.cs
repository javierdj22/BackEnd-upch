using Microsoft.AspNetCore.Mvc;
using BackEndUpch.Models;
using BackEndUpch.Services.Interfaces;

namespace BackEndUpch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] string? brand, [FromQuery] string? type, [FromQuery] int? year)
        //{
        //    var cars = await _carService.GetAllAsync();

        //    if (!string.IsNullOrEmpty(brand))
        //        cars = cars.Where(c => c.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (!string.IsNullOrEmpty(type))
        //        cars = cars.Where(c => c.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (year.HasValue)
        //        cars = cars.Where(c => c.Year == year.Value).ToList();

        //    return Ok(cars);
        //}
        [HttpGet]
        public IActionResult GetAll()
        {
            var cars = new[]
            {
                new { Id = 1, Brand = "Toyota", Model = "Corolla" },
                new { Id = 2, Brand = "Ford", Model = "Focus" }
            };
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            return car == null ? NotFound() : Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarDto car)
        {
            var newCar = await _carService.CreateAsync(car);
            return CreatedAtAction(nameof(GetById), new { id = newCar.Id }, newCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Car car)
        {
            if (id != car.Id) return BadRequest();
            var updatedCar = await _carService.UpdateAsync(car);
            return Ok(updatedCar);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _carService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
