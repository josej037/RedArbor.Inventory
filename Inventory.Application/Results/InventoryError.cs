namespace Inventory.Application.Results;

public sealed record InventoryError(string Code, string Message)
{
    public static readonly InventoryError None = new(string.Empty, string.Empty);
}
