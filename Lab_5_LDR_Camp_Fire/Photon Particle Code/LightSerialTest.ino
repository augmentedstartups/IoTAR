//Created By    : Ritesh Kanjee
//Project       : Light Intensity Serial Test
//Date          : 09 April 2017
//Description   : Prints light intensity via serial (May require calibration)
//Website       : www.ArduinoStartups.com

int analogPin = A0;
int LightInput,LightIntensity;

void setup() {
	Serial.begin(9600); //Start Serial
}

void loop() {
    LightInput = analogRead(analogPin);                 //Read Analog Pin
    LightIntensity = map(LightInput, 20, 4080, 0, 100); //Convert our light input into Percentage
	Serial.printlnf("Light Intensity is at %d %", LightIntensity);
	delay(100);
}