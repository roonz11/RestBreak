using System;

namespace RestTray.Models
{
    public class Session
    {
        public int Id { get; set; }
        public TimeSpan ActiveTime { get; set; }
        public TimeSpan RestTime { get; set; }
        public DateTime Date { get; set; }
    }
}
