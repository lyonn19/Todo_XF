using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.todo.DependencyService
{
    /// <summary>
    /// Interface for Share Content using in DependencyService
    /// </summary>
    public interface IShareServices
    {
        void ShareTodoContent(string content);
    }
}
