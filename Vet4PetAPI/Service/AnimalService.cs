using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnimalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            return await _unitOfWork.Animals.GetAllAsync();
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await _unitOfWork.Animals.GetByIdAsync(id);
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            await _unitOfWork.Animals.AddAsync(animal);
            await _unitOfWork.SaveChangesAsync();
            return animal;
        }

        public async Task UpdateAnimalAsync(Animal animal)
        {
            _unitOfWork.Animals.Update(animal);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAnimalAsync(int id)
        {
            var animal = await _unitOfWork.Animals.GetByIdAsync(id);
            if (animal != null)
            {
                _unitOfWork.Animals.Delete(animal);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
} 