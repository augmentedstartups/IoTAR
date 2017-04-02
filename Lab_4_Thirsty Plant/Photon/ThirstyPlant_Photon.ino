//Created By: Ritesh Kanjee
//Project   : AR Thirsty Plant Part 2
//Date      : 31st April 2017
//Website   : www.ArduinoStartups.com

int t = 2000; // time delay
// Pins
int WaterLevel_pin = A0;
int WaterLevel_Calibrated;

void setup() {
   
}

void loop() {
    // Water level Measurement
    int WaterLevel = analogRead(WaterLevel_pin);
    WaterLevel_Calibrated = map(WaterLevel, 2210, 3220, 0, 100); 
    
    
    // Publish data
    //Particle.publish("waterlevel", String(WaterLevel));// Publish Variable to Cloud);
    Particle.publish("waterlevel", String(WaterLevel_Calibrated));// Publish Variable to Cloud);
    delay(t);
}




