using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _unitOfWork.Appointments.GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _unitOfWork.Appointments.GetByIdAsync(id);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment != null)
            {
                _unitOfWork.Appointments.Delete(appointment);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
} 