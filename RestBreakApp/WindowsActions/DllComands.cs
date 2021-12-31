using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RestBreakService.WindowsActions
{
    public static class DllComands
    {
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
    }
}
