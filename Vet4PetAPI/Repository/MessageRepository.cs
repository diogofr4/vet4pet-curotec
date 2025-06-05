using Domain;
using Infrastructure;
using Repository.Interfaces;

namespace Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(Vet4PetDbContext context) : base(context) { }
        // Add message-specific methods here if needed
    }
} 