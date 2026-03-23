using APBD_TASK2.Database;
using APBD_TASK2.Enum;
using APBD_TASK2.Interface;
using APBD_TASK2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Services
{
    public class RentalService : IRentalService
    {

        private readonly Singleton _repository;

        public RentalService(Singleton repository)
        {
            _repository = repository;
        }

        public string GenerateReport()
        {
            throw new NotImplementedException();
        }

        public List<RentalObject> GetActiveRentalsForUser(int userId)
        {
            return _repository.RentalObjects
                .Where(r => r.User.Id == userId && r.ReturnDate == null).ToList();
        }

        public List<RentalObject> GetOverdueRentals()
        {
            return _repository.RentalObjects
                .Where(r => r.ReturnDate == null && DateTime.Now > r.DueTime).ToList();
        }

        public RentalObject RentEquipment(int userId, int equipmentId)
        {
            var user = _repository.Users.FirstOrDefault(u => u.Id == userId) 
                ?? throw new InvalidOperationException($"User {userId} not found");
            var equipment = _repository.Equipment.FirstOrDefault(e => e.Id == equipmentId)
                ?? throw new InvalidOperationException($"Equipment {equipmentId} not found");
            if (equipment.Status != EquipmentStatus.Available)
            {
                throw new InvalidOperationException($"{equipment.Name} is not available");    
            }

            var rental = new RentalObject(user, equipment);
            equipment.Status = EquipmentStatus.Rented;
            _repository.RentalObjects.Add(rental);
            return rental;
        }

        public RentalObject ReturnEquipment(int rentalId)
        {
            //TODO
        }
    }
}
