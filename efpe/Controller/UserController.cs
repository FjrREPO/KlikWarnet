using efpe.Model.Entity;
using efpe.Model.Repository;
using System;

namespace efpe.Controller
{
    internal class UserController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public bool RegisterUser(string username, string email, string password)
        {
            UserEntity newUser = new UserEntity { Username = username, Email = email, Password = password };

            return _userRepository.RegisterUser(newUser);
        }

        public bool AuthenticateUser(string usernameOrEmail, string password)
        {
            return _userRepository.AuthenticateUser(usernameOrEmail, password);
        }

        public Tuple<string, string> GetUsernameAndEmail(string usernameOrEmail)
        {
            return _userRepository.GetUsernameAndEmail(usernameOrEmail);
        }
    }
}
