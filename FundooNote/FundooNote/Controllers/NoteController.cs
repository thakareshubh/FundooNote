using BuisnessLayer.Interface;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundooDbContext fundooDbContext;
        InoteBl inoteBl;

        public NoteController(FundooDbContext fundooDbContext, InoteBl inoteBl)
        {
            this.fundooDbContext = fundooDbContext;
            this.inoteBl = inoteBl;
        }

        //Add  Note
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NoteModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.inoteBl.AddNote(UserId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteNote")]
        public ActionResult DeleteNote(int UserId)
        {
            try
            {
                if(inoteBl.DeleteNote(UserId))
                {
                    return this.Ok(new { success = true, message = "Note is deleted" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note is not dleted" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }




    }
}
