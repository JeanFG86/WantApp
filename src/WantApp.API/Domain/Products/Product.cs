namespace WantApp.API.Domain.Products;

public class Product : Entity
{
    private string userId;

    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Description { get; set; }
    public bool HasStock { get; set; }
    public bool Active { get; set; }

    public Product()
    {

    }
    public Product(string name, Category category, string description, bool hasStock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;

        CreateBy = createdBy;
        EditedBy = createdBy;
        CreateOn = DateTime.Now;
        EditedOn = DateTime.Now;
        //CategoryId = category.Id;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, Description);
            
        AddNotifications(contract);
    }
}
