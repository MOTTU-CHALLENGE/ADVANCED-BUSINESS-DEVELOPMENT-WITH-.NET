using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
namespace az_function;

public class IotDataForwarder
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public IotDataForwarder(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = loggerFactory.CreateLogger<IotDataForwarder>();
    }

    [Function("IotDataForwarder")]
    public async Task Run(
        [EventHubTrigger("messages/events", Connection = "IoTHubConnection")] IEnumerable<string> eventData, FunctionContext context)
    {
        _logger.LogInformation($"Mensagem recebida do IoT Hub: {eventData}");

        try
        {
            foreach (var msg in eventData)
            {
                try
                {
                    _logger.LogInformation($"Processando mensagem: {msg}");

                    var doc = JsonDocument.Parse(msg);

                    int idIot = 0;
                    var iotElement = doc.RootElement.GetProperty("IOT");
                    if (iotElement.ValueKind == JsonValueKind.Number)
                        idIot = iotElement.GetInt32();
                    else if (iotElement.ValueKind == JsonValueKind.String)
                        idIot = int.TryParse(iotElement.GetString(), out var parsedId) ? parsedId : 0;

                    if (!doc.RootElement.TryGetProperty("antenas", out var antenas))
                    {
                        _logger.LogWarning("Nenhuma antena encontrada.");
                        continue;
                    }

                    foreach (var antena in antenas.EnumerateArray())
                    {
                        var bssid = antena.GetProperty("BSSID").GetString();
                        var rssi = antena.GetProperty("RSSI").GetDouble();

                        var payload = new RegistroDto
                        {
                            IdIot = idIot,
                            Bssid = bssid ?? "",
                            Rssi = rssi
                        };

                        string json = JsonSerializer.Serialize(payload);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var apiUrl = "https://wa-challenge-mottu.azurewebsites.net/api/v2/RegistroSinalApiV";

                        var response = await _httpClient.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                            _logger.LogInformation($"Enviado com sucesso: {json}");
                        else
                            _logger.LogError($"Falha ao enviar: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao processar mensagem: {ex.Message}\n{ex.StackTrace}");
                }
            }


        }
        catch (JsonException e)
        {
            _logger.LogError($"Erro ao processar JSON: {e.Message}");
        }
    }
}
public class RegistroDto
{
    public int IdIot { get; set; }
    public string Bssid { get; set; } = string.Empty;
    public double Rssi { get; set; }
}

