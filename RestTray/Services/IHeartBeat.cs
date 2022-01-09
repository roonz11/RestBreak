namespace RestTray.Services
{
    public interface IHeartBeat
    {
        void Restart();
        void Start();
        void Stop();
    }
}