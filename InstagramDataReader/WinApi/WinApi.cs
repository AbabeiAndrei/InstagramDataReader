using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstagramDataReader
{
    public static class WinApi
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, int pszSubIdList);

        public static void SetExplorerTheme(Control control)
        {
            SetWindowTheme(control.Handle, "Explorer", 0);
        }
    }
}
