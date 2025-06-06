using Domain;
using Infrastructure;
using Repository.Interfaces;

namespace Repository
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(Vet4PetDbContext context) : base(context) { }
        // Add animal-specific methods here if needed
    }
} 