using BusinessObject.Models;
using DataAccessObject;
using Repository.Interface;
using System.Text.RegularExpressions;

namespace Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private UserDAO userDAO;

        public UserRepo()
        {
            userDAO = new UserDAO();
        }

        public User CreateUser(User user) => userDAO.CreateUser(user);

        public void DeleteUser(int id) => userDAO.DeleteUser(id);

        public List<User> GetAllUser() => userDAO.GetAllUser();

        public User GetUserByEmail(string email) => userDAO.GetUserByEmail(email);

        public User GetUserByEmailAndPassword(string email, string password) => userDAO.GetUserByEmailAndPassword(email, password);

        public User GetUserById(int id) => userDAO.GetUserById(id);

        public int GetUserCount() => userDAO.GetUserCount();

        public List<User> GetUserList() => userDAO.GetUserList();

        public List<int> GetUserTypeList() => userDAO.GetUserTypeList();

        public IEnumerable<User> searchUser(string name) => userDAO.searchUser(name);

        public User UpdateUser(User user) => userDAO.UpdateUser(user);
    }
}
