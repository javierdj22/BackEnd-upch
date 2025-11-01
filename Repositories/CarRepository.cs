using BackEndUpch.Data;
using BackEndUpch.Models;
using BackEndUpch.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEndUpch.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarsDbContext _context;

        public CarRepository(CarsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            try
            {
                var cars = await _context.Cars.ToListAsync();
                Console.WriteLine($"Conectado correctamente. Total de autos: {cars.Count}");
                return cars;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                return Enumerable.Empty<Car>();
            }
        }


        public async Task<Car?> GetByIdAsync(int id) => await _context.Cars.FindAsync(id);

        public async Task<Car> AddAsync(CreateCarDto carDto)
        {
            var car = new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Year = carDto.Year,
                Type = carDto.Type,
                Seats = carDto.Seats,
                Color = carDto.Color,
                Notes = carDto.Notes,
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
