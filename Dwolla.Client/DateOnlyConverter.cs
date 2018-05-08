using Newtonsoft.Json.Converters;

namespace Dwolla.Client
{
    class DateOnlyConverter : IsoDateTimeConverter
    {
        public DateOnlyConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}