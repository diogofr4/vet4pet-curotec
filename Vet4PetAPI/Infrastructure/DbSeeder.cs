using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class DbSeeder
    {
        public static void Seed(Vet4PetDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var vet = new User { Name = "Dr. John Doe", Email = "vet1@vet4pet.com", PasswordHash = "vetpass", Role = UserRole.Vet };
                var owner = new User { Name = "Jane Smith", Email = "owner1@vet4pet.com", PasswordHash = "ownerpass", Role = UserRole.Owner };
                context.Users.AddRange(vet, owner);
                context.SaveChanges();

                var animal = new Animal { Name = "Buddy", Species = "Dog", Breed = "Labrador", Age = 3, OwnerId = owner.Id };
                context.Animals.Add(animal);
                context.SaveChanges();
            }
        }
    }
} 