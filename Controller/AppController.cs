using APBD_TASK2.Enum;
using APBD_TASK2.Interface;
using APBD_TASK2.Models;
using APBD_TASK2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Controller
{
    public class AppController
    {

        private static IEquipmentService _equipmentService = null!;
        private static IRentalService _rentalService = null!;
        private static IUserService _userService = null!;

        public static void Run(IEquipmentService equipmentService, 
            IRentalService rentalService, 
            IUserService userService
            )
        {
            _equipmentService = equipmentService;
            _rentalService = rentalService;
            _userService = userService;

            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine()?.Trim();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddUser();
                            break;
                        case "2":
                            AddEquipment();
                            break;
                        case "3":
                            ListAllEquipment();
                            break;
                        case "4":
                            ListAvailableEquipment();
                            break;
                        case "5":
                            RentEquipment();
                            break;
                        case "6":
                            ReturnEquipment();
                            break;
                        case "7":
                            MarkUnavailable();
                            break;
                        case "8":
                            ActiveRentalsForUser();
                            break;
                        case "9":
                            OverdueRentals();
                            break;
                        case "10":
                            Console.WriteLine(_rentalService.GenerateReport());
                            break;
                        case "0": Console.WriteLine("Exiting..."); return;
                        default: Console.WriteLine("Unknown option. Try again"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }


        private static void ShowMenu()
        {
            Console.WriteLine("1.  Add user");
            Console.WriteLine("2.  Add equipment");
            Console.WriteLine("3.  List all equipment");
            Console.WriteLine("4.  List available equipment");
            Console.WriteLine("5.  Rent equipment");
            Console.WriteLine("6.  Return equipment");
            Console.WriteLine("7.  Mark equipment unavailable");
            Console.WriteLine("8.  Active rentals for user");
            Console.WriteLine("9.  Overdue rentals");
            Console.WriteLine("10. Generate report");
            Console.WriteLine("0.  Exit");
            Console.WriteLine("Choose: ");
        }

        private static void AddUser()
        {
            Console.Write("First name: ");
            var name = ReadRequired("First name");

            Console.Write("Last name: ");
            var surname = ReadRequired("Last name");

            Console.Write("Email: ");
            var email = ReadRequired("Email");

            Console.Write("Type (1 = Student, 2 = Employee): ");
            var type = ReadEnum<UserType>("User type");

            _userService.AddUser(new User(name, surname, email, type));
            Console.WriteLine("User added successfully");
        }

        private static void AddEquipment()
        {
            Console.WriteLine("Equipment type: 1 = Laptop, 2 = Projector, 3 = Camera");
            Console.Write("Choose: ");
            var typeInput = ReadInt("Equipment type");

            Console.Write("Name: ");
            var name = ReadRequired("Name");

            Equipment equipment = typeInput switch
            {
                1 => CreateLaptop(name),
                2 => CreateProjector(name),
                3 => CreateCamera(name),
                _ => throw new InvalidOperationException("Invalid equipment type.")
            };

            _equipmentService.AddEquipment(equipment);
            Console.WriteLine($"Equipment '{name}' added with ID #{equipment.Id}");
        }

        private static Laptop CreateLaptop(string name)
        {
            Console.Write("RAM (GB): ");
            int ram = ReadInt("RAM");
            Console.Write("Screen size (inches): ");
            int screen = ReadInt("Screen size");
            return new Laptop(name, ram, screen);
        }

        private static Projector CreateProjector(string name)
        {
            Console.Write("Resolution (0=FHD, 1=QHD, 2=UHD): ");
            var res = ReadEnum<Resolution>("Resolution");
            Console.Write("Brightness (lumens): ");
            int lumens = ReadInt("Brightness");
            return new Projector(name, res, lumens);
        }

        private static Camera CreateCamera(string name)
        {
            Console.Write("Resolution (megapixels): ");
            int mp = ReadInt("Resolution");
            Console.Write("Weight (grams): ");
            int weight = ReadInt("Weight");
            Console.Write("Can shoot video? (y/n): ");
            bool video = Console.ReadLine()?.Trim().ToLower() == "y";
            return new Camera(name, mp, weight, video);
        }

        private static void ListAllEquipment()
        {
            var list = _equipmentService.GetEquipment();
            if (!list.Any()) { Console.WriteLine("No equipment registered"); return; }
            foreach (var e in list)
                Console.WriteLine(FormatEquipment(e));
        }

        private static void ListAvailableEquipment()
        {
            var list = _equipmentService.getAllAvailableEquipment();
            if (!list.Any()) { 
                Console.WriteLine("No equipment available"); 
                return; 
            }
            foreach (var e in list)
                Console.WriteLine(FormatEquipment(e));
        }

        private static void RentEquipment()
        {
            PrintUsers();
            Console.Write("User ID: ");
            int userId = ReadInt("User ID");

            ListAvailableEquipment();
            Console.Write("Equipment ID: ");
            int equipId = ReadInt("Equipment ID");

            var rental = _rentalService.RentEquipment(userId, equipId);
            Console.WriteLine($"Rental #{rental.Id} created. Due: {rental.DueTime:yyyy-MM-dd}");
        }

        private static void ReturnEquipment()
        {
            Console.Write("Rental ID: ");
            int rentalId = ReadInt("Rental ID");

            var rental = _rentalService.ReturnEquipment(rentalId);
            Console.WriteLine($"Equipment '{rental.Equipment.Name}' returned on {rental.ReturnDate:yyyy-MM-dd}");

            if (rental.Penalty > 0)
                Console.WriteLine($"Late return penalty: {rental.Penalty:F2} PLN ({rental.User.Type})");
            else
                Console.WriteLine("No penalty. Returned on time.");
        }

        private static void MarkUnavailable()
        {
            ListAllEquipment();
            Console.Write("Equipment ID: ");
            int id = ReadInt("Equipment ID");
            _equipmentService.MarkEquipmentUnavailable(id);
            Console.WriteLine($"Equipment #{id} marked as unavailable");
        }

        private static void ActiveRentalsForUser()
        {
            PrintUsers();
            Console.Write("User ID: ");
            int userId = ReadInt("User ID");

            var rentals = _rentalService.GetActiveRentalsForUser(userId);
            if (!rentals.Any()) { Console.WriteLine("No active rentals for this user."); return; }

            foreach (var r in rentals)
            {
                bool overdue = DateTime.Now > r.DueTime;
                Console.WriteLine($"{r.Id,-5} {r.Equipment.Name,-25} {r.RentalDate:yyyy-MM-dd}-{r.DueTime:yyyy-MM-dd} {(overdue ? "YES" : "no")}");
            }
        }

        private static void OverdueRentals()
        {
            var rentals = _rentalService.GetOverdueRentals();
            if (!rentals.Any()) { 
                Console.WriteLine("No overdue rentals found"); 
                return; 
            }

            foreach (var r in rentals)
            {
                int days = (int)Math.Ceiling((DateTime.Now - r.DueTime).TotalDays);
                Console.WriteLine($"{r.Id,-5} {r.User.Name + " " + r.User.Surname,-20} {r.Equipment.Name,-25} {r.DueTime:yyyy-MM-dd,-14} {days}");
            }
        }

        private static string FormatEquipment(Equipment e)
        {
            var type = e.GetType().Name;
            var details = e switch
            {
                Laptop l => $"RAM: {l.RamGb}GB, Screen: {l.ScreenSize}\"",
                Projector p => $"Res: {p.Resolution}, Brightness: {p.BrightnessLumens}lm",
                Camera c => $"{c.ResolutionMegapixels}MP, {c.WeightGrams}g, Video: {(c.CanShootVideo ? "yes" : "no")}",
                _ => ""
            };
            return $"{e.Id,-5} {e.Name,-25} {type,-12} {e.Status,-14} {details}";
        }

        private static void PrintUsers()
        {
            Console.WriteLine("Users:");
            foreach (var u in _userService.GetAllUsers())
                Console.WriteLine($"ID: {u.Id} {u.Name} {u.Surname} ({u.Type})");
        }


        //Utility reading methods
        private static string ReadRequired(string field)
        {
            var value = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"{field} cannot be empty");
            return value;
        }

        private static int ReadInt(string field)
        {
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int result))
                throw new InvalidOperationException($"'{field}' must be a valid number");
            return result;
        }

        private static T ReadEnum<T>(string field) where T : struct, System.Enum
        {
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int val) || !System.Enum.IsDefined(typeof(T), val))
                throw new InvalidOperationException($"Invalid value for {field}");
            return (T)(object)val;
        }
    }
}
