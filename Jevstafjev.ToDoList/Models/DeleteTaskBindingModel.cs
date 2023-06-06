using System.ComponentModel.DataAnnotations;

namespace Jevstafjev.ToDoList.Models
{
    public class DeleteTaskBindingModel
    {
        [Required]
        public string Id { get; set; } = null!;
    }
}
