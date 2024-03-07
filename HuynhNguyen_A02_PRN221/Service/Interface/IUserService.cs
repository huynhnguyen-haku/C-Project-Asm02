using BusinessObject.Models;

namespace Service.Interface
{
    public interface IUserService
    {
        public User GetUserByEmailAndPassword(string email, string password);
        public int GetNumberOfUserAccounts();
        public List<User> GetUsersList();
        public User GetUserByID(int id);
        public List<int> GetUserTypeList();
        public User CreateUserAccounts(User user);
        public User UpdateUsersAccount(User user);
        public List<User> SearchUsers(string searchTerm);
        public bool DeleteUser(int userId);
        public int GetNextUserId();
        Task<int> AddUserAsync(User user);
    }
}
