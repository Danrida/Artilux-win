using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArtiluxEOL.Framework
{
    public static class VisualExtensions
    {
        /// <summary>
        /// Returns System DPI X.
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static double GetDpiX(this Visual visual) {
            PresentationSource source = PresentationSource.FromVisual(visual);
            double dpiX = 96.0;
            if (source != null)
                return dpiX * source.CompositionTarget.TransformToDevice.M11;
            return dpiX;
        }
        /// <summary>
        /// Returns System DPI Y.
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static double GetDpiY(this Visual visual) {
            PresentationSource source = PresentationSource.FromVisual(visual);
            double dpiY = 96.0;
            if (source != null)
                return dpiY * source.CompositionTarget.TransformToDevice.M22;
            return dpiY;
        }

        public static ImageBrush CreateBrush(this Visual visual, int width, int height) {
            var target = new RenderTargetBitmap(width, height, visual.GetDpiX(), visual.GetDpiY(), PixelFormats.Pbgra32);
            target.Render(visual);
            ImageBrush brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }
    }
}
