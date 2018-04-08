using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace com.company.todo.Models
{
    public class TodoItem 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }
        
        public bool Status { get; set; }

        public byte[] Imagen { get; set; }

        [Ignore]
        public ImageSource ImagenSource { get; set; }
    }
}
    