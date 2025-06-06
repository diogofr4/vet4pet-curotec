using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Animal> Animals { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
} 