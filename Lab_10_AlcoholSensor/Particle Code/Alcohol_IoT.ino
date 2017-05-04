//Created By    : Ritesh Kanjee
//Project       : Alcohol Sensor Project (IoTAR)
//Date          : 3 May 2017
//Description   : Sends the Alcohol gas values to the Particle Cloud (May require calibration)
//Website       : www.ArduinoStartups.com

int analogPin = A0;											//Declare Pins and Variables
double AlcoholInput,AlcoholPercent;

void setup() {
	Serial.begin(9600); //Start Serial
	Particle.variable("Alcohol", AlcoholPercent);			   //Expose a variable through the Cloud
}

void loop() {
    AlcoholInput = analogRead(analogPin);                    //Read Analog Pin
    AlcoholPercent = map(AlcoholInput, 1710, 3850, 0, 100);       //Express analog values as a percentage (You may need to Calibrate these Values)
	Serial.println(AlcoholPercent);
	delay(100);
}