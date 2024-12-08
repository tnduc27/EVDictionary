using EVDictionary.Models;
using EVDictionary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }
        public User GetUserByName(string userName)
        {
            return userRepository.GetUserByName(userName);
        }
        public bool RegisterUser(User newUser)
        {
            return userRepository.RegisterUser(newUser);
        }
    }
}
