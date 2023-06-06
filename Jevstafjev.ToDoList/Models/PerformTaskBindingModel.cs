using System.ComponentModel.DataAnnotations;

namespace Jevstafjev.ToDoList.Models
{
    public class PerformTaskBindingModel
    {
        [Required]
        public string Id { get; set; } = null!;
    }
}
