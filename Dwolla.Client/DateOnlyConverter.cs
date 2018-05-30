using Newtonsoft.Json.Converters;

namespace Dwolla.Client
{
    public class DateOnlyConverter : IsoDateTimeConverter
    {
        public DateOnlyConverter() => DateTimeFormat = "yyyy-MM-dd";
    }
}