using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Service.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesByAppointmentIdAsync(int appointmentId);
        Task<Message> SendMessageAsync(Message message);
    }
} 