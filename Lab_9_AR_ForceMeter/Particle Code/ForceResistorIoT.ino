//Created By    : Ritesh Kanjee
//Project       : Force Sensitive Resistor Project (IoTAR)
//Date          : 1 May 2017
//Description   : Sends the Force values to the Particle Cloud (May require calibration)
//Website       : www.ArduinoStartups.com

int analogPin = A0;											//Declare Pins and Variables
double ForceInput,ForcePercent;

void setup() {
	Serial.begin(9600); //Start Serial
	Particle.variable("Force", ForcePercent);			   //Expose a variable through the Cloud
}

void loop() {
    ForceInput = analogRead(analogPin);                    //Read Analog Pin
    ForcePercent = map(ForceInput, 0, 3980, 0, 100);       //Express analog values as a percentage (You may need to Calibrate these Values)
	Serial.println(ForcePercent);
	delay(10);
}