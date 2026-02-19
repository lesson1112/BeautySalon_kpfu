using System;
using System.Collections.Generic;

public class Client
{
    public string Name { get; }
    public string Phone { get; }

   
    private List<(Service service, DateTime time)> bookings = new();//

    public Client(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }

    public bool BookService(Service service, DateTime time)
    {
        foreach (var booking in bookings)
        {
            if (booking.time == time)
                return false;
        }

        bookings.Add((service, time));
        return true;
    }

    public List<(Service service, DateTime time)> GetBookings()
    {
        return bookings;
    }

    public decimal CalculateTotalCost()
    {
        decimal total = 0;

        foreach (var booking in bookings)
        {
            total += booking.service.Price;
        }

        return total;
    }
}
