using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class Note
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string color { get; set; }

        public bool isPin { get; set; }
        public bool isReminder { get; set; }
        public bool isArchieve { get; set; }
        public bool isTrash { get; set; }
        
        public DateTime RegisterDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime ReminderDate{ get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }


    }
}
