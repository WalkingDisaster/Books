using System;

namespace Books.Logic
{
    public interface IDateService
    {
        DateTime FirstDayOfCurrentWeek();
        int CurrentYear { get; }
    }
}