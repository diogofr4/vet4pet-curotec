using System.Threading.Tasks;
using Repository.Interfaces;
using Infrastructure;
using Domain;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Vet4PetDbContext _context;
        public IUserRepository Users { get; }
        public IAnimalRepository Animals { get; }
        public IAppointmentRepository Appointments { get; }
        public IMessageRepository Messages { get; }

        public UnitOfWork(Vet4PetDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Animals = new AnimalRepository(context);
            Appointments = new AppointmentRepository(context);
            Messages = new MessageRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 