using CommonLayer.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRl : InoteRl
    {
        FundooDbContext fundooDbContext;

        public IConfiguration configuration { get; }

        public NoteRl(FundooDbContext fundooDbContext,IConfiguration configuration)
        {
            this.fundooDbContext = fundooDbContext;
            this.configuration = configuration;
        }
        public async Task AddNote(int userId, NoteModel noteModel)
        {
            try 
            {
                Note noteData = new Note();
                noteData.UserId = userId;
                noteData.Title = noteModel.Title;
                noteData.Description = noteModel.Description;
                noteData.color = noteModel.color;

                noteData.isPin = false;
                noteData.isReminder = false;
                noteData.isArchieve = false;
                noteData.isTrash = false;

                noteData.RegisterDate = DateTime.Now;
                noteData.ModifyDate = DateTime.Now;

                fundooDbContext.Add(noteData);
                await fundooDbContext.SaveChangesAsync();
            }
            catch (Exception )
            {
                throw;
            }
        }

        //Delete Note
        public bool DeleteNote(int noteId)
        {
            Note note = fundooDbContext.notes.FirstOrDefault(e => e.NoteId == noteId);
            if (note == null)
            {
                fundooDbContext.Remove(note);
                fundooDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        //Change color of note
        public async Task ChangeColor(int userId, int noteId, string color)
        {
            try
            {
                var note=fundooDbContext.notes.FirstOrDefault(x=> x.UserId == userId && x.NoteId == noteId);
                if(note != null)
                {
                    note.color=color;
                    await fundooDbContext.SaveChangesAsync();   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Archieve note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task ArchiveNote(int userId, int noteId)
        {
            try
            {
                var note = fundooDbContext.notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);
                if (note != null)
                {
                    if (note.isArchieve == true)
                    {
                        note.isArchieve = false;
                    }
                    else
                    {
                        note.isArchieve = true;
                    }

                }
                await fundooDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Update Note
        public async Task<Note> UpdateNote(int noteId, NoteUpDateModel noteUpdateModel)
        {
            try
            {
                var note=fundooDbContext.notes.FirstOrDefault(e => e.NoteId == noteId );

                if(note != null)
                {
                    note.Title=noteUpdateModel.Title;
                    note.Description=noteUpdateModel.Description;
                    note.isReminder = noteUpdateModel.isReminder;
                    note.isArchieve = noteUpdateModel.isArchieve;
                    note.color = noteUpdateModel.color;
                    note.isPin = noteUpdateModel.isPin;
                    note.isTrash = noteUpdateModel.isTrash;

                    await fundooDbContext.SaveChangesAsync();
                }
                return await fundooDbContext.notes.Where(u => u.UserId == u.UserId && u.NoteId == noteId).Include(u => u.User).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get note
        public async Task<Note> GetNote(int noteId)
        {
            try
            {
                return await fundooDbContext.notes.Where(u => u.NoteId == noteId && u.UserId == u.UserId).Include(u => u.User).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //Trash note

        public async Task<Note> TrashNote(int userId, int noteId)
        {
            try
            {
                var note = fundooDbContext.notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);
                if (note != null)
                {
                    if (note.isTrash == true)
                    {
                        note.isTrash = false;
                    }
                    else
                    {
                        note.isTrash=true;
                    }
                  
                }
                await fundooDbContext.SaveChangesAsync();
                return await fundooDbContext.notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Note>> GetAllNote(int userId)
        {
            try
            {
                return await fundooDbContext.notes.Where(u => u.UserId == userId).Include(u => u.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public  async Task<List<Note>> GetAllNoteRedis(int userId)
        {
            try
            {
                return await fundooDbContext.notes.Where(u => u.UserId == userId).Include(u => u.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
