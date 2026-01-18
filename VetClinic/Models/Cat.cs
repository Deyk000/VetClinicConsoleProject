using System;
using VetClinicConsoleApp.Enums;
using VetClinicConsoleApp.Interfaces;

namespace VetClinicConsoleApp.Models
{
    public class Cat : IAnimal
    {
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Breed { get; private set; }
        public string MedicalHistory { get; private set; }
        public double Weight { get; private set; }
        public string Color { get; private set; }
        public string FavoriteToy { get; private set; }
        public Status Status { get; set; }

        
        public Cat(string name, DateTime birthday, string breed, string medicalHistory, double weight, string color, string favoriteToy)
        {
            Name = name;
            Birthday = birthday;
            Breed = breed;
            MedicalHistory = medicalHistory;
            Weight = weight;
            Color = color;
            FavoriteToy = favoriteToy;
            Status = Status.NotAdopted;
        }

        public void Adopt()
        {
            Status = Status.Adopted;
        }

        public override string ToString()
        {
            return $"[Cat] {Name} ({Breed})";
        }
    }
}