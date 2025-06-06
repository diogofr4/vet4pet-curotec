using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IAnimalRepository Animals { get; }
        IAppointmentRepository Appointments { get; }
        IMessageRepository Messages { get; }
        Task<int> SaveChangesAsync();
    }
} 