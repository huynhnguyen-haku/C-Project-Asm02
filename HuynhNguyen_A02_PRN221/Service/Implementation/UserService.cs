using BusinessObject.Models;
using Repository.Implementation;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo userRepo;
        public UserService()
        {
            userRepo = new UserRepo();
        }

        public int GetUserCount() => userRepo.GetUserCount();
        public User CreateUser(User user) => userRepo.CreateUser(user);

        public void DeleteUser(int id) => userRepo.DeleteUser(id);

        public User GetUserByEmail(string email) => userRepo.GetUserByEmail(email);

        public User GetUserByEmailAndPassword(string email, string password) => userRepo.GetUserByEmailAndPassword(email, password);

        public User GetUserById(int id) => userRepo.GetUserById(id);

        public List<User> GetUserList() => userRepo.GetUserList();

        public IEnumerable<User> searchUser(string name) => userRepo.searchUser(name);

        public User UpdateUser(User user) => userRepo.UpdateUser(user);

        public List<int> GetUserTypeList() => userRepo.GetUserTypeList();

        public List<User> GetAllUser() => userRepo.GetAllUser();
    }
}
