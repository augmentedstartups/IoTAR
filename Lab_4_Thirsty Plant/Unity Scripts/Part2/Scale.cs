//Created By: Ritesh Kanjee
//Project   : AR Thirsty Plant - Scale Cube
//Date      : 31st April 2017
//Website   : www.ArduinoStartups.com

using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {
	public GameObject GameSize; 
	public float size = 5f;
	public float WL;
	public float WL_Calibrated;
	Vector3 temp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
		WL = GameSize.GetComponent<IoT>().WaterSensorValue/1000;
		temp =	transform.localScale;


		if(WL < 0)
			WL_Calibrated = 0;
		else
			WL_Calibrated = WL * 7;
		
		temp.x = WL_Calibrated ;
		 
		transform.localScale = temp;
	}

	public void	AdjustSize(float newSize){
		size = newSize;

	
	}
}