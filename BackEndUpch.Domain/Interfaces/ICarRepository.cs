namespace BackEndUpch.Domain.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task<Car> AddAsync(CreateCarDto car);
        Task<Car> UpdateAsync(Car car);
        Task<bool> DeleteAsync(int id);
    }
}
