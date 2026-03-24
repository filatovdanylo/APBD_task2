using APBD_TASK2.Interface;
using APBD_TASK2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Database
{
    public static class DataSeeder
    {
        public static void Seed(IEquipmentService equipmentService, IUserService userService)
        {
            userService.AddUser(new User("John", "Doe", "john.doe@mail.com", Enum.UserType.Student));
            userService.AddUser(new User("Michael", "Doe", "mick.doe@mail.com", Enum.UserType.Employee));
            userService.AddUser(new User("Jimmy", "Doe", "jimmy.doe@mail.com", Enum.UserType.Student));

            equipmentService.AddEquipment(new Laptop("Lenovo super5", 16, 15));
            equipmentService.AddEquipment(new Laptop("Apple Macbook Air", 32, 15));

            equipmentService.AddEquipment(new Camera("Sony Alpha 6400", 50, 650, true));
            equipmentService.AddEquipment(new Camera("Nikon Z8", 45, 500, true));

            equipmentService.AddEquipment(new Projector("Xiaomi Smart Projector L1", Enum.Resolution.FHD, 400));
            equipmentService.AddEquipment(new Projector("Epson EB-X51", Enum.Resolution.QHD, 3600));
        }
    }
}
