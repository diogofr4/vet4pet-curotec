using System;

namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 