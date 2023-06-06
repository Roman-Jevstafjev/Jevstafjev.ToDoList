using System.ComponentModel.DataAnnotations;

namespace Jevstafjev.ToDoList.Models
{
    public class CreateUserBindingModel
    {
        [Required]
        public string Username { get; set; } = null!;

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
    }
}
