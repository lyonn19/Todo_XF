using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Templates
{
    /// <summary>
    /// Template View for render cells in Todos/Done Listview
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksTemplate : ContentView
    {
        public TasksTemplate()
        {
            InitializeComponent();
        }
    }
}