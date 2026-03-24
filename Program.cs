using APBD_TASK2.Controller;
using APBD_TASK2.Database;
using APBD_TASK2.Services;

var equipmentService = new EquipmentService();
var userService = new UserService();
var rentalService = new RentalService();

DataSeeder.Seed(equipmentService, userService);
AppController.Run(equipmentService, rentalService, userService);