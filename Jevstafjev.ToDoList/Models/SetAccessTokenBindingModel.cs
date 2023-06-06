using System.ComponentModel.DataAnnotations;

namespace Jevstafjev.ToDoList.Models
{
    public class SetAccessTokenBindingModel
    {
        [Required]
        public string AccessToken { get; set; } = null!;
    }
}
