
using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNote.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooDbContext fundooDbContext;
        IuserBl iuserBl;

        public UserController(FundooDbContext fundooDbContext, IuserBl iuserBl)
        {
            this.fundooDbContext = fundooDbContext;
            this.iuserBl = iuserBl;
        }

        [HttpPost("AddUser")]
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

        [HttpPost("Login")]

        public ActionResult LoginUser(string email, string password)
        {
            try
            {

                var userdata = fundooDbContext.user.FirstOrDefault(u => u.Email == email && u.Password == password);
                string token = this.iuserBl.LoginUser(email, password);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, massage = $"Email or Password Invalid" });
                }

                return this.Ok(new { sucess = true, message = $"Token generated is " , data= token });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { succes = false, massage = $"Login faild {e.Message}" });
            }
        }

        //forget password
        [HttpPost("ForgotPassword/{email}")]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.iuserBl.ForgotPassword(email);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = $"Mail Sent Successfully " + $" token:  {result}" });
                }
                return this.BadRequest(new { success = false, message = $"mail not sent" });
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// reset the password
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPut("ResetPassword")]

        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {

                string email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                bool result = iuserBl.ResetPassword(resetPasswordModel, email);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, message = "Enter valid password" });

                }

                return this.Ok(new { success = true, message = "reset password set successfully" });
            }
            catch
            {
                throw;
            }
        }
    }
}
