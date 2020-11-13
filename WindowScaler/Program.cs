using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowScaler
{
    class Program
    {
        #region Mouse Cursor
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern bool ClipCursor(ref Rectangle rcClip);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(ref Win32Point pt);


        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

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
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Form form = new Form(); 
                Point cursorPos = GetMousePosition();
                Screen[] allScreens = Screen.AllScreens;
                Screen screen = Screen.FromControl(form);

                SetCursorPos(0, 0);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine((int)cursorPos.X + ":" + (int)cursorPos.Y+ "                                      ");
                
                Thread.Sleep(100);
            }
        }
    }
}
