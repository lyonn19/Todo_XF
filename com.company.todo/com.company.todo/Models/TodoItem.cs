using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.ViewModels;
using com.company.todo.ViewModels.Base;
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

        public DateTime UpdatedAt { get; set; }

        public bool Status { get; set; }
        
        public byte[] Imagen { get; set; }

        [Ignore]
        public ImageSource ImagenSource { get; set; }
    }

    public class ItemUpdate
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
    