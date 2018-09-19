using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCC
{
    // Screen size
    public struct SIZE
    {
        public int cx;
        public int cy;
    }

    // User32 necessary API calls
    public class PlatformInvokeUSER32
    {
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        public PlatformInvokeUSER32()
        {
        }
    }

    // GDI32 necessary API calls
    public class PlatformInvokeGDI32
    {
        public const int SRCCOPY = 13369376;

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        public PlatformInvokeGDI32()
        {
        }
    }

    public class DesktopBitmapCapture
    {
        public static Bitmap GetCursorAreaBitmap(int areaWidth, int areaHeight)
        {
            IntPtr bitmapHandle;
            IntPtr desktopDeviceContextHandle = PlatformInvokeUSER32.GetDC(PlatformInvokeUSER32.GetDesktopWindow());
            IntPtr compatibleDesktopDeviceContextInMemoryHandle = PlatformInvokeGDI32.CreateCompatibleDC(desktopDeviceContextHandle);
            bitmapHandle = PlatformInvokeGDI32.CreateCompatibleBitmap(desktopDeviceContextHandle, areaWidth, areaHeight);

            if (bitmapHandle != IntPtr.Zero)
            {
                IntPtr oldBitmapHandle = (IntPtr)PlatformInvokeGDI32.SelectObject(compatibleDesktopDeviceContextInMemoryHandle, bitmapHandle);
                PlatformInvokeGDI32.BitBlt(compatibleDesktopDeviceContextInMemoryHandle, 0, 0, areaWidth, areaHeight, desktopDeviceContextHandle, Cursor.Position.X - (areaWidth / 2), Cursor.Position.Y - (areaHeight / 2), PlatformInvokeGDI32.SRCCOPY);
                PlatformInvokeGDI32.SelectObject(compatibleDesktopDeviceContextInMemoryHandle, oldBitmapHandle);
                PlatformInvokeGDI32.DeleteDC(compatibleDesktopDeviceContextInMemoryHandle);
                PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.GetDesktopWindow(), desktopDeviceContextHandle);
                Bitmap bitmap = System.Drawing.Image.FromHbitmap(bitmapHandle);
                PlatformInvokeGDI32.DeleteObject(bitmapHandle);
                GC.Collect();
                return bitmap;
            }
            return null;
        }
    }
}
