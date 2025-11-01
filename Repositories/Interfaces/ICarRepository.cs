using BackEndUpch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndUpch.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task<Car> AddAsync(CreateCarDto car);
        Task<Car> UpdateAsync(Car car);
        Task<bool> DeleteAsync(int id);
    }
}
