using MongoDB.Bson.Serialization.Attributes;

namespace CM_API_MVC.Dtos.Registro
{
    public class RegistroDto
    {
        public int IdIot { get; set; }

        public string Bssid { get; set; }

        public double Rssi { get; set; }
    }
}
