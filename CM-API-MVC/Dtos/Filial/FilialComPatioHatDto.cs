using CM_API_MVC.Dtos.Link;
using System.Text.Json.Serialization;

namespace CM_API_MVC.Dtos.Filial
{
    public class FilialComPatioHatDto : FilialComPatioDto
    {
        [JsonPropertyName("_links")]
        [JsonPropertyOrder(999)]
        public List<LinkDto> Links { get; set; } = new();
    }
}
