using desafio_backend.Models;

namespace desafio_backend.Repository.Contracts
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User AddUser(User user);
        User UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
