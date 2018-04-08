using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.todo.DependencyService
{
    /// <summary>
    /// Interface for Image Resize using in DependencyService
    /// </summary>
    public interface IImageResize
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
