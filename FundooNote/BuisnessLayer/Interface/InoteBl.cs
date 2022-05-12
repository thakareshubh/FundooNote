using CommonLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface InoteBl
    {
        Task AddNote(int userId, NoteModel noteModel);
        bool DeleteNote(int noteId);

        Task ChangeColor(int userId, int noteId, string color);
    }
}
