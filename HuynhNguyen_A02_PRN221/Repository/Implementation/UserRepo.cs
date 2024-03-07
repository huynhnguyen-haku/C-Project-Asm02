using BusinessObject.Models;
using DataAccessObject;
using Repository.Interface;
using System.Text.RegularExpressions;

namespace Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        public int GetNumberOfUserAccounts() => UserDAO.Instance.GetNumberOfUserAccounts();
        public User GetUserByEmailAndPassword(string email, string password) => UserDAO.Instance.GetUserByEmailAndPassword(email, password);
        public User CreateUserAccounts(User user) => UserDAO.Instance.CreateUserAccounts(user);
        public List<User> GetUsersList() => UserDAO.Instance.GetUsersList();
        public User GetUserByID(int id) => UserDAO.Instance.GetUserByID(id);
        public List<int> GetUserTypeList() => UserDAO.Instance.GetUserTypeList();
        public User UpdateUsersAccount(User user) => UserDAO.Instance.UpdateUsersAccount(user);
        public List<User> SearchUsers(string searchTerm) => UserDAO.Instance.SearchUsers(searchTerm);
        public bool DeleteUser(int userId) => UserDAO.Instance.DeleteUser(userId);
        public int GetNextUserId() => UserDAO.Instance.GetNextUserId();
        Task<int> IUserRepo.AddUserAsync(User user) => UserDAO.Instance.AddUserAsync(user);
    }
}
