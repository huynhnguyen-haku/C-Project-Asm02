using BusinessObject.Models;

namespace Service.Interface
{
    public interface IUserService
    {
        public int GetUserCount();
        public User GetUserByEmailAndPassword(string email, string password);
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public User CreateUser(User user);
        public User UpdateUser(User user);
        public List<User> GetUserList();
        public IEnumerable<User> searchUser(string name);
        public void DeleteUser(int id);
        public List<int> GetUserTypeList();
        public List<User> GetAllUser();
    }
}
