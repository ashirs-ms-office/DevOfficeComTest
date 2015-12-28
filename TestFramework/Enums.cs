using System;
using System.ComponentModel;
using System.Reflection;

namespace TestFramework
{
    public enum Product
    {
        Word,
    }

    public enum Platform
    {
        Android,
        [Description("ASP.NET MVC")] 
        DotNET,
        iOS,
        [Description("Node.js")]
        Node,
        PHP,
        Python,
        Ruby,
        Angular,
        [Description("Universal Windows Platform")]
        WindowsUniversal
    }

    public static class EnumExtension
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}
