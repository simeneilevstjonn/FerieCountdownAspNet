namespace FerieCountdown.Classes.TimeHandler
{
    public class Time
    {
        public int Minutes { get; private set; }
        public int Hours { get; private set; } 
        public Time(int h, int m)
        {
            Minutes = m;
            Hours = h;
        }
        
        public Time(string time)
        {
            Hours = int.Parse(time.Substring(0, 2));
            Minutes = int.Parse(time.Substring(3, 2));
        }

        public void SetTime(int h, int m)
        {
            Minutes = m;
            Hours = h;
        }

        public override int GetHashCode() => Hours ^ Minutes;

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Time t = (Time)obj;
                return (Hours == t.Hours) && (Minutes == t.Minutes);
            }
        }

        public override string ToString() => string.Format("{0}:{1}", Hours, Minutes);


        // Operators
        public static bool operator <(Time a, Time b) => (a.Hours < b.Hours || (a.Hours == b.Hours && a.Minutes < b.Minutes));
        public static bool operator >(Time a, Time b) => (a.Hours > b.Hours || (a.Hours == b.Hours && a.Minutes > b.Minutes));
        public static bool operator ==(Time a, Time b) => (a.Hours == b.Hours && a.Minutes == b.Minutes);
        public static bool operator !=(Time a, Time b) => (a.Hours != b.Hours || a.Minutes != b.Minutes);
    }
}
