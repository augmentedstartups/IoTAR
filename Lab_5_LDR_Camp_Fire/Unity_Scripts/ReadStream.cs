// based on this: http://answers.unity3d.com/questions/1085293/www-not-getting-site-that-doesnt-end-on-html.html 
using UnityEngine;
using System.Collections.Generic;
using System.Net;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReadStream : MonoBehaviour
{
	public string PhotonParticleURL = "https://api.particle.io/v1/devices/events?access_token=fc30489129ccbad879a2e3921485501418ada51c"; //This API will be RESET
	WebStreamReader request = null;

	DataClassTemperature parseDataTemperature = new DataClassTemperature ();

	bool lightTrue = false;
	bool humidityTrue = false;
	bool motionTrue = false;
	bool temperatureTrue = false;
	bool ultravioletTrue = false;

	public class DataClass
	{
		public int data;
	}


	public class DataClassTemperature
	{
		public int data;
	}
		

	void Start()
	{
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

				string[] data = stream.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
				//Debug.Log ("Data length: " + data.Length);
				stream = data[data.Length - 1];

				for (int i = 0; i < data.Length - 1; i++)
				{
					if (!string.IsNullOrEmpty(data[i]))
					{
						//	Debug.Log ("Data: " + data [i]); // print all block of data (event + data)

						if (data [i].Contains ("LightIntensity")) {
							temperatureTrue = true;
							lightTrue = true;
							motionTrue = true;
							ultravioletTrue = true;
							humidityTrue = true;
							string output = data [i].Substring(data [i].IndexOf("{"));
							parseDataTemperature = JsonUtility.FromJson<DataClassTemperature> (output);
							//Debug.Log ("Data of waterlevel sensor: " + data [i]);

						}
						if (temperatureTrue) {
							gameObject.GetComponent<IoT> ().microTemperatureVal = parseDataTemperature.data;

							humidityTrue = false;
							motionTrue = false;
							temperatureTrue = false;
							lightTrue = false;
							ultravioletTrue = false;
						}
					}
				}
			
			}
			yield return new WaitForSecondsRealtime(1); 
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