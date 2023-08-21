using desafio_backend.Models;

namespace desafio_backend.Repository.Contracts
{
    public interface IUserRepository
    {
        void AddValue(int userId, decimal value);
        void RmValue(int userId, decimal value);
        User GetUser(int id);
        User AddUser(User user);
        User UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
