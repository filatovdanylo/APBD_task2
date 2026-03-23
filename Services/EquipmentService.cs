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
    public class EquipmentService : IEquipmentService
    {
        
        private readonly Singleton _repository;

        public EquipmentService()
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

        public Equipment? GetEquipmentById(int id)
        {
            return _repository.Equipment.FirstOrDefault(e => e.Id == id);
        }

        public void MarkEquipmentUnavailable(int equipmentId)
        {
            var equipment = GetEquipmentById(equipmentId) ??
                throw new InvalidOperationException($"No equipment with id = {equipmentId} exists");
            if (equipment.Status == Enum.EquipmentStatus.Rented)
                throw new InvalidOperationException("Cannot mark rented equipment as unavailable");

            equipment.Status = Enum.EquipmentStatus.Unavailable;
        }
    }
}
