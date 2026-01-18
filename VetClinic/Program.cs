using System;
using System.Collections.Generic;
using VetClinicConsoleApp.Enums;
using VetClinicConsoleApp.Interfaces;
using VetClinicConsoleApp.Models;

namespace VetClinicConsoleApp
{
    class Program
    {
        static List<IAnimal> pets = new List<IAnimal>();

        static void Main(string[] args)
        {
            // Seed data updated (removed image paths)
            pets.Add(new Cat("Fluffy", DateTime.Now.AddYears(-2), "Persian", "Regular checkups", 4.5, "Golden", "Feather wand"));
            pets.Add(new Dog("Buddy", DateTime.Now.AddYears(-3), "Golden Retriever", "Vaccinated", 30.0, "Cream"));

            bool running = true;
            while (running)
            {
                Console.Clear();
                WriteColor("=== VET CLINIC CONSOLE APP ===", ConsoleColor.Cyan);
                Console.WriteLine("1. View All Pets");
                Console.WriteLine("2. Add New Pet");
                Console.WriteLine("3. View Details / Adopt Pet");
                Console.WriteLine("4. Exit");
                Console.WriteLine("-----------------------------");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAllPets();
                        break;
                    case "2":
                        AddNewPet();
                        break;
                    case "3":
                        ViewDetailsAndAdopt();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        WriteColor("Invalid option. Press Enter to try again.", ConsoleColor.Red);
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ViewAllPets()
        {
            Console.Clear();
            WriteColor("--- All Pets ---", ConsoleColor.Yellow);

            if (pets.Count == 0)
            {
                WriteColor("No pets in the clinic.", ConsoleColor.DarkGray);
            }
            else
            {
                for (int i = 0; i < pets.Count; i++)
                {
                    var pet = pets[i];
                    Console.Write($"{i + 1}. ");

                    ConsoleColor typeColor = pet is Cat ? ConsoleColor.Magenta : ConsoleColor.Blue;
                    WriteColor(pet.ToString(), typeColor, false);

                    Console.Write(" - Status: ");
                    if (pet.Status == Status.Adopted)
                        WriteColor("Adopted", ConsoleColor.Green);
                    else
                        WriteColor("Not Adopted", ConsoleColor.Red);
                }
            }
            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        static void AddNewPet()
        {
            Console.Clear();
            WriteColor("--- Add New Pet ---", ConsoleColor.Yellow);

            string type = "";
            while (type != "cat" && type != "dog")
            {
                Console.Write("Type (Cat/Dog): ");
                type = Console.ReadLine()?.ToLower();
                if (type != "cat" && type != "dog")
                    WriteColor("Invalid type. Please enter 'Cat' or 'Dog'.", ConsoleColor.Red);
            }

            string name = GetValidString("Name", 3);
            string breed = GetValidString("Breed", 5);
            string medicalHistory = GetValidString("Medical History", 10);
            string color = GetValidString("Color", 1);
            double weight = GetValidDouble("Weight (kg)", 0);
            // Image input removed here
            DateTime birthday = GetValidDate("Birthday (yyyy-mm-dd)");

            if (type == "cat")
            {
                string favToy = GetValidString("Favorite Toy", 3);
                pets.Add(new Cat(name, birthday, breed, medicalHistory, weight, color, favToy));
            }
            else
            {
                pets.Add(new Dog(name, birthday, breed, medicalHistory, weight, color));
            }

            WriteColor("\nPet added successfully! Press Enter to continue...", ConsoleColor.Green);
            Console.ReadLine();
        }

        static void ViewDetailsAndAdopt()
        {
            Console.Clear();
            WriteColor("--- Select a Pet to View/Adopt ---", ConsoleColor.Yellow);

            if (pets.Count == 0)
            {
                WriteColor("No pets available.", ConsoleColor.DarkGray);
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i]}");
            }

            Console.Write("\nEnter ID number (or 0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= pets.Count)
            {
                var pet = pets[selection - 1];
                ShowPetDetails(pet);
            }
        }

        static void ShowPetDetails(IAnimal pet)
        {
            bool stayingInDetails = true;
            while (stayingInDetails)
            {
                Console.Clear();
                WriteColor($"--- Details for {pet.Name} ---", ConsoleColor.Cyan);

                Console.WriteLine($"Type: {pet.GetType().Name}");
                Console.WriteLine($"Birthday: {pet.Birthday.ToShortDateString()}");
                Console.WriteLine($"Breed: {pet.Breed}");
                Console.WriteLine($"Medical History: {pet.MedicalHistory}");
                Console.WriteLine($"Weight: {pet.Weight} kg");
                Console.WriteLine($"Color: {pet.Color}");

                Console.Write("Status: ");
                if (pet.Status == Status.Adopted)
                    WriteColor($"{pet.Status}", ConsoleColor.Green);
                else
                    WriteColor($"{pet.Status}", ConsoleColor.Red);

                if (pet is Cat cat)
                {
                    Console.WriteLine($"Favorite Toy: {cat.FavoriteToy}");
                }

                Console.WriteLine("-----------------------------");

                if (pet.Status == Status.NotAdopted)
                {
                    Console.WriteLine("A. Adopt this Pet");
                }
                else
                {
                    WriteColor("(This pet is already adopted)", ConsoleColor.DarkGray);
                }
                Console.WriteLine("B. Back to Menu");

                Console.Write("Select option: ");
                string choice = Console.ReadLine()?.ToUpper();

                if (choice == "A" && pet.Status == Status.NotAdopted)
                {
                    pet.Adopt();
                    WriteColor($"\nCongratulations! You have adopted {pet.Name}!", ConsoleColor.Green);
                    Console.WriteLine("Press Enter to update details...");
                    Console.ReadLine();
                }
                else if (choice == "B")
                {
                    stayingInDetails = false;
                }
            }
        }

        static void WriteColor(string text, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }

        static string GetValidString(string fieldName, int minLength)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && input.Length >= minLength)
                {
                    return input;
                }

                WriteColor($"Error: {fieldName} is required and must be at least {minLength} characters long.", ConsoleColor.Red);
            }
        }

        static double GetValidDouble(string fieldName, double min)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                if (double.TryParse(Console.ReadLine(), out double result) && result > min)
                {
                    return result;
                }
                WriteColor($"Error: {fieldName} must be a valid number greater than {min}.", ConsoleColor.Red);
            }
        }

        static DateTime GetValidDate(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    return date;
                }
                WriteColor("Error: Invalid date format.", ConsoleColor.Red);
            }
        }
    }
}