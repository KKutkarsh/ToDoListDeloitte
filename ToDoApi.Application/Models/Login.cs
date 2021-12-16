
using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Application.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(1)]
        public string Password { get; set; }
    }
}
