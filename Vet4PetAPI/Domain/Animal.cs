namespace Domain
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public int? VetId { get; set; }
        public User Vet { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
} 