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

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _unitOfWork.Messages.GetAllAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _unitOfWork.Messages.GetByIdAsync(id);
        }

        public async Task<Message> SendMessageAsync(Message message)
        {
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return message;
        }

        public async Task UpdateMessageAsync(Message message)
        {
            _unitOfWork.Messages.Update(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);
            if (message != null)
            {
                _unitOfWork.Messages.Delete(message);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
} 