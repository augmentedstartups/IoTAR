using UnityEngine;
using System.Collections;

public class SmokeControl : MonoBehaviour {


	public GameObject Game_Temp;
	public float Alcohol;
	ParticleSystem smoke;

	// Use this for initialization
	void Start () {
		smoke = GetComponent<ParticleSystem>();			 	 //Assign particle system to
		Game_Temp = GameObject.FindWithTag("TEMP_ALCOHOL");  //Create a tag in Unity underNeedle, otherwise the script will not work.
	}
	
	// Update is called once per frame
	void Update () {
		Alcohol = Game_Temp.GetComponent<ForceController>().ForceInput;
		smoke.emissionRate = Alcohol/20f;			//Calibrate to smoke rate
		Debug.Log (smoke.emissionRate);			//Debug to console
	}
}
