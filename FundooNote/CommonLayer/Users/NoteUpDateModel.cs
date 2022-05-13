using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Users
{
    public class NoteUpDateModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string color { get; set; }

        public bool isPin { get; set; }
        public bool isReminder { get; set; }
        public bool isArchieve { get; set; }
        public bool isTrash { get; set; }

    }
}
