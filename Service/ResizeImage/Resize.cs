using System.Drawing;
using System.Drawing.Imaging;

namespace Service.ResizeImage
{
    public class Resize : IResize
    {
        public byte[] ImageResizer(byte[] imageBytes, int height, int width, ImageFormat imageFormat)
        {
            MemoryStream memoryStreamForImageSize = new(imageBytes);
            Image image = Image.FromStream(memoryStreamForImageSize);
            Bitmap bitmap = new Bitmap(image, width, height);
            MemoryStream memoryStreamForFormat = new();
            bitmap.Save(memoryStreamForFormat, imageFormat);

            return memoryStreamForFormat.ToArray();
        }
    }
}
