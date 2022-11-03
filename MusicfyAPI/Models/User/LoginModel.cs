using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicfyAPI.Models.User
{
    public class LoginModel
    {
        [MinLength(5,ErrorMessage = "Please enter a username of at least 5 characters.")]
        [Required(AllowEmptyStrings =false,ErrorMessage = "Username Cannot be Empty")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage = "Password Cannot be Blank")]
        [MaxLength(32,ErrorMessage = "Please enter your password between 8 and 32 characters.")]
        [MinLength(8,ErrorMessage = "Please enter your password between 8 and 32 characters.")]
        public string Password { get; set; }
    }
}
