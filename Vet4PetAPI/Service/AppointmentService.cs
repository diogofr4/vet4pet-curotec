using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Repository.Interfaces;
using Service.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                _unitOfWork.Appointments.Remove(appointment);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResponse<Appointment>> GetAppointmentsByAnimalAsync(int animalId, int page, int pageSize)
        {
            var query = _unitOfWork.Appointments.GetQueryable()
                .Where(a => a.AnimalId == animalId)
                .Include(a => a.Vet)
                .Include(a => a.Owner)
                .OrderByDescending(a => a.Date);

            var totalCount = await query.CountAsync();
            var totalPages = (int)System.Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<Appointment>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
} 