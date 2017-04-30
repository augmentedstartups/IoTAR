using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Temperature_Text : MonoBehaviour
{
    public float TemperatureInput;
	float temp;
	string path;
	string Url;
	string jsonRate;
	WWW www;
    
   
	void Start() // Use this for initialization
    {

    }

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			string work = www.data;

			_Particle fields = JsonUtility.FromJson<_Particle>(work);
			string jsonRate = fields.result;

			temp = float.Parse (jsonRate);
			//TemperatureInput = Mathf.FloorToInt(temp);
			TemperatureInput = temp;
			//Debug.Log (TemperatureInput);
		} else {

		}    
	}

    // Update is called once per frame
    void Update()
    {

        //heartrate = GameBeat.GetComponent<IoT>().heartrate;
		string url = "https://api.particle.io/v1/devices/230039000a47353138383138/Temperature?access_token=fc30489129ccbad879a2e3921485501418ada51c";
		www = new WWW(url);
		StartCoroutine(WaitForRequest(www));

		GetComponent<TextMesh>().text = TemperatureInput.ToString("f1")+"° C";


    }

	[System.Serializable]
	public class _Particle{
		public string name;
		public string result;
	}


}
