using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Backend.Domain.Abstractions;

namespace Todo.Backend.Domain.Entities
{
    public class TodoTag:Entity
    {
        public Guid TodoId { get; set; }
        public Guid TagId { get; set; }



        public Todo? Todo { get; set; }
        public Tag? Tag { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
