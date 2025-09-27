using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CM_API_MVC.Models
{

    public class RegistroIntensidadeSinal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdRegistro { get; set; }

        [BsonElement("ID_DISPOSITIVO")]
        public int IdIot { get; set; }

        [BsonElement("DS_ENDERECO_MAC")]
        public string Bssid { get; set; }

        [BsonElement("NR_INTENSIDADE_SINAL")]
        public double Rssi { get; set; }

        [BsonElement("DT_REGISTRO")]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
