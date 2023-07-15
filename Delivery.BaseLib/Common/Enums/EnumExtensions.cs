using System.ComponentModel;

namespace Delivery.BaseLib.Common.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value, bool isLower = false)
    {
        var fi = value.GetType().GetField(value.ToString());

        var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        var description = attributes.Length > 0 ? attributes[0].Description : value.ToString();
        return isLower ? description.ToLower() : description;
    }

    public static EnumValue GetValue(this Enum value)
    {
        return new EnumValue {Value = value.ToString(), Description = value.GetDescription()};
    }

    public static IList<EnumValue> GetValues<T>()
    {
        var type = typeof(T);

        var values = Enum
            .GetNames(type)
            .Select(item => (Enum) Enum.Parse(type, item))
            .Select(enun => new EnumValue {Value = enun.ToString(), Description = enun.GetDescription()})
            .ToList();

        return values.OrderBy(x => x.Description).ToList();
    }
}