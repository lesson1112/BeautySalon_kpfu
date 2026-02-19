public class Service
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Duration { get; }

    public Service(int id, string name, decimal price, int duration)//
    {
        Id = id;
        Name = name;
        Price = price;
        Duration = duration;
    }
}
