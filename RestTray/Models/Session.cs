using System;

namespace RestTray.Models
{
    public class Session
    {
        public int Id { get; set; }
        public double ActiveTime { get; set; }
        public double RestTime { get; set; }
        public DateTime Date { get; set; }
    }
}
