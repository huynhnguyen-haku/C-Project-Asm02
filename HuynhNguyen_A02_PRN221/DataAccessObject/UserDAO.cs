using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Principal;


namespace DataAccessObject
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private readonly CarManagementContext dbContext = null;

        private UserDAO()
        {
            dbContext = new CarManagementContext();
        }
        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance;
            }
        }

        public int GetNextUserId()
        {
            // Lấy UserId lớn nhất trong cơ sở dữ liệu và tăng lên 1
            int nextUserId = dbContext.Users.Max(u => (int?)u.UserId) ?? 0;
            return nextUserId + 1;
        }

        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                return user.UserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                User mappedUser = new User
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    UserName = user.UserName,
                    City = user.City,
                    Country = user.Country,
                    Password = user.Password,
                    Birthday = user.Birthday,
                    Role = user.Role
                };
                return mappedUser;
            }
            return null;
        }
        public List<User> GetUsersList()
        {
            List<User> list = null;
            try
            {
                var dbContext = new CarManagementContext();
                list = dbContext.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        // thêm hàm này để hiện tổng User account trên dashboard
        public int GetNumberOfUserAccounts()
        {
            int NumberOfUserAccounts = 0;
            try
            {
                var dbContext = new CarManagementContext();
                NumberOfUserAccounts = dbContext.Users.Count(m => m.Role == "Customer");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return NumberOfUserAccounts;
        }

        public User GetUserByID(int id)
        {
            User user = null;
            try
            {
                var dbContext = new CarManagementContext();
                user = dbContext.Users.SingleOrDefault(m => m.UserId.Equals(id));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;
            try
            {
                var dbContext = new CarManagementContext();
                user = dbContext.Users.SingleOrDefault(m => m.Email.Equals(email));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User CreateUserAccounts(User user)
        {
            User _customer = GetUserByEmail(user.Email);
            try
            {
                if (_customer == null)
                {
                    var dbContext = new CarManagementContext();
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _customer;
        }

        public User UpdateUsersAccount(User user)
        {
            var existingAccount = dbContext.Users.Find(user.UserId);
            try
            {
                if (existingAccount != null)
                {
                    existingAccount.Email = user.Email;
                    existingAccount.UserName = user.UserName;
                    existingAccount.City = user.City;
                    existingAccount.Country = user.Country;
                    existingAccount.Password = user.Password;
                    existingAccount.Birthday = user.Birthday;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return existingAccount;
        }

        public List<User> SearchUsers(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            var result = dbContext.Users
                .Where(u => u.Email.ToLower().Contains(searchTerm) ||
                            u.UserName.ToLower().Contains(searchTerm) ||
                            u.City.ToLower().Contains(searchTerm) ||
                            u.Country.ToLower().Contains(searchTerm))
                .ToList();

            return result;
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var userToDelete = dbContext.Users.Find(userId);

                if (userToDelete != null)
                {
                    dbContext.Users.Remove(userToDelete);
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<int> GetUserTypeList()
        {
            List<int> userTypes;
            try
            {
                var dbContext = new CarManagementContext();
                userTypes = dbContext.Users
                .Where(user => user.UserId != null)
                .Select(user => user.UserId)
                .Distinct()
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userTypes;
        }
    }
}
