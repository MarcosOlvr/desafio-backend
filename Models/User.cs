using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public UserType UserType { get; set; }
    }
}
