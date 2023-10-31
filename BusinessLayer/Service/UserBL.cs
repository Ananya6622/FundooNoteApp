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
    }
}
