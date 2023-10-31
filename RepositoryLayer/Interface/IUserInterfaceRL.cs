using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserInterfaceRL
    {
        public UserEntity UserRegistrations(UserRegistration userRegistration);
      
       public  UserEntity UserLogins(UserLogin userLogin);

        public bool DoesEmailExists(string email);

        public List<UserEntity> GetAllUsers();
    }
}
