using Domain;
using Infrastructure;
using Repository.Interfaces;
using System.Linq;

namespace Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly Vet4PetDbContext _context;
        public AppointmentRepository(Vet4PetDbContext context) : base(context) 
        {
            _context = context;
        }

        public IQueryable<Appointment> GetQueryable()
        {
            return _context.Appointments.AsQueryable();
        }
        // Add appointment-specific methods here if needed
    }
} 