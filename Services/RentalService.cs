using APBD_TASK2.Database;
using APBD_TASK2.Enum;
using APBD_TASK2.Interface;
using APBD_TASK2.Models;
using APBD_TASK2.Policy;
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
            var sb = new System.Text.StringBuilder();
            var all = _repository.RentalObjects;

            sb.AppendLine("---Report---");
            sb.AppendLine($"Total users: {_repository.Users.Count}");
            sb.AppendLine($"Total equipment: {_repository.Equipment.Count}");
            sb.AppendLine($"Available: {_repository.Equipment.Count(e => e.Status == EquipmentStatus.Available)}");
            sb.AppendLine($"Rented: {_repository.Equipment.Count(e => e.Status == EquipmentStatus.Rented)}");
            sb.AppendLine($"Unavailable: {_repository.Equipment.Count(e => e.Status == EquipmentStatus.Unavailable)}");
            sb.AppendLine($"Total rentals: {all.Count}");
            sb.AppendLine($"Active: {all.Count(r => r.ReturnDate == null)}");
            sb.AppendLine($"Overdue: {GetOverdueRentals().Count}");
            sb.AppendLine($"Completed: {all.Count(r => r.ReturnDate != null)}");
            sb.AppendLine($"Total penalties: {all.Sum(r => r.Penalty):F2} PLN");

            return sb.ToString();
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

            int activeCount = GetActiveRentalsForUser(userId).Count;
            int maxAllowed = Policy.Policy.GetMaxRentalCount(user.Type);
            if (activeCount >= maxAllowed)
                throw new InvalidOperationException(
                    $"{user.Type} may have at most {maxAllowed} active rental(s), but " +
                    $"{user.Name} currently has {activeCount}"
                    );

            var rental = new RentalObject(user, equipment);
            equipment.Status = EquipmentStatus.Rented;
            _repository.RentalObjects.Add(rental);
            return rental;
        }

        public RentalObject ReturnEquipment(int rentalId)
        {
            var rental = _repository.RentalObjects.FirstOrDefault(r => r.Id == rentalId && r.ReturnDate == null)
               ?? throw new InvalidOperationException($"Active rental #{rentalId} not found");

            rental.ReturnDate = DateTime.Now;
            rental.Equipment.Status = EquipmentStatus.Available;

            rental.Penalty = Policy.Policy.CalculatePenalty(rental.User.Type, rental.DueTime, rental.ReturnDate.Value);

            return rental;
        }
    }
}
