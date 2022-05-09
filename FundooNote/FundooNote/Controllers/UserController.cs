
using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooDbContext fundooDbContext;
        IuserBl iuserBl;

        public UserController(FundooDbContext fundooDbContext,IuserBl iuserBl)
        {
            this.fundooDbContext = fundooDbContext;
            this.iuserBl = iuserBl;
        }

        [HttpPost("Add user")]
        public ActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.iuserBl.AddUser(user);
                return this.Ok(new { sucess = true, message = $"User data added successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("LoginUser")]

        public ActionResult LoginUser(string email,string password)
        {
            try
            {

                var userdata = fundooDbContext.user.FirstOrDefault(u => u.Email == email && u.Password == password);
                string token = this.iuserBl.LoginUser(email, password);
                if (token == null)
                {
                    return this.BadRequest(new { success = false,massage=$"Email or Password Invalid" }) ;
                }

                return this.Ok(new { sucess = true, message = $"Token generated is " + token });
            }
            catch(Exception e)
            {
                return this.BadRequest(new { succes = false, massage = $"Login faild {e.Message}" });
            }
        }

    } 
}
