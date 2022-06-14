using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Users
{
    public class lablePostModel
    {
        [Required]
        public string lableName { get; set; }
    }
}
