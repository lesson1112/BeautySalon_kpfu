using BeautySalon;
using System.Collections.Generic;

public class SalonManager
{
    private List<Service> services = new List<Service>();
    private List<Client> clients = new List<Client>();
    private List<Master> masters = new List<Master>();

    public SalonManager()
    {
        services.Add(new Service(1, "Стрижка", 1500, 60));
        services.Add(new Service(2, "Окрашивание", 3000, 120));
        services.Add(new Service(3, "Маникюр", 800, 45));

        masters.Add(new Master("Анна"));
        masters.Add(new Master("Мария"));
    }

    public List<Service> GetAllServices() => services;
    public List<Client> GetAllClients() => clients;
    public List<Master> GetAllMasters() => masters;

    public void AddService(Service service)
    {
        services.Add(service);
    }

    public Client RegisterClient(string name, string phone)
    {
        Client client = new Client(name, phone);
        clients.Add(client);
        return client;
    }

    public Client? FindClientByPhone(string phone)
    {
        foreach (var client in clients)
        {
            if (client.Phone == phone)
                return client;
        }
        return null;
    }
}
