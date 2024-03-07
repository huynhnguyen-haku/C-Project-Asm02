using BusinessObject.Models;
using Repository.Implementation;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepo userRepository;
        public UserService()
        {
            userRepository = new UserRepo();
        }

        public Task<int> AddUserAsync(User user)
        {
            return userRepository.AddUserAsync(user);
        }

        public User CreateUserAccounts(User user)
        {
            return userRepository.CreateUserAccounts(user);
        }

        public bool DeleteUser(int userId) => userRepository.DeleteUser(userId);

        public int GetNextUserId()
        {
            return userRepository.GetNextUserId();
        }

        public int GetNumberOfUserAccounts()
        {
            return userRepository.GetNumberOfUserAccounts();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return userRepository.GetUserByEmailAndPassword(email, password);
        }

        public User GetUserByID(int id)
        {
            return userRepository.GetUserByID(id);
        }

        public List<User> GetUsersList()
        {
            return userRepository.GetUsersList();
        }

        public List<int> GetUserTypeList()
        {
            return userRepository.GetUserTypeList();
        }

        public List<User> SearchUsers(string searchTerm)
        {
            return userRepository.SearchUsers(searchTerm);
        }

        public User UpdateUsersAccount(User user)
        {
            return userRepository.UpdateUsersAccount(user);
        }
    }
}