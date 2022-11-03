using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicfyAPI.Models.User
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your name is Cannot be blank")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your Firts Cannot be blank")]
        public string LastName { get; set; }

        [MinLength(5, ErrorMessage = "Please enter a username of at least 5 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı adı Cannot be blank")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your Email Cannot be blank")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid E-Mail Address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Cannot be blank")]
        [MaxLength(32, ErrorMessage = "Please enter your password between 8 and 32 characters.")]
        [MinLength(8, ErrorMessage = "Please enter your password between 8 and 32 characters.")]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
