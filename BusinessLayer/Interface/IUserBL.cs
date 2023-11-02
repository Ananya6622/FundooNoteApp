﻿using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity UserRegistrations(UserRegistration userRegistration);
        public UserEntity UserLogins(UserLogin userLogin);
        public bool DoesEmailExist(string email);
        public List<UserEntity> GetAllUsers();
        public string ForgetPassword(string Email);
        public string GenerateToken(string Email, int UserId);
        public bool ResetPassword(string email, resetPassword reset);
        public List<UserEntity> GetDetailsOfUser(string firstName);
    }
}
