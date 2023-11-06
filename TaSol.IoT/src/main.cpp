#include "Adafruit_HTU21DF.h"
#include <Adafruit_Sensor.h>
#include <Wire.h>

Adafruit_HTU21DF htu = Adafruit_HTU21DF();

#define LDR_PIN 34
#define RAIN_SENSOR_PIN 35

void setup() {
  Serial.begin(115200);

  Wire.begin();

  if (!htu.begin()) {
    Serial.println("Couldn't find HTU21D, check wiring!");
  }
}

void loop() {
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

  delay(5000);
}
