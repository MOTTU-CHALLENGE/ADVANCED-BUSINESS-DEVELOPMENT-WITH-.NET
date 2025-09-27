using CM_API_MVC.Dtos.Link;
using System.Text.Json.Serialization;

namespace CM_API_MVC.Dtos.Patio
{
    public class PatioComWifiHatDto : PatioComWifiDto
    {
        [JsonPropertyName("_links")]
        [JsonPropertyOrder(999)]
        public List<LinkDto> Links { get; set; } = new();

    }
}
