using EVDictionary.DAO;
using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByName(string userName)
                => UserDAO.GetUserByName(userName);
        public bool RegisterUser(User newUser)
       => UserDAO.RegisterUser(newUser);
    }
}
