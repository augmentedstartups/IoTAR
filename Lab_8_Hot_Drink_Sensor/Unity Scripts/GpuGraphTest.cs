// © 2016 BRANISLV GRUJIC ALL RIGHTS RESERVED
// Provided AS IS
// For any official support, please use the contact on the unity asset store

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpuGraphTest : MonoBehaviour
{
    private int Count;
	public GameObject Game_Temp;
    float Temp_Variable;
    private Rect GraphRect;

    // Use this for initialization
    void Start()
    {
        Count = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
		Game_Temp = GameObject.FindWithTag("TEMP_TEXT1");
		Temp_Variable = Game_Temp.GetComponent<Temperature_Text>().TemperatureInput;
        Count = 2 + Count;



        float xSize = 0.5f * Screen.width;
        float ySize = 0.2f * Screen.height;
        float xPos = 0.2f * Screen.width;
        float yPos = 0.0f * Screen.height;

        GraphRect = new Rect(xPos, yPos, xSize, ySize);

        // calculate current time in ms
        float currentDeltaTime = Time.deltaTime * 1000.0f;

        if(GraphManager.Graph != null && currentDeltaTime < 18.0f)
        {
			GraphManager.Graph.Plot("Test_WorldSpace", Temp_Variable, Color.green, new GraphManager.Matrix4x4Wrapper(transform.position, transform.rotation, transform.localScale));
          //  GraphManager.Graph.Plot("Test_ScreenSpace", currentDeltaTime, Color.green, GraphRect);
        }
    }
}
