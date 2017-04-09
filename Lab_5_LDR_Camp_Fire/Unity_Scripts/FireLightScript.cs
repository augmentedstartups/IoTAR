using UnityEngine;

public class FireLightScript : MonoBehaviour
{
	public GameObject GameLight; 
	public float LightVariance;
	public float minIntensity = 0.25f;
	public float maxIntensity = 0.5f;

	public Light fireLight;

	float random;

	void Update()
	{
		LightVariance = GameLight.GetComponent<IoT> ().LightSensorValue/10;
		Debug.Log("LightVariance " + LightVariance);
		random = Random.Range(0.0f, 150.0f);
		float noise = Mathf.PerlinNoise(random, Time.time);
		fireLight.GetComponent<Light>().intensity = Mathf.Lerp(LightVariance, LightVariance, noise);
	}
}