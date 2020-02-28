using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HumanResources.WebUI.Areas.JsonEntities
{
    public static class Serialize
    {
        public static string ToJson(this EmployeeJsonEntity self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            }
        };
    }
}