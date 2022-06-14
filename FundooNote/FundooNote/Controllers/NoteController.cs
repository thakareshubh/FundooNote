using BuisnessLayer.Interface;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooDbContext fundooDbContext;
        InoteBl inoteBl;

        public NoteController(FundooDbContext fundooDbContext, InoteBl inoteBl,IMemoryCache memoryCache,IDistributedCache distributedCache)
        {
            this.fundooDbContext = fundooDbContext;
            this.inoteBl = inoteBl;
            this.distributedCache = distributedCache;
            this.memoryCache = memoryCache;
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
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooDbContext.notes.FirstOrDefault(x => x.UserId == UserId && x.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This noteID is doesn't exist" });
                }
                await this.inoteBl.DeleteNote(UserId, noteId);
                return this.Ok(new { success = true, message = "Note Deleted Successfully" });
            }
            catch (Exception ex)
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
        [HttpPut("changeColor/{noteId}/{color}")]

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
                return this.Ok(new { success = true, message = "color change succesfully" });

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
        public async Task<ActionResult> ArchiveNote( int noteId)
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
        [HttpPut("UpdateNote/{noteId}")]
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
        [HttpPut("Trash/{noteId}")]
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


        [Authorize]
        [HttpGet("GetAllNoteRadies")]
        public async Task<IActionResult> GetAllNotes_ByRedisCache()
        {
            try
            {
                var cachekey = "noteList";
                string serializedNoteList;
                var noteList = new List<Note>();
                var redisnoteList = await distributedCache.GetAsync(cachekey);
                if (redisnoteList != null)
                {
                    serializedNoteList = Encoding.UTF8.GetString(redisnoteList);
                    noteList = JsonConvert.DeserializeObject<List<Note>>(serializedNoteList);
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                    int userID = Int32.Parse(userid.Value);
                    noteList = await inoteBl.GetAllNote(userID);

                    serializedNoteList = JsonConvert.SerializeObject(noteList);
                    redisnoteList = Encoding.UTF8.GetBytes(serializedNoteList);

                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    await distributedCache.SetAsync(cachekey, redisnoteList, option);
                }
                return this.Ok(new { success = true, message = "Get note Successfull !!", data = noteList });

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
