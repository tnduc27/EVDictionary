using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Repositories
{
    public interface IUserRepository
    {
        User GetUserByName(string userName);
        bool RegisterUser(User newUser);
    }
}
