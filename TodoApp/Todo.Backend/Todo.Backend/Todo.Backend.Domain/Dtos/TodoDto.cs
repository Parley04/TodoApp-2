namespace Todo.Backend.Domain.Dtos
{
    public class TodoDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BackgroundColor { get; set; }
        public bool IsCompleted { get; set; }


    }
}
