using RestTray.WindowsActions;

namespace RestTray.Services
{
    public class RestAction : IRestAction
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
