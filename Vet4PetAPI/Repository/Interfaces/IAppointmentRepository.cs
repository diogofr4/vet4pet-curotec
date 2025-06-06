using Domain;
using System.Linq;

namespace Repository.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IQueryable<Appointment> GetQueryable();
        // Add appointment-specific methods here if needed
    }
} 