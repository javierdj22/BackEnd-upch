using BackEndUpch.Data;
using BackEndUpch.Domain;
using BackEndUpch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEndUpch.Infrastructure
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
            return await _context.Cars.ToListAsync();
        }
        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

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
            return car; // ✅ Devuelve el auto agregado
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
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return true; 
            }
            return false; 
        }

    }
}
