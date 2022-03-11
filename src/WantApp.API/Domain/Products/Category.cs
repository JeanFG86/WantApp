using Flunt.Validations;

namespace WantApp.API.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; }
    public bool Active { get; set; }

    public Category(string name, string createdBy, string editedBy)
    {
        var contract = new Contract<Category>().IsNotNullOrEmpty(name, "Name");
        AddNotifications(contract);

        Name = name;
        Active = true;
        CreateBy = createdBy;
        EditedBy = editedBy;
        CreateOn = DateTime.Today;
        EditedOn = DateTime.Today;
    }
}
