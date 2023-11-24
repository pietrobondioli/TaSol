#include "Adafruit_HTU21DF.h"
#include <Adafruit_Sensor.h>
#include <PubSubClient.h>
#include <WiFi.h>
#include <Wire.h>

Adafruit_HTU21DF htu = Adafruit_HTU21DF();

#define LDR_PIN 34
#define RAIN_SENSOR_PIN 35

const char *ssid = "wifi";
const char *password = "";
const char *mqttServer = "192.168.15.146";
const int mqttPort = 1883;
const char *authKey = "";

WiFiClient espClient;
PubSubClient client(espClient);

void setup() {
  Serial.begin(115200);

  Wire.begin();

  if (!htu.begin()) {
    Serial.println("Couldn't find HTU21D, check wiring!");
  }

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.println("Connecting to WiFi...");
  }
  Serial.println("Connected to WiFi");

  client.setServer(mqttServer, mqttPort);
}

void connectToMqtt() {
  while (!client.connected()) {
    Serial.println("Connecting to MQTT...");
    if (client.connect("ESP32Client")) {
      Serial.println("Connected to MQTT");
    } else {
      Serial.print("Failed with state ");
      Serial.print(client.state());
      delay(2000);
    }
  }
}

void loop() {
  if (!client.connected()) {
    connectToMqtt();
  }
  client.loop();

  Serial.println("--------");

  // HTU21D sensor (humidity and temperature)
  float humidity = htu.readHumidity();
  float temp_h = htu.readTemperature();
  Serial.print(F("Humidity (HTU21D): "));
  Serial.print(humidity);
  Serial.println(F("%"));
  Serial.print(F("Temperature (HTU21D): "));
  Serial.print(temp_h);
  Serial.println(F("°C"));

  // Light sensor
  int lightLevel = analogRead(LDR_PIN);
  Serial.print(F("Light Level: "));
  Serial.println(lightLevel);

  // Rain sensor
  int rainLevel = analogRead(RAIN_SENSOR_PIN);
  Serial.print(F("Rain Level: "));
  Serial.println(rainLevel);

  Serial.println("--------");
  char payload[256];
  sprintf(payload,
          "{ \"Humidity\": %.2f, \"Temperature\": %.2f, \"LightLevel\": %d, "
          "\"RainLevel\": %d, \"AuthToken\": \"%s\" }",
          humidity, temp_h, lightLevel, rainLevel, authKey);
  client.publish("TaSol", payload);

  delay(5000);

  delay(5000);
}
