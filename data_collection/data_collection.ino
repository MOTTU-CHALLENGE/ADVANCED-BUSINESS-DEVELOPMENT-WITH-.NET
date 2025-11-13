#include <WiFi.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>
#include <WiFiClientSecure.h>

// ======== CONFIG Wi-Fi =========
const char* ssid = "Benedetto_2G";
const char* password = "enzokekoedna";

// ======== CONFIG IoT Hub =========
const char* mqtt_broker = "hi-challenge-mottu.azure-devices.net";
const char* device_id = "1";
const char* sas_token = "SharedAccessSignature sr=hi-challenge-mottu.azure-devices.net%2Fdevices%2F1&sig=gpzDi%2Bu8WZeN2ueeRu%2B%2FLNOLA%2FwlnnUO%2FXYk%2Fg8%2BNxM%3D&se=1762120366";
const int mqtt_port = 8883;
const char* mqtt_client_id = device_id;
const char* mqtt_username = "hi-challenge-mottu.azure-devices.net/1/?api-version=2021-04-12";

// Tópico de publicação no Azure IoT Hub
const char* publish_topic = "devices/1/messages/events/";

WiFiClientSecure wifiClient;
PubSubClient client(wifiClient);

void setup() {
  Serial.begin(115200);

  WiFi.begin(ssid, password);
  Serial.print("Conectando ao Wi-Fi");
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("\nWi-Fi conectado!");

  // Configura certificação TLS
  wifiClient.setInsecure();

  client.setServer(mqtt_broker, mqtt_port);
  connectToMQTT();
}

void connectToMQTT() {
  Serial.println("Conectando ao IoT Hub MQTT...");
  while (!client.connected()) {
    if (client.connect(mqtt_client_id, mqtt_username, sas_token)) {
      Serial.println("✅ Conectado ao Azure IoT Hub via MQTT!");
    } else {
      Serial.print("❌ Falha: ");
      Serial.println(client.state());
      delay(2000);
    }
  }
}

void loop() {
  if (!client.connected()) {
    connectToMQTT();
  }

  StaticJsonDocument<256> doc;
  doc["IOT"] = atoi(device_id);

  JsonArray antenas = doc.createNestedArray("antenas");
  JsonObject ant = antenas.createNestedObject();
  ant["BSSID"] = "24:FD:0D:D7:17:4A";
  ant["RSSI"] = -65.5;

  char buffer[256];
  serializeJson(doc, buffer);
  Serial.println("Enviando JSON:");
  Serial.println(buffer);

  bool sent = client.publish(publish_topic, buffer);
  if (sent) {
    Serial.println("✅ Enviado com sucesso!");
  } else {
    Serial.println("❌ Falha ao enviar!");
  }

  delay(10000);
}
