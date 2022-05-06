
using BuisnessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;

namespace FundooNote.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooDbContext fundooDbContext;
        UserBl iuserBl;
        public UserController(FundooDbContext fundooDbContext, UserBl iuserBl)
        {
            this.fundooDbContext = fundooDbContext;
            this.iuserBl = iuserBl;
        }
        [HttpPost]

        public IActionResult AddUser(User user)
        {
            try
            {
                this.iuserBl.AddUser(user);
                return this.Ok(new { sucess = true, message = $"User data added successfully" });

            }
            catch(Exception e)
            {
                throw e;
            }
        }

    } 
}
