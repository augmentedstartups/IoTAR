using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

	public GameObject Game_Temp;
	public float Temp_Variable;
	public float Temp_test;
	float RedColor, GreenColor, BlueColor;
	// Use this for initialization
	void Start () {
		Temp_test = 36;
	}
	
	// Update is called once per frame
	void Update () {
		Game_Temp = GameObject.FindWithTag("TEMP_TEXT1");
		Temp_Variable = Game_Temp.GetComponent<Temperature_Text>().TemperatureInput;
		//Debug.Log (Temp_Variable);
		Temp_test =Temp_Variable;
		RedColor =   Mathf.Clamp((Temp_test-22)/(37-22),0,1);
		if(Temp_test >= 22)
		{
			GreenColor =   Mathf.Clamp((37 - Temp_test)/(37-22),0,1);
		}
		else
		{
			GreenColor =   Mathf.Clamp((Temp_test-15)/(22-15),0,1);
		}

		BlueColor =  Mathf.Clamp((22-Temp_test)/(22-15),0,1);
		Debug.Log (RedColor);

		GetComponent<Renderer>().material.color = new Color(RedColor, GreenColor, BlueColor); //C sharp
	}
}
