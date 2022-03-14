using Flunt.Validations;

namespace WantApp.API.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public Category()
    {

    }

    public Category(string name, string createdBy, string editedBy)
    {        
        Name = name;
        Active = true;
        CreateBy = createdBy;
        EditedBy = editedBy;
        CreateOn = DateTime.Today;
        EditedOn = DateTime.Today;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;

        Validate();
    }
}
