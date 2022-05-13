using BuisnessLayer.Interface;
using CommonLayer.Users;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class NoteBl:InoteBl
    {
        InoteRl inoteRl;
        public NoteBl(InoteRl inoteRl)
        {
            this.inoteRl = inoteRl;
        }

        //Add note
        public async Task AddNote(int userId, NoteModel noteModel)
        {
            try
            {
                await inoteRl.AddNote(userId, noteModel);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Archive Notes
        public async Task ArchiveNote(int userId, int noteId)
        {
            try
            {
                await this.inoteRl.ArchiveNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Change color of note
        public async Task ChangeColor(int userId, int noteId, string color)
        {
            try
            {
                await this.inoteRl.ChangeColor(userId, noteId, color);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Delete Note
        public  bool DeleteNote(int noteId)
        {
            try
            {
                if( inoteRl.DeleteNote(noteId))
                {
                    return true;
                }
                    
                else
                {
                    return false;
                }
                    
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Get note
        public  async Task<Note> GetNote(int noteId)
        {
            try
            {
                return await this.inoteRl.GetNote(noteId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //Trash note
        public Task<Note> TrashNote(int userId, int noteId)
        {
            try
            {
                return this.inoteRl.TrashNote(userId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update note
        public async Task<Note> UpdateNote(int noteId, NoteUpDateModel noteUpdateModel)
        {
            try
            {
                return await this.inoteRl.UpdateNote(noteId, noteUpdateModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
