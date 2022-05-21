namespace WantApp.API.Domain.Orders;

public class Order : Entity
{
    public string ClientId { get; set; }
    public List<Product> Products { get; set; }
    public decimal Total { get; set; }
    public string DeliveryAddress { get; set; }

    public Order()
    {

    }

    public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
    {
        ClientId = clientId;
        Products = products;
        DeliveryAddress = deliveryAddress;
        CreateBy = clientName;
        EditedBy = clientName;
        CreateOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        Total = 0;
        foreach (var item in products)
        {
            Total += item.Price;
        }
        Validate();
        
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "Client")
            .IsNotNull(Products, "Products");
        AddNotifications(contract);
    }
}
