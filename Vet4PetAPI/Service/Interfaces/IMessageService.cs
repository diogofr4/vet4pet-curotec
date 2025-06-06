using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Service.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task<IEnumerable<Message>> GetMessagesByAnimalAsync(int animalId);
        Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(int senderId, int receiverId, int animalId);
        Task<Message> SendMessageAsync(Message message);
        Task UpdateMessageAsync(Message message);
        Task DeleteMessageAsync(int id);
    }
} 