using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Service.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task<Message> SendMessageAsync(Message message);
        Task UpdateMessageAsync(Message message);
        Task DeleteMessageAsync(int id);
    }
} 