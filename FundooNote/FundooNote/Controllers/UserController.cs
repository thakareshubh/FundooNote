
using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;

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

        [HttpPost]
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

    } 
}
