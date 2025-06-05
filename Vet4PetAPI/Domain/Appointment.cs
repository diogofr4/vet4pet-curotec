using System;
using System.Collections.Generic;

namespace Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int VetId { get; set; }
        public User Vet { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
} 