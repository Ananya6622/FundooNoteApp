using BusinessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserInterfaceRL userInterfaceRL;

        public UserBL(IUserInterfaceRL userInterfaceRL )
        {
            this.userInterfaceRL = userInterfaceRL;
        }

        public UserEntity UserRegistrations(UserRegistration userRegistration)
        {
            try
            {
                return this.userInterfaceRL.UserRegistrations(userRegistration);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public UserEntity UserLogins(UserLogin userLogin)
        {
            try
            {
                UserEntity userEntity = userInterfaceRL.UserLogins(userLogin);
                if(userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DoesEmailExist(string email)
        {
            return userInterfaceRL.DoesEmailExists(email);
        }

        public List<UserEntity> GetAllUsers()
        {
            return userInterfaceRL.GetAllUsers();
        }

        public string ForgetPassword(string Email)
        {
            return userInterfaceRL.ForgetPassword(Email);
        }

        public string GenerateToken(string Email, int UserId)
        {
            try
            {
                return userInterfaceRL.GenerateToken(Email, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, resetPassword reset)
        {
            try
            {
                return userInterfaceRL.ResetPassword(email, reset);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserEntity> GetDetailsOfUser(string firstName)
        {
            try
            {
                return userInterfaceRL.GetDetailsOfUser(firstName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string LoginWithJwt(UserLogin userLogin)
        {
            try
            {
                return userInterfaceRL.LoginWithJwt(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserTicket CreateTicketForPassword(string emailId, string token)
        {
            try
            {
                return userInterfaceRL.CreateTicketForPassword(emailId, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserEntity GetUserById(int userId)
        {
            try
            {
                return userInterfaceRL.GetUserById(userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateUser(long userId, UserRegistration userRegistration)
        {
            try
            {
                return userInterfaceRL.UpdateUser(userId, userRegistration);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
