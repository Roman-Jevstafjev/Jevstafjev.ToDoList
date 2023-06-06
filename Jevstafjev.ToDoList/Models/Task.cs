namespace Jevstafjev.ToDoList.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public bool MarkedAsDone { get; set; }
    }
}
