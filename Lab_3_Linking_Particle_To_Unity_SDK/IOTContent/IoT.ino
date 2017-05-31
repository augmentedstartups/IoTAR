//Created By    : Ritesh Kanjee
//Project       : Linking Particle Photon to Cloud (IoTAR)
//Date          : 12 April 2017
//Description   : Code allows you link published values from Photon to the Cloud
//Website       : www.ArduinoStartups.com
//--------------------------------------------------
// REVISION LIST<br>
// ==========
//        
// 1.00   New Version<br>
// 1.01   31 May 2017 -   R. Kanjee : Deleted Adafruit DHT Libraries
//--------------------------------------------------
// based on; https://www.openhomeautomation.net/cloud-data-logger-particle-photon/
// ome useful stuff: http://jflasher.github.io/spark-helper/
//--------------------------------------------------
// Variables
int temperature;
int humidity;
int light;
int motion;
int ultraviolet;
int t = 1000; // time
// Pins
int led = D7;
int led2 = D1;
int light_sensor_pin = A0;
int pir = 2;
int pirState = LOW;
int uv = A1;

void setup() {
    Particle.function("led", ledControl);
    Particle.function("led2", ledControl2);
    // Start DHT sensor
    pinMode(pir,INPUT);
    pinMode(led,OUTPUT);
    pinMode(led2,OUTPUT);
    digitalWrite(led,LOW);
    digitalWrite(led2,LOW);
}

void loop() {
    
    // Humidity measurement
    temperature = 23;
    
    // Humidity measurement
    humidity = 50;
    
    // Light level measurement
    float light_measurement = analogRead(light_sensor_pin);
    light = (int)(light_measurement/4096*100);
    
    int uvValue=analogRead(uv);
    ultraviolet = (uvValue*100)/1023;
    // Publish data
    Particle.publish("temperature", String(random(100, 200)));// + " °C");
    delay(t);
    Particle.publish("humidity", String(random(50, 100)));// + "%");
    delay(t);
    Particle.publish("light", String(random(50, 60) ));// + "%");
    delay(t);
    pirSensor(); // motion sensor information
    delay(t);
    Particle.publish("ultraviolet", String(random(10, 50)));
   // uvSensor(); // ultraviolet sensor information
    delay(t);
   // Particle.publish("time", Time.timeStr());
   // Particle.publish("time", String(Time.now()));
   // delay(t);
   // delay(t);
   // Particle.function("led",ledControl);
    // https://api.spark.io/v1/devices/340024001547343339383037/led?access_token=649e7d09d0980e4b649e42f6dcff79887d9570e2&args=l0,HIGH
    
}

void pirSensor() {
     motion = digitalRead(random(0, 1)); // read input value
// if (motion == HIGH) { // check if the input is HIGH
   // digitalWrite(led, HIGH); // turn LED ON
  //  if (pirState == LOW) {
  if(motion ==true)
     Particle.publish("motion", "1"); // or 1 -  We only want to print on the output change, not state
  //  pirState = HIGH;
  // }
 // } 
//  else {
  //digitalWrite(led, LOW); // turn LED OFF
 // if (pirState == HIGH){
 if(motion==false)
    Particle.publish("motion", "0"); // or 0 - Serial.println("Motion ended!");
  //  pirState = LOW; // We only want to print on the output change, not state
 // }
// }
}

void uvSensor() {
 // float uvValue;
 // long  sum=0;
 // for(int i=0;i<1024;i++)
 //  {  
   //   uvValue=analogRead(uv);///1023;
  //    sum=uvValue+sum;
  //    delay(2);   
  //  }   
  // sum = sum >> 10;
  // ultraviolet = sum*4980.0/1023.0;

  // uvValue*0.097f; //(int)(uvValue/1023*100); //
 //  Particle.publish("ultraviolet", String(ultraviolet));
}


int ledControl(String command) {

    if (command=="on") {
        digitalWrite(led,HIGH);
        return 1;
    }
    else if (command=="off") {
        digitalWrite(led,LOW);
        return 0;
    }
    else {
        return -1;
    }

}

int ledControl2(String command) {

    if (command=="on") {
        digitalWrite(led2,HIGH);
        return 1;
    }
    else if (command=="off") {
        digitalWrite(led2,LOW);
        return 0;
    }
    else {
        return -1;
    }

}
