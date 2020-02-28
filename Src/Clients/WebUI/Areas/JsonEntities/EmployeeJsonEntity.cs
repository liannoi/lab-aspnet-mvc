using Newtonsoft.Json;

namespace HumanResources.WebUI.Areas.JsonEntities
{
    public partial class EmployeeJsonEntity
    {
        [JsonProperty("EmployeeId")] public long EmployeeId { get; set; }
    }

    public partial class EmployeeJsonEntity
    {
        public static EmployeeJsonEntity FromJson(string json)
        {
            return JsonConvert.DeserializeObject<EmployeeJsonEntity>(json, Converter.Settings);
        }
    }
}