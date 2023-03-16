using Play.Common;

namespace Play.Catalog.Service.Entities;

public class Item : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}