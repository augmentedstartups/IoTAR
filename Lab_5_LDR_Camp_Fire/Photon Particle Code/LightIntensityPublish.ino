//Created By    : Ritesh Kanjee
//Project       : Light Intensity Publish
//Date          : 09 April 2017
//Description   : Prints light intensity to the cloud (May require calibration)
//Website       : www.ArduinoStartups.com

int t = 2000; // time delay
// Pins
int analogPin = A0;
int LightInput,LightIntensity;

void setup() {
   
}

void loop() {
    
    LightInput = analogRead(analogPin);                 //Read Analog Pin
    LightIntensity = map(LightInput, 20, 4080, 0, 100); //Convert our light input into Percentage
    // Publish data
    Particle.publish("Light Intensity", String(LightIntensity));// Publish Variable to Cloud);

    delay(t);
}



