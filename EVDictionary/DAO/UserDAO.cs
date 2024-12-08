using System;
using System.Linq;
using System.Threading.Tasks;
using EVDictionary.Models;
using Microsoft.EntityFrameworkCore;

namespace EVDictionary.DAO
{
    public class UserDAO
    {
        // Lấy thông tin người dùng theo username
        public static User GetUserByName(string userName)
        {
            using var db = new EvdictionaryContext();
            return db.Users.FirstOrDefault(u => u.Username == userName); // Tìm người dùng theo UserName
        }
        public static bool RegisterUser(User newUser)
        {
            using var db = new EvdictionaryContext();

            // Kiểm tra xem tên người dùng đã tồn tại chưa
            if (db.Users.Any(u => u.Username == newUser.Username))
            {
                return false; // Trả về false nếu tên người dùng đã tồn tại
            }

            // Thêm người dùng mới vào cơ sở dữ liệu
            db.Users.Add(newUser);
            return db.SaveChanges() > 0; // Trả về true nếu thêm thành công
        }
    }
}
