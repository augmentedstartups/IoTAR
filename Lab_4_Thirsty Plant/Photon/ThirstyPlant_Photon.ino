//Created By: Ritesh Kanjee
//Project   : AR Thirsty Plant
//Date      : 31st April 2017
//Website   : www.ArduinoStartups.com

int t = 2000; // time delay
// Pins
int WaterLevel_pin = A0;


void setup() {
   
}

void loop() {
    // Water level Measurement
    int WaterLevel = analogRead(WaterLevel_pin);
    
    // Publish data
    Particle.publish("waterlevel", String(WaterLevel));// Publish Variable to Cloud);
    delay(t);
}
