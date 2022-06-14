using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CommonLayer.Users
{
    public class UserPostModel
    {
        public int userId { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{4,}$", ErrorMessage = " First letter should  Cap and has minimum 4 characters")]
        public string firstName { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{4,}$", ErrorMessage = "First letter should  Cap and has minimum 4 characters")]
        public string lastName { get; set; }

        [Required]
        [RegularExpression("^[a-z]{3,}[1-9]{1,4}[@][a-z]{3,}[.][a-z]{2,}$", ErrorMessage = "Please Enter Valid Email")]
        public string email { get; set; }

        public string password { get; set; }
    }
}
