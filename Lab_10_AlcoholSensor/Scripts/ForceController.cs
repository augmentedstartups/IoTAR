//Created By    : Ritesh Kanjee
//Project       : Force Sensitive Resistor Project (IoTAR)
//Date          : 1 May 2017
//Description   : Receives the Force Variable from the Particle Photon 
//				  via the cloud using JSON Utility
//Website       : www.ArduinoStartups.com
using UnityEngine;
using System.Collections;

public class ForceController : MonoBehaviour {

	public float ForceInput; 		//Declare Variables and Strings
	float ParticleVariable;
	string path;
	string Url;
	string jsonRate;
	WWW www;

	public float ForceMode;
	// Use this for initialization
	void Start () {

	}

	IEnumerator WaitForRequest(WWW www)				//Obtain Variables from the Photon Cloud using JSON
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			string work = www.data;

			_Particle fields = JsonUtility.FromJson<_Particle>(work);
			string jsonRate = fields.result;

			ParticleVariable = float.Parse (jsonRate);
			//Debug.Log (ParticleVariable);			//Debug to console
			ForceInput = ParticleVariable;
		} 
		else {}    
	}

	[System.Serializable]
	public class _Particle{							//Class defined to obtain the Cloud Variable Name and Result
		public string name;
		public string result;
	}

	// Update is called once per frame
	void Update () {
		//Insert your cURL here:
		string url = "https://api.particle.io/v1/devices/230039000a47353138383138/Alcohol?access_token=fc30489129ccbad879a2e3921485501418ada51c";
		www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
		AnalogueSpeedConverter.ShowSpeed (ForceInput, 0, 100); //Send Force reading to analog dial.

	}
}
