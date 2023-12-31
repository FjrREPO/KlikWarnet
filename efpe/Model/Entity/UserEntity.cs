using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efpe.Model.Entity
{
    public class UserEntity
    {
        string username, email, password, vipAtauReguler;

        public UserEntity() { }

        public UserEntity(string username, string email, string password, string vipAtauReguler)
        {
            this.username = username;
            this.email = email;
            this.password = password;
            this.vipAtauReguler = vipAtauReguler;
        }

        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string VipAtauReguler {  get => vipAtauReguler; set => vipAtauReguler = value;}
    }
}
