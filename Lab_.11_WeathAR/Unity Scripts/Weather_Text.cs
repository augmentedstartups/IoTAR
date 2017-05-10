using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Weather_Text : MonoBehaviour
{
	string JSON_Name;
	string JSON_Country;
	string JSON_Temperature;
	string JSON_Weather;
	string path;
	string Url;
	float temperature;
	public int StateWeather;



	string Zero;
	WWW www;
	string url = "https://api.apixu.com/v1/current.json?key=9c7878a483664ef6928185252170905&q=London";
   
	void Start() // Use this for initialization
    {
		www = new WWW(url);
		StartCoroutine(WaitForRequest(www));



    }

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			string work = www.data;

			_Particle fields = JsonUtility.FromJson<_Particle>(work);
			JSON_Name = fields.location.name;
			JSON_Country = fields.location.country;
			JSON_Weather = fields.current.condition.text;
			JSON_Temperature = fields.current.temp_c;
			temperature = float.Parse (JSON_Temperature);
			Debug.Log (JSON_Name);
			Debug.Log (JSON_Country);
			Debug.Log (JSON_Weather);
			Debug.Log (JSON_Temperature);
		} else {

		}    
	}

    // Update is called once per frame
    void Update()
    {

		GetComponent<TextMesh>().text = temperature.ToString("f0")+"° C " + "in \n " + JSON_Name + ",\n " + JSON_Country;
		if (JSON_Weather == "Overcast" || JSON_Weather == "Partly cloudy") {
			StateWeather = 5;
			Debug.Log (StateWeather);
		}
		else if (JSON_Weather == "Sunny"){
				StateWeather = 3;
				Debug.Log (StateWeather);
		}
		else if (JSON_Weather == "Clear"){
			StateWeather = 2;
			Debug.Log (StateWeather);
		}
		

	 }


	[System.Serializable]
	public class _condition{
		public string text;

	}

	[System.Serializable]
	public class _location{
		public string name;
		public string country;

	}

	[System.Serializable]
	public class _current{
		public _condition condition;
		public string temp_c;

	}


	[System.Serializable]
	public class _Particle{
		public _condition condition;
		public _location location;
		public _current current;
		public string temp;
		public string main;
	}




}
