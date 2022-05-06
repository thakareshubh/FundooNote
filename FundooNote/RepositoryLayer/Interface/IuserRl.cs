using CommonLayer.NewUser;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IuserRl
    {
        public void AddUser(User user);
    }
}
