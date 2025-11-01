using BackEndUpch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndUpch.Services.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task<Car> CreateAsync(CreateCarDto car);
        Task<Car> UpdateAsync(Car car);
        Task<bool> DeleteAsync(int id);
    }
}
