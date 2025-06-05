using Domain;
using Infrastructure;
using Repository.Interfaces;

namespace Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(Vet4PetDbContext context) : base(context) { }
        // Add appointment-specific methods here if needed
    }
} 