using System;
using System.Collections.Generic;

public class Master
{
    public string Name { get; }

    private List<DateTime> bookings = new();

    public Master(string name)
    {
        Name = name;
    }

    public bool AddBooking(DateTime time)
    {
        foreach (var booked in bookings)
        {
            if (booked == time)
                return false;
        }

        bookings.Add(time);
        return true;
    }
}
