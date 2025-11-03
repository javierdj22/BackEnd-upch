using BackEndUpch.Domain;
using BackEndUpch.Domain.Interfaces;
using BackEndUpch.Services.Interfaces;

namespace BackEndUpch.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Car>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Car?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task<Car> CreateAsync(CreateCarDto Car) => await _repository.AddAsync(Car);
        public async Task<Car> UpdateAsync(Car Car) => await _repository.UpdateAsync(Car);
        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
