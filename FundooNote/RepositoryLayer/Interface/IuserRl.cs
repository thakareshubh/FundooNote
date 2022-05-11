
using CommonLayer.Users;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IuserRl
    {
        public void AddUser(UserPostModel userPost);
        public string LoginUser(string email, string password);

        public bool ForgotPassword(string email);
    }
}
