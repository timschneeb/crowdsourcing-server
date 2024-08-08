using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GalaxyBudsCrowdsourcing.Converters
{
    public class EnumArrayConverter<T>(ConverterMappingHints? mappingHints = null) : ValueConverter<T[], string>(
        x => ToCsvString(x), x => ToEnumArray(x), mappingHints)
    {
        private static T[] ToEnumArray(string enums)
        {
            return enums.Split(',')
                .ToList()
                .Select(x => Enum.Parse(typeof(T), x))
                .Where(x => x is T).Cast<T>()
                .ToArray();
        }

        private static string ToCsvString(T[] enums)
        {
            return string.Join(',', enums.Cast<int>().Select(x => x.ToString()).ToArray());
        }
    }
}