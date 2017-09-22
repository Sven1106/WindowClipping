using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace WindowClipping
{
    class Program
    {

        #region Import Window Functions From DLL
        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int maxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        #region Mouse Cursor
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ClipCursor(ref Rectangle rcClip);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);


        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        } 


        #endregion

        #endregion


        /// <summary> 
        /// Copies the text of the specified window's title bar (if it has one) into a buffer.  
        /// </summary>
        /// <param name="hWnd"> A handle to the window or control containing the text.</param>
        public static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);//How does GetWindowText write to Builder?
                return builder.ToString();
            }


            return String.Empty;
        }


        static void Main()
        {
            Rectangle rect = new Rectangle();
            
            IntPtr hWnd = new IntPtr();

            while (true)
            {
                //ClipCursor(ref rOldClip);
                GetWindowRect(hWnd, out rect);//based on the ForegroundWindow
                hWnd = GetForegroundWindow();
                Point cursorPos = GetMousePosition();
                int x = (int)cursorPos.X; //(int)converts from Point to int
                int y = (int)cursorPos.Y;

                //Console.WriteLine("x: {0},y: {1}", x, y);
                Console.WriteLine(rect.Width);
                Console.WriteLine(rect.Height);
                //    Console.WriteLine(GetWindowText(hWnd));
                Thread.Sleep(1000);
            }


        }

    }
}
