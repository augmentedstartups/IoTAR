//Created By    : Ritesh Kanjee
//Project       : Force Sensitive Resistor Project (IoTAR)
//Date          : 1 May 2017
//Description   : Changes the angle of the dial in response 
//				  to the force experienced by Force Dependent Resister
//Website       : www.ArduinoStartups.com
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnalogueSpeedConverter : MonoBehaviour {

	static float minAngle = 126.3f;				//Minimum angle at Point 0 on the Gauge		
	static float maxAngle = -122.1f;			//Maximum angle at Point 10 on the Gauge	
	static AnalogueSpeedConverter thisSpeedo;


	// Use this for initialization
	void Start () {
		thisSpeedo = this;						//Assign this game object to this Speedometer
	}

	public	static	void ShowSpeed(float speed, float min, float max)
	{
		float ang = Mathf.Lerp (minAngle, maxAngle, Mathf.InverseLerp (min, max, speed)); //Linear Interpolation Function
		thisSpeedo.transform.eulerAngles = new Vector3 (0, 0, ang);						  //Change Angle of Needle
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
