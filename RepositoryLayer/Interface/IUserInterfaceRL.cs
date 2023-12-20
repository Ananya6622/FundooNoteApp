using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserInterfaceRL
    {
        public UserEntity UserRegistrations(UserRegistration userRegistration);
      
       public  UserEntity UserLogins(UserLogin userLogin);

        public bool DoesEmailExists(string email);

        public List<UserEntity> GetAllUsers();
        public  Task<string> ForgetPassword(string Email, string Token);
        public string GenerateToken(string Email, int UserId);

        public bool ResetPassword(string email, resetPassword reset);
        public List<UserEntity> GetDetailsOfUser(string firstName);
        public string LoginWithJwt(UserLogin userLogin);
        public UserTicket CreateTicketForPassword(string emailId, string token);
        public UserEntity GetUserById(int userId);
        public bool UpdateUser(long userId, UserRegistration userRegistration);
    }
}
