namespace Delivery.BaseLib.Common.Enums;

public class EnumValue
{
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        return obj is EnumValue value &&
               Value == value.Value &&
               Description == value.Description;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Description);
    }
}