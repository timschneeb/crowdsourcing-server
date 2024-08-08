using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GalaxyBudsCrowdsourcing.Converters
{
    public class EnumConverter<T>(ConverterMappingHints? mappingHints = null) : ValueConverter<T, string>(
        x => ToString(x), x => ToEnum(x), mappingHints)
    {
        private static T ToEnum(string @enum)
        {
            if(Enum.Parse(typeof(T), @enum) is T t)
            {
                return t;
            }

            throw new FormatException("Enum has invalid format");
        }

        private static string ToString(T @enum)
        {
            return @enum.ToString();
        }
    }
}