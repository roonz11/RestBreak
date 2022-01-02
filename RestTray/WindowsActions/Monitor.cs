namespace RestTray.WindowsActions
{
    public static class Monitor
    {
        public static int HWND_BROADCAST = 0xFFFF;
        public static int SC_MONITORPOWER = 0xF170;
        public static int WM_SYSCOMMAND = 0x0112;

        public enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
    }
}
