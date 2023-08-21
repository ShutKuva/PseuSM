namespace Tools.Abstractions
{
    public interface ITimeService
    {
        ValueTask<DateTime> GetCurrentDateTime(); 
    }
}
