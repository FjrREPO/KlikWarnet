using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efpe.Model.Entity
{
    public class UserEntity
    {
        string username, email, password;

        public UserEntity() { }

        public UserEntity(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }

        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}
