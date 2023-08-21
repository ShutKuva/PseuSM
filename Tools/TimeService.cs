using Tools.Abstractions;

namespace Tools
{
    public class TimeService : ITimeService
    {
        public ValueTask<DateTime> GetCurrentDateTime()
        {
            return new ValueTask<DateTime>(DateTime.Now);
        }
    }
}
