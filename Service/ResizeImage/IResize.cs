using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ResizeImage
{
    public interface IResize
    {
        public byte[] ImageResizer(byte[] imageBytes, int height, int width, ImageFormat imageFormat);
    }
}
