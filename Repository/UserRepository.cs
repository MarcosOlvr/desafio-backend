using desafio_backend.Data;
using desafio_backend.Models;
using desafio_backend.Repository.Contracts;

namespace desafio_backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) 
        {
            _db = db;
        }

        public User AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }

        public void DeleteUser(int id)
        {
            var user = _db.Users.Find(id);

            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }

        public User GetUser(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);

            return user;
        }

        public User UpdateUser(int id, User user)
        {
            var userById = _db.Users.FirstOrDefault(x => x.Id == id);

            if (userById != null) 
            {
                userById.FirstName = user.FirstName;
                userById.LastName = user.LastName;
                userById.Document = user.Document;
                userById.Email = user.Email;
                userById.Password = user.Password;
                userById.Balance = user.Balance;

                _db.Users.Update(userById);
                _db.SaveChanges();

                return userById;
            }

            throw new Exception("Update error!");
        }
    }
}
