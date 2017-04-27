/*  PulseSensor™ Starter Project   www.ArduinoStartups.com
Designed by Ritesh Kanjee
Github Repo: https://github.com/reigngt09/IoTAR/    
This a Particle Photon project. It's Best Way to Get Started with your PulseSensor™ & Particle Photon. 
-------------------------------------------------------------
1) This shows a live human Heartbeat Pulse. 
2) Live visualization in Arduino's Cool "Serial Plotter".
3) Blink an LED on each Heartbeat and send BPM to Unity via Partile Cloud.
4) This is the direct Pulse Sensor's Signal.  
5) A great first-step in troubleshooting your circuit and connections. 
6) "Human-readable" code that is newbie friendly." 

*/
//  Variables
int PulseSensorPurplePin0 = 0;        // Pulse Sensor PURPLE WIRE connected to ANALOG PIN 0
int LED13 = D7;   //  The on-board Arduion LED

int Signal_In;                // holds the incoming raw data. Signal value can range from 0-1024
int PulseIR;
int Threshold = 2400;            // Determine which Signal to "count as a beat", and which to ingore. 
// Variables will change:
int PulseCounter = 0;   // counter for the number of button presses
int PulseState = 0;         // current state of the Pulse
int lastPulseState = 0;     // previous state of the Pulse
int LastPulseCounter=0;
//int alert_state = OFF;
float SetFlowRate = 0;
int state = 1013;
int lastState = 1013;
unsigned long timelatest = 0;
double FlowRate = 0;
unsigned long timeoldest = 0;
unsigned long period = 0;
int i;
int touch_state = 0;
int touch_lastState = 0;
unsigned long touch_timelatest = 0;
unsigned long touch_timeoldest = 0;


// The SetUp Function:
void setup() {
  Particle.variable("rate", FlowRate);
}

// The Main Loop Function
void loop() {

 Signal_In = analogRead(PulseSensorPurplePin0);  // Read the PulseSensor's value. 

 CalculateFlowRate();
   
   if(Signal_In > Threshold){                          // If the signal is above "550", then "turn-on" Arduino's on-Board LED.  
     digitalWrite(LED13,HIGH);         
     PulseIR = 1;
   } else {
     digitalWrite(LED13,LOW);                //  Else, the sigal must be below "550", so "turn-off" this LED.
     PulseIR = 0;
   }
   
//-------------------------------------------
//----------State-Machine---------------------
 PulseState = PulseIR;

  // compare the PulseState to its previous state
  if (PulseState != lastPulseState) {
    // if the state has changed, increment the counter
    if (PulseState == 1) {
      // if the current state is HIGH then the Pulse
      // wend from off to on:
      PulseCounter++;
      //Serial.println(PulseCounter);
    } 
    else {

    }
  }
  // save the current state as the last state, 
  //for next time through the loop
  lastPulseState = PulseState;

//--------------------------------  

delay(10);
 
   
}

//Function to Calculate HeartRate or BPM - Referred to as Flowrate but you can change this with your code.
 void CalculateFlowRate(void) { 
   
 if (PulseCounter != LastPulseCounter)
  {
    timelatest = micros();
    period = timelatest - timeoldest;

      FlowRate = (float)(1000*1000*60)  / (timelatest - timeoldest);
      
      timeoldest = timelatest;
 
  }
 timeoldest = timelatest;
LastPulseCounter = PulseCounter;
   
 }




