namespace Inventory.Application.Results;

public class Result
{
    protected Result(bool isSuccess, InventoryError error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public InventoryError Error { get; }

    public static Result Success() => new(true, InventoryError.None);

    public static Result Failure(InventoryError error) => new(false, error);
}


public sealed class Result<T> : Result
{
    private Result(T? value, bool isSuccess, InventoryError error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value)
        => new(value, true, InventoryError.None);

    public new static Result<T> Failure(InventoryError error)
        => new(default, false, error);
}
