using System;
using System.Text;

namespace Code.Runtime.Common.Extensions
{
    internal static class TypeExtensions
    {
        public static string GetBeautifulName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            var builder = new StringBuilder();
            var name = type.Name;
            var index = name.IndexOf('`');
            if (index > 0)
                name = name.Substring(0, index);

            builder.Append(name);
            builder.Append('<');

            var genericArgs = type.GetGenericArguments();
            for (int i = 0; i < genericArgs.Length; i++)
            {
                if (i > 0) 
                    builder.Append(", ");
                builder.Append(genericArgs[i].GetBeautifulName());
            }

            builder.Append('>');
            return builder.ToString();
        }
    }
}