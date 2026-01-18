using System;
using VetClinicConsoleApp.Enums;
using VetClinicConsoleApp.Interfaces;

namespace VetClinicConsoleApp.Models
{
    public class Dog : IAnimal
    {
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Breed { get; private set; }
        public string MedicalHistory { get; private set; }
        public double Weight { get; private set; }
        public string Color { get; private set; }
        public Status Status { get; set; }

        
        public Dog(string name, DateTime birthday, string breed, string medicalHistory, double weight, string color)
        {
            Name = name;
            Birthday = birthday;
            Breed = breed;
            MedicalHistory = medicalHistory;
            Weight = weight;
            Color = color;
            Status = Status.NotAdopted;
        }

        public void Adopt()
        {
            Status = Status.Adopted;
        }

        public override string ToString()
        {
            return $"[Dog] {Name} ({Breed})";
        }
    }
}