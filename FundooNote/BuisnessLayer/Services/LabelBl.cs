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
    public class LabelBl:ILabelBl
    {
        ILabaleRl labelRL;
        public LabelBl(ILabaleRl labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task AddLabel(int userId, int noteId, string LabelName)
        {
            try
            {
                await this.labelRL.AddLabel(userId, noteId, LabelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task createLabel(int userId, lablePostModel lablePostModel)
        {
            try
            {
                await this.labelRL.createLabel(userId, lablePostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteLabel(int LabelId, int UserId)
        {
            try
            {
                await labelRL.DeleteLabel(LabelId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Label> UpdateLabel(int UserId, int NoteId, string LabelName)
        {
            try
            {
                return await this.labelRL.UpdateLabel(UserId, NoteId,  LabelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Label>> Getlabel(int UserId)
        {
            try
            {
                return await this.labelRL.Getlabel(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
