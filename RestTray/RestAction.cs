using RestTray.WindowsActions;
using System.Windows;

namespace RestTray
{
    public class RestAction
    {        
        public void TakeRest()
        {            
            DllComands.SendMessage(Monitor.HWND_BROADCAST, 
                Monitor.WM_SYSCOMMAND, 
                Monitor.SC_MONITORPOWER, 
                (int)Monitor.MonitorState.OFF);
            //DllComands.LockWorkStation();
            //DllComands.SetSuspendState(false, true, true);
        }
    }
}
