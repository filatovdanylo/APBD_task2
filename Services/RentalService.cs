using APBD_TASK2.Database;
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

        public RentalService()
        {
            _repository = Singleton.Instance;
        }

        public void AddEquipment(Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException(nameof(equipment));
            if (_repository.Equipment.Any(e => e.Id == equipment.Id))
                throw new InvalidOperationException("Equipment already exists");

            _repository.Equipment.Add(equipment);
        }

        
        public string GenerateReport()
        {
            throw new NotImplementedException();
        }

        public List<Equipment> getAllAvailableEquipment()
        {
            var listOfAvailableEquipment = _repository.Equipment
                .Where(e => e.Status == Enum.EquipmentStatus.Available)
                .ToList();
            return listOfAvailableEquipment;
        }

        public List<Equipment> GetEquipment()
        {
            return _repository.Equipment.ToList();
        }
    }
}
