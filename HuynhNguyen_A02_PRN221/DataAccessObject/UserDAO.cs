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
        public UserDAO() { }

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

        public int GetUserCount()
        {
            int count = 0;
            try
            {
                var dbContext = new CarManagementContext();
                count = dbContext.Users.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            User user = null;
            try
            {
                var dbContext = new CarManagementContext();
                user = dbContext.Users.SingleOrDefault(m => m.Email.Trim().Equals(email) && m.Password.Trim().Equals(password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User GetUserById(int id)
        {
            User user = null;
            try
            {
                var dbContext = new CarManagementContext();
                user = dbContext.Users.SingleOrDefault(m => m.UserId == id);
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
                user = dbContext.Users.SingleOrDefault(m => m.Email.Trim().Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User UpdateUser(User user)
        {
            var dbContext = new CarManagementContext();
            var existingUser = GetUserById(user.UserId);
            try
            {
                if(existingUser != null)
                {
                    existingUser.UserName = user.UserName;
                    existingUser.Email = user.Email;
                    existingUser.City = user.City;
                    existingUser.Country = user.Country;
                    existingUser.Birthday = user.Birthday;
                    existingUser.Role = user.Role;
                    dbContext.Entry(existingUser).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return existingUser;
        }

        public List<User> GetUserList()
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
        public IEnumerable<User> searchUser(string name)
        {
            List<User> list = null;
            try
            {
                var dbContext = new CarManagementContext();
                list = dbContext.Users.Where(m => m.UserName.Contains(name)).ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public User CreateUser(User user)
        {
            User customer = GetUserByEmail(user.Email);
            try
            {
                if(customer == null)
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
            return customer;
        }

        public void DeleteUser(int id)
        {
            try
            {
                User checkExisted = GetUserById(id);
                if (checkExisted != null)
                {
                    var dbContext = new CarManagementContext();
                    dbContext.Users.Remove(checkExisted);
                    dbContext.SaveChanges();
                }
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

        //--------------------------
        readonly CarManagementContext _context = new CarManagementContext();

        public List<User> GetAllUser()
        {
           return _context.Users.ToList();
        }
    }
}
