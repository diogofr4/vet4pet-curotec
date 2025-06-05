using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Message>> GetMessagesByAppointmentIdAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            return appointment?.Messages ?? new List<Message>();
        }

        public async Task<Message> SendMessageAsync(Message message)
        {
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return message;
        }
    }
} 