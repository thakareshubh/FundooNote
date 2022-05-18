using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface ILabelBl
    {
        Task AddLabel(int userId, int noteId, string LabelName);
    }
}
