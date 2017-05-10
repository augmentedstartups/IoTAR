using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LoadObject : MonoBehaviour {

	public GameObject Game_Temp;
	public int State;

	public int counter = 1;
	public GameObject Sun;
	public GameObject Moon;
	public GameObject Storm;
	public GameObject Rain;
	public GameObject Cloud;

	GameObject MoonObject;
	GameObject SunObject;
	GameObject StormObject;
	GameObject RainObject;
	GameObject CloudObject;


	public void Button_Click()
	{

		switch (counter)
		{
		case 5:
			print ("Cloud");
			Destroy (MoonObject);
			Destroy (RainObject);
			Destroy (StormObject);
			Destroy (SunObject);
			CloudObject = (GameObject)Instantiate (Cloud);
			CloudObject.transform.position = new Vector3 (0, 0, 0);
			break;
		case 4:
			print ("Rain");
			Destroy (MoonObject);
			Destroy (StormObject);
			Destroy (SunObject);
			RainObject = (GameObject)Instantiate (Rain);
			RainObject.transform.position = new Vector3 (0, 0, 0);
			break;
		case 3:
			print ("Sun");
			Destroy (RainObject);
			Destroy (MoonObject);
			Destroy (StormObject);
			 SunObject = (GameObject)Instantiate (Sun);
			SunObject.transform.position = new Vector3 (0, 0, 0);
			break;
		case 2:
			print ("Moon");
			Destroy (RainObject);
			Destroy (SunObject);
			Destroy (StormObject);
			MoonObject = (GameObject)Instantiate (Moon);

			MoonObject.transform.position = new Vector3 (0,0,0);
			break;
		case 1:
			print ("Storm");
			Destroy (CloudObject);
			Destroy (RainObject);
			Destroy (SunObject);
			Destroy (MoonObject);
			StormObject = (GameObject)Instantiate (Storm);
			StormObject.transform.position = new Vector3 (0,0,0);
			break;
		default:
			print ("Nothing Doing.");
			break;
		}
		if (counter >= 5)
		{
			counter = 1;
		}
		else
		{
			counter++;
		}
		Debug.Log (counter);


	}
	// Use this for initialization
	void Start () {
		Game_Temp = GameObject.FindWithTag("TAG_WEATHER");
	}

	void Update(){
		State = Game_Temp.GetComponent<Weather_Text>().StateWeather;
	}



		



}
