using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Service.Interfaces
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAllAnimalsAsync();
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<Animal> CreateAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(int id);
        Task<IEnumerable<Animal>> GetAnimalsByVetIdAsync(int vetId);
    }
} 