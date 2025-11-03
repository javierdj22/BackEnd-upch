using BackEndUpch.Domain;
using BackEndUpch.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(new { success = true, data = cars });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null)
                return NotFound(new { success = false, message = $"No se encontró el auto con ID {id}." });

            return Ok(new { success = true, data = car });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarDto car)
        {
            var newCar = await _carService.CreateAsync(car);
            return CreatedAtAction(nameof(GetById), new { id = newCar.Id }, new { success = true, data = newCar });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Car car)
        {
            if (id != car.Id)
                return BadRequest(new { success = false, message = "El ID de la ruta no coincide con el del cuerpo." });

            var updatedCar = await _carService.UpdateAsync(car);
            return Ok(new { success = true, data = updatedCar });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _carService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { success = false, message = $"No se encontró el auto con ID {id} para eliminar." });

            return Ok(new { success = true, message = "Auto eliminado correctamente." });
        }
    }
}
