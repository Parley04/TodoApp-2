using Todo.Backend.Domain.Abstractions;

namespace Todo.Backend.Domain.Entities
{
    public class Tag:Entity
    {
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
