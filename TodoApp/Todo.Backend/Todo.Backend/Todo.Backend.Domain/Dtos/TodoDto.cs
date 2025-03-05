namespace Todo.Backend.Domain.Dtos
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public List<TagDto>? Tags { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BackgroundColor { get; set; }
        public bool IsCompleted { get; set; }
    }
}
