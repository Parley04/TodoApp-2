using Todo.Backend.Domain.Abstractions;

namespace Todo.Backend.Domain.Entities
{
    public class Todo: Entity
    {
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
        public int BackgroundColor { get; set; }
        public bool IsCompleted { get; set; }
        public List<Tag>? Tags { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive{ get; set; }= true;


    }
}
