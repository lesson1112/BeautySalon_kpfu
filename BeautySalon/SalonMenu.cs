using BeautySalon;
using System;

public class SalonMenu
{
    private SalonManager manager = new SalonManager();

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("САЛОН КРАСОТЫ");
            Console.WriteLine("1. Посмотреть услуги");
            Console.WriteLine("2. Запись клиента");
            Console.WriteLine("3. Статистика клиентов");
            Console.WriteLine("4. Добавить услугу");
            Console.WriteLine("5. Посмотреть расписание");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    ShowServices();
                    break;
                case "2":
                    BookAppointment();
                    break;
                case "3":
                    ShowStatistics();
                    break;
                case "4":
                    AddService();
                    break;
                case "5":
                    ShowSchedule();
                    break;
                case "6":
                    return;
            }

            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();
        }
    }

    private void ShowServices()
    {
        foreach (var service in manager.GetAllServices())
        {
            Console.WriteLine($"{service.Id}. {service.Name} ({service.Duration} мин) - {service.Price} руб");
        }
    }

    private void BookAppointment()
    {
        Console.Write("Телефон клиента: ");
        string phone = Console.ReadLine() ?? "";

        var client = manager.FindClientByPhone(phone);

        if (client == null)
        {
            Console.Write("Имя клиента: ");
            string name = Console.ReadLine() ?? "";
            client = manager.RegisterClient(name, phone);
        }

        ShowServices();

        Console.Write("Выберите услугу: ");
        if (!int.TryParse(Console.ReadLine(), out int index))
        {
            Console.WriteLine("Ошибка ввода.");
            return;
        }

        var services = manager.GetAllServices();

        if (index < 1 || index > services.Count)
        {
            Console.WriteLine("Неверный номер услуги.");
            return;
        }

        var service = services[index - 1];

        Console.Write("Введите дату (дд-мм-гггг чч:мм): ");
        string input = Console.ReadLine() ?? "";

        if (!DateTime.TryParse(input, out DateTime time))
        {
            Console.WriteLine("Неверный формат даты.");
            return;
        }

        foreach (var master in manager.GetAllMasters())
        {
            if (master.AddBooking(time))
            {
                client.BookService(service, time);
                Console.WriteLine($"Запись создана. Мастер: {master.Name}");
                return;
            }
        }

        Console.WriteLine("Нет свободных мастеров.");
    }

    private void ShowStatistics()
    {
        foreach (var client in manager.GetAllClients())
        {
            Console.WriteLine($"{client.Name} - потрачено {client.CalculateTotalCost()} руб");
        }
    }

    private void AddService()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Цена: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            return;

        Console.Write("Длительность (мин): ");
        if (!int.TryParse(Console.ReadLine(), out int duration))
            return;

        int newId = manager.GetAllServices().Count + 1;

        manager.AddService(new Service(newId, name, price, duration));

        Console.WriteLine("Услуга добавлена.");
    }

    private void ShowSchedule()
    {
        var clients = manager.GetAllClients();

        if (clients.Count == 0)
        {
            Console.WriteLine("Записей нет.");
            return;
        }

        foreach (var client in clients)
        {
            var bookings = client.GetBookings();

            foreach (var booking in bookings)
            {
                Console.WriteLine($"{booking.time:dd-MM-yyyy HH:mm} | {client.Name} | {booking.service.Name}");
            }
        }
    }
}
