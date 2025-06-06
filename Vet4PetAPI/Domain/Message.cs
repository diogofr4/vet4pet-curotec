using System;

namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 