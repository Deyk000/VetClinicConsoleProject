using System;
using VetClinicConsoleApp.Enums;

namespace VetClinicConsoleApp.Interfaces
{
    public interface IAnimal
    {
        string Name { get; }
        DateTime Birthday { get; }
        string Breed { get; }
        string MedicalHistory { get; }
        double Weight { get; }
        string Color { get; }
        Status Status { get; set; }

        void Adopt();
    }
}