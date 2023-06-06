using System.ComponentModel.DataAnnotations;

namespace Jevstafjev.ToDoList.Models
{
    public class AddTaskBindingModel
    {
        [Required]
        public string Title { get; set; } = null!;
    }
}
