using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {

	public float size = 5f;

	Vector3 temp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () { 
		temp =	transform.localScale;

		temp.x = size;

		transform.localScale = temp;
	}

	public void	AdjustSize(float newSize){
		size = newSize;
	
	
	}
}
