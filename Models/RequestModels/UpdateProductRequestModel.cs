using Work_with_orders.Enums;

namespace Work_with_orders.Models.RequestModels;

public class UpdateProductRequestModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
}