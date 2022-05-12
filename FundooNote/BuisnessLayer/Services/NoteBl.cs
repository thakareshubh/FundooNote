using BuisnessLayer.Interface;
using CommonLayer.Users;
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
    }
}
