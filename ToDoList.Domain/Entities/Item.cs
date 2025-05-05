namespace ToDoList.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateOnly? DueDate { get; set; }
        public bool IsCompleted { get; set; } = default!;
    }
}
