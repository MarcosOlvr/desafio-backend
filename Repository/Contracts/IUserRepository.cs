using desafio_backend.Models;
using desafio_backend.Models.DTO;

namespace desafio_backend.Repository.Contracts
{
    public interface IUserRepository
    {
        List<UserDTO> GetUsers();
        User GetUser(int id);
        User AddUser(User user);
    }
}
