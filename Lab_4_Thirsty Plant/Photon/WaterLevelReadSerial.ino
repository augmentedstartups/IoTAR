//Created By: Ritesh Kanjee
//Project   : AR Thirsty Plant Serial Read Test
//Date      : 31st April 2017
//Website   : www.ArduinoStartups.com
int analogPin = A0;
int WaterLevel;

void setup() {
	Serial.begin(9600);
}

void loop() {
    WaterLevel = analogRead(analogPin);
	Serial.printlnf("WaterLevel is %d", WaterLevel);
	delay(100);
}