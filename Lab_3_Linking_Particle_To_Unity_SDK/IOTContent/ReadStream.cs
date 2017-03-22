// based on this: http://answers.unity3d.com/questions/1085293/www-not-getting-site-that-doesnt-end-on-html.html 
using UnityEngine;
using System.Collections.Generic;
using System.Net;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReadStream : MonoBehaviour
{
	public string PhotonParticleURL = "https://api.particle.io/v1/devices/events?access_token=8f2dde300247081efd541b1e36dbbc1a3ce4db23";
	WebStreamReader request = null;

	DataClassHumidity parseDataHumidity = new DataClassHumidity ();
	DataClassUltraviolet parseDataUltraviolet = new DataClassUltraviolet ();
	DataClassMotion parseDataMotion = new DataClassMotion ();
	DataClassTemperature parseDataTemperature = new DataClassTemperature ();
	DataClassLight parseDataLight = new DataClassLight ();

	bool lightTrue = false;
	bool humidityTrue = false;
	bool motionTrue = false;
	bool temperatureTrue = false;
	bool ultravioletTrue = false;

	public class DataClass
	{
		public int data;
	}

	public class DataClassHumidity
	{
		public int data;
	}

	public class DataClassUltraviolet
	{
		public int data;
	}

	public class DataClassMotion
	{
		public int data;
	}

	public class DataClassTemperature
	{
		public int data;
	}

	public class DataClassLight
	{
		public int data;
	}

	void Start()
	{
		//parseDataHumidity = new DataClass ();
		StartCoroutine(WRequest());
	}

	void Update() {

	}

	IEnumerator WRequest()
	{
		request = new WebStreamReader();
		request.Start(PhotonParticleURL); //https://www.ourtechart.com//wp-content/uploads/2016/04/jsonAllData.txt");
		string stream = "";
		while (true)
		{
			string block = request.GetNextBlock();
			if (!string.IsNullOrEmpty(block))
			{
				stream += block;
				//Debug.Log ("Stream1: " + stream);
				string[] data = stream.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
				//Debug.Log ("Data length: " + data.Length);
				stream = data[data.Length - 1];

				for (int i = 0; i < data.Length - 1; i++)
				{
					if (!string.IsNullOrEmpty(data[i]))
					{
						//	Debug.Log ("Data: " + data [i]); // print all block of data (event + data)
						if (data [i].Contains ("light")) {
							lightTrue = true;
							string output = data [i].Substring(data [i].IndexOf("{"));
							parseDataLight = JsonUtility.FromJson<DataClassLight> (output);
							//Debug.Log ("Data of Photoresistor: " + parseData.data);
							//text.text = parseData.data.ToString ();
							//gameObject.GetComponent<IoT> ().microPhotoresistorVal = parseDataLight.data;
						}
						
						if (data [i].Contains ("temperature")) {
							temperatureTrue = true;
							string output = data [i].Substring(data [i].IndexOf("{"));
							parseDataTemperature = JsonUtility.FromJson<DataClassTemperature> (output);
							//Debug.Log ("Data of Temperature sensor: " + parseData.data);
							//text.text = parseData.data.ToString ();
							//gameObject.GetComponent<IoT> ().microTemperatureVal = parseDataTemperature.data;
						}
						if (data [i].Contains ("motion")) {
							motionTrue = true;
							string output = data [i].Substring(data [i].IndexOf("{"));
							parseDataMotion = JsonUtility.FromJson<DataClassMotion> (output);
							//Debug.Log ("Data of PIR sensor: " + parseData.data);
							//text.text = parseData.data.ToString ();
							//gameObject.GetComponent<IoT> ().motionDetectedBool = Convert.ToBoolean(parseDataMotion.data);
						}
						if (data [i].Contains ("ultraviolet")) {
							ultravioletTrue = true;
							string output = data [i].Substring(data [i].IndexOf("{"));
							parseDataUltraviolet = JsonUtility.FromJson<DataClassUltraviolet> (output);
							//Debug.Log ("Data of PIR ultraviolet: " + parseData.data);
							//text.text = parseData.data.ToString ();
							//gameObject.GetComponent<IoT> ().microUltravioletVal = parseDataUltraviolet.data;
						}
						if (data [i].Contains ("humidity")) {
							humidityTrue = true;
							string output = data [i].Substring(data [i].IndexOf("{"));
							parseDataHumidity = JsonUtility.FromJson<DataClassHumidity> (output);
							//Debug.Log ("Data of Humidity: " + parseData.data);
							//text.text = parseData.data.ToString ();
							//gameObject.GetComponent<IoT> ().microHumidityVal = parseDataHumidity.data;
						}
						//Debug.Log ("TEst: " + humidityTrue + temperatureTrue + lightTrue + ultravioletTrue);
						if (humidityTrue && temperatureTrue && lightTrue && ultravioletTrue) {
							//Debug.Log ("PRINT ALLLLLLLLLLLLLL");
							gameObject.GetComponent<IoT> ().microPhotoresistorVal = parseDataLight.data;
							gameObject.GetComponent<IoT> ().microTemperatureVal = parseDataTemperature.data;
							gameObject.GetComponent<IoT> ().motionDetectedBool = Convert.ToBoolean(parseDataMotion.data);
							gameObject.GetComponent<IoT> ().microUltravioletVal = parseDataUltraviolet.data;
							gameObject.GetComponent<IoT> ().microHumidityVal = parseDataHumidity.data;
							humidityTrue = false;
							motionTrue = false;
							temperatureTrue = false;
							lightTrue = false;
							ultravioletTrue = false;
						}
					}
				}
			
			}
			yield return new WaitForSeconds(1);
		}
	}

	void OnApplicationQuit()
	{
		if (request != null)
			request.Dispose();
	}

	void OnDataUpdate(decimal aAmount)
	{
		Debug.Log("Received new amount: " + aAmount);
		// Do whatever you want with the value
	}
}