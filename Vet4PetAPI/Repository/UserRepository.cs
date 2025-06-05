using Domain;
using Infrastructure;
using Repository.Interfaces;

namespace Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Vet4PetDbContext context) : base(context) { }
        // Add user-specific methods here if needed
    }
} 