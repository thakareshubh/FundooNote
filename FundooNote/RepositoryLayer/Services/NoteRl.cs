using CommonLayer.Users;
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
    }
}
