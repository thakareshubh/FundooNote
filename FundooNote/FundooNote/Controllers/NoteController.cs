using BuisnessLayer.Interface;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
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
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.inoteBl.AddNote(UserId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Delete the note
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
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

       
        /// <summary>
        /// Change color of note
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("changeColor")]

        public async Task<ActionResult>ChangeColor(int noteId,string color)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                var note=fundooDbContext.notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if(note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Note does not exixt" });
                }

                await this.inoteBl.ChangeColor(UserId,noteId,color);
                return this.Ok(new { success = false, message = "color change succesfully" });

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Archieve Note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("ArchiveNote")]
        public async Task<ActionResult> ArchiveNote(int userId, int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooDbContext.notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to Archive Note" });
                }
                await this.inoteBl.ArchiveNote(UserId, noteId);
                return this.Ok(new { success = true, message = " Archived Successfully" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public async Task<ActionResult<Note>> UpdateNote(int noteId, NoteUpDateModel noteUpdateModel)
        {
            try
            {

                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                var note = fundooDbContext.notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);

                if(note != null)
                {
                    await this.inoteBl.UpdateNote(noteId, noteUpdateModel);
                    return this.Ok(new { success = true, message = "Note Updated Successfully" });
                }

                return this.BadRequest(new { success = false, message = "Sorry! Update notes Failed" });

            }
            catch(Exception e)
            {
                throw e;
            }

        }

        [Authorize]
        [HttpGet(("GetNote/{noteId}"))]
        public async Task<ActionResult<Note>> GetNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooDbContext.notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if (note != null)
                {
                   
                     await this.inoteBl.GetNote(noteId);
                     return this.Ok(new { success = true, message = "Get note Success" });
                    
                }
                return this.BadRequest(new { success = false, message = "Sorry! Get Note Failed" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("Trash/{NoteId}")]
        public async Task<ActionResult> IsTrash(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundooDbContext.notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = " Id does not exists" });
                }
                await this.inoteBl.TrashNote(userId, noteId);
                return this.Ok(new { success = true, message = "Note Trashed successfully" });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Note> result = new List<Note>();
                result = await this.inoteBl.GetAllNote(userId);
                return this.Ok(new { success = true, message = $"Below are all notes", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
