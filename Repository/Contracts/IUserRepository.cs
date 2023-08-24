using desafio_backend.Models;

namespace desafio_backend.Repository.Contracts
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        User AddUser(User user);
    }
}
