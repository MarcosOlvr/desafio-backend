﻿using desafio_backend.Data;
using desafio_backend.Models;
using desafio_backend.Models.DTO;
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

        public User GetUser(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);

            return user;
        }
        
        public List<UserDTO> GetUsers()
        {
            var users = _db.Users.ToList();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(new UserDTO { Id = user.Id, Name = user.FirstName + ' ' + user.LastName });
            }

            return usersDTO;
        }
    }
}
