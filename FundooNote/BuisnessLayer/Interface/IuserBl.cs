using CommonLayer.Users;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IuserBl
    {
        public void AddUser(UserPostModel userPost);

        public string LoginUser(string email, string password);

        public bool ForgotPassword(string email);
    }
}
