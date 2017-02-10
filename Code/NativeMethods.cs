using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CustomControl
{
    internal static class NativeMethods
    {
        #region Constants
        public const int HTCLIENT = 1;
        public const int WM_SETCURSOR = 0x0020;
        #endregion 

        #region Properties
        public static HandleRef NullHandleRef
        {
            get { return new HandleRef(null, IntPtr.Zero); }
        }
        #endregion

        #region Enumerations
        [Flags()]
        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000
        }

        [Flags()]
        public enum DeviceContextValues : uint
        {
            Window = 0x00000001,
            Cache = 0x00000002,
            NoResetAttrs = 0x00000004,
            ClipChildren = 0x00000008,
            ClipSiblings = 0x00000010,
            ParentClip = 0x00000020,
            ExcludeRgn = 0x00000040,
            IntersectRgn = 0x00000080,
            ExcludeUpdate = 0x00000100,
            IntersectUpdate = 0x00000200,
            LockWindowUpdate = 0x00000400,
            UseStyle = 0x00010000,
            Validate = 0x00200000,
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        };
        #endregion

        #region Imports
        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, DeviceContextValues flags);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] HandleRef hdc, [In] HandleRef hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] HandleRef hObject);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(HandleRef hWnd, HandleRef hDC);

        [DllImport("gdi32.dll")]
        public static extern bool PatBlt(HandleRef hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, TernaryRasterOperations dwRop);
        #endregion

        #region Methods
        public static IntPtr CreateBrush()
        {
            var brush = new SolidBrush(Color.Black);
            var hbrush = IntPtr.Zero;
            var fieldInfo = typeof(Brush).GetField("nativeBrush", BindingFlags.NonPublic | BindingFlags.Instance);

            return (IntPtr)fieldInfo.GetValue(brush);
        }
        #endregion
    }
}