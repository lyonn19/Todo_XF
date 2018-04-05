using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.todo.Models
{
    public class Task
    {
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }
        
        public bool Status { get; set; }

        public byte[] Imagen { get; set; }
    }
}
