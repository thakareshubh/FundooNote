using BuisnessLayer.Interface;
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

       
    }
}
