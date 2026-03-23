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
    public class UserService : IUserService
    {

        private readonly Singleton _repository;

        public UserService()
        {
            _repository = Singleton.Instance;
        }

        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _repository.Users.Add(user);
        }

        public List<User> GetAllUsers()
        {
            return _repository.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _repository.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
