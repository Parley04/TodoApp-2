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
        public string TodoId { get; set; } = string.Empty;
        public string TagId { get; set; } = string.Empty;
    }
}
