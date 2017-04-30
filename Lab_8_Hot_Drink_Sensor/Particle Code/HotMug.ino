//Created By    : Ritesh Kanjee
//Project       : Hot Mug Sensor Project
//Date          : 30 April 2017
//Description   : Sends the temperature values to the Particle Cloud (May require calibration)
//Website       : www.ArduinoStartups.com

int analogPin = A0;
double TemperatureInput,TemperatureCalibrated;

void setup() {
	Serial.begin(9600); //Start Serial
	Particle.variable("Temperature", TemperatureCalibrated);
}

void loop() {
    TemperatureInput = analogRead(analogPin);                 //Read Analog Pin
    TemperatureCalibrated = (TemperatureInput/2110)*22;       //Simple Linear Calibration
	Serial.println(TemperatureCalibrated);
	delay(10);
}