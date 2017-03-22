using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
//using System.IO.Ports;

public class IoT : MonoBehaviour, ITrackableEventHandler {	
	public GameObject imageTarget;
	private TrackableBehaviour mTrackableBehaviour;
	private bool targetFound = false;
	public GameObject hide;
	string value;	
	public Text timeText;
	public string timeTextVal;
	public Text microTemperatureText;
	public float microTemperatureVal;
	public Text microHumidityText;
	public float microHumidityVal;
	public Text microPhotoresistorText;
	public float microPhotoresistorVal;
	public Text microUltravioletText;
	public float microUltravioletVal;
	public Text MotionText;
	public bool motionDetectedBool = false;

	public Text summary;
	public Text currentMeasurement;
	public Text mean;
	public Text LowerLimit;
	public Text UpperLimit;
	public Text LowerVariance;
	public Text UpperVariance;
	public Text UpperTime;
	public Text LowerTime;

	// temperature
	float minTemp = 20.0f;
	float maxTemp = 24.0f;
	float meanTemperature = 0;
	float meanAboveTemperature = 0;
	float meanBelowTemperature = 0;
	bool firstT = true;
	bool firstAT = true;
	bool firstBT = true;

	// photoresistor
	float minLight = 60.0f;
	float maxLight = 90.0f;
	float meanPhotoresistor = 0;
	float meanAbovePhotoresistor = 0;
	float meanBelowPhotoresistor = 0;
	bool firstL = true;
	bool firstAL = true;
	bool firstBL = true;

	// humidity
	float minHumidity = 50.0f;
	float maxHumidity = 60.0f;
	float meanHumidity = 0;
	float meanAboveHumidity = 0;
	float meanBelowHumidity = 0;
	bool firstH = true;
	bool firstAH = true;
	bool firstBH = true;

	// ultraviolet
	float minUltraviolet = 0.0f;
	float maxUltraviolet = 50.0f;
	float meanUltraviolet = 0;
	float meanAboveUltraviolet = 0;
	float meanBelowUltraviolet = 0;
	bool firstU = true;
	bool firstAU = true;
	bool firstBU = true;

	int index;
	public GameObject[] Buttons =new GameObject[6];

	bool hit;
	bool showTemperature = false;
	bool showLight = false;
	bool showHumidity = false;
	bool showUltraviolet = false;

	void Start () {	
		mTrackableBehaviour = imageTarget.GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
	}

	void Update () {
		if (targetFound) {
			hide.SetActive (true);
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit (); // exit app if back/esc button is pressed
			}

				timeText.text = System.DateTime.Now.ToString ();
				microTemperatureText.text = microTemperatureVal.ToString ();
				checkTemperature ();
				microHumidityText.text = microHumidityVal.ToString ();
				checkHumidity ();
				microPhotoresistorText.text = microPhotoresistorVal.ToString ();
				checkPhotoresistor ();
				checkMotion ();
				microUltravioletText.text = microUltravioletVal.ToString ();
				checkUltraviolet ();

				getTemperatureInfo ();
				if (showTemperature) {
					showTemperatureInfo ();
				}

				getLightInfo ();
				if (showLight) {
					showLightInfo ();
				}

				getHumidityInfo ();
				if (showHumidity) {
					showHumidityInfo ();
				}

				getUltravioletInfo ();
				if (showUltraviolet) {
					showUltravioletInfo ();
				}

			}

		if (!targetFound) {
			hide.SetActive (false);
		}
	}

	public void getTemperatureInfo() {
		if (firstT) {
			meanTemperature = microTemperatureVal;
			firstT = false;
		}
		if (!firstT)
			meanTemperature = (meanTemperature + microTemperatureVal)/2;

		if (firstAT && microTemperatureVal >= maxTemp) {
			meanAboveTemperature = microTemperatureVal;
			firstAT = false;
		}
		if (!firstAT && microTemperatureVal >= maxTemp) {
			meanAboveTemperature = (meanAboveTemperature + microTemperatureVal) / 2;
		}

		if (firstBT && microTemperatureVal <= minTemp) {
			meanAboveTemperature = microTemperatureVal;
			firstBT = false;
		}
		if (!firstBT && microTemperatureVal <= minTemp) {
			meanBelowTemperature = (meanBelowTemperature + microTemperatureVal) / 2;
		}
	}

	public void showTemperatureInfo() {
		summary.text = "Temperature summary: ";
		currentMeasurement.text = microTemperatureVal.ToString();

		if (!firstAT && microTemperatureVal >= maxTemp) {
			UpperVariance.text = (meanAboveTemperature - maxTemp).ToString ();
			UpperTime.text = System.DateTime.Now.ToString ();
		}
		if (!firstBT && microTemperatureVal <= minTemp) {
			LowerVariance.text = (minTemp - meanBelowTemperature).ToString ();
			LowerTime.text = System.DateTime.Now.ToString ();
		}
		mean.text = meanTemperature.ToString ();
		UpperLimit.text = maxTemp.ToString ();
		LowerLimit.text = minTemp.ToString ();

		showTemperature = true;
	}


	public void getLightInfo() {
		if (firstL) {
			meanPhotoresistor = microPhotoresistorVal;
			firstL = false;
		}
		if (!firstL)
			meanPhotoresistor = (meanPhotoresistor + microPhotoresistorVal)/2;

		if (firstAL && microPhotoresistorVal >= maxLight) {
			meanAbovePhotoresistor = microPhotoresistorVal;
			firstAL = false;
		}
		if (!firstAL && microPhotoresistorVal >= maxLight) {
			meanAbovePhotoresistor = (meanAbovePhotoresistor + microPhotoresistorVal) / 2;
		}

		if (firstBL && microPhotoresistorVal <= minLight) {
			meanBelowPhotoresistor = microPhotoresistorVal;
			firstBL = false;
		}
		if (!firstBL && microPhotoresistorVal <= minLight) {
			meanBelowPhotoresistor = (meanBelowPhotoresistor + microPhotoresistorVal) / 2;
		}
	}

	public void showLightInfo() {
		summary.text = "Light summary: ";
		currentMeasurement.text = microPhotoresistorVal.ToString();
		if (!firstAL && microPhotoresistorVal >= maxLight) {
			UpperVariance.text = (meanAbovePhotoresistor - maxLight).ToString ();
			UpperTime.text = System.DateTime.Now.ToString ();
		}
		if (!firstBL && microPhotoresistorVal <= minLight) {
			LowerVariance.text = (minLight - meanBelowPhotoresistor).ToString ();
			LowerTime.text = System.DateTime.Now.ToString ();
		}
		mean.text = meanPhotoresistor.ToString ();
		UpperLimit.text = maxLight.ToString ();
		LowerLimit.text = minLight.ToString ();
		showLight = true;
	}

	public void getHumidityInfo() {
		if (firstH) {
			meanHumidity = microHumidityVal;
			firstH = false;
		}
		if (!firstH)
			meanHumidity = (meanHumidity + microHumidityVal)/2;

		if (firstAH && microHumidityVal >= maxHumidity) {
			meanAboveHumidity = microHumidityVal;
			firstAH = false;
		}
		if (!firstAH && microHumidityVal >= maxHumidity) {
			meanAboveHumidity = (meanAboveHumidity + microHumidityVal) / 2;
		}

		if (firstBH && microHumidityVal <= minHumidity) {
			meanBelowHumidity = microHumidityVal;
			firstBH = false;
		}
		if (!firstBH && microHumidityVal <= minHumidity) {
			meanBelowHumidity = (meanBelowHumidity + microHumidityVal) / 2;
		}
	}

	public void showHumidityInfo() {
		summary.text = "Humidity summary: ";
		currentMeasurement.text = microHumidityVal.ToString();
		if (!firstAH && microHumidityVal >= maxHumidity) {
			UpperVariance.text = (meanAboveHumidity - maxHumidity).ToString ();
			UpperTime.text = System.DateTime.Now.ToString ();
		}
		if (!firstBH && microHumidityVal <= minHumidity) {
			LowerVariance.text = (minHumidity - meanBelowHumidity).ToString ();
			LowerTime.text = System.DateTime.Now.ToString ();
		}
		mean.text = meanHumidity.ToString ();
		UpperLimit.text = maxHumidity.ToString ();
		LowerLimit.text = minHumidity.ToString ();

		showHumidity = true;
	}

	public void getUltravioletInfo() {
		if (firstU) {
			meanUltraviolet = microUltravioletVal;
			firstU = false;
		}
		if (!firstU)
			meanUltraviolet = (meanUltraviolet + microUltravioletVal)/2;

		if (firstAU && microUltravioletVal >= maxUltraviolet) {
			meanAboveUltraviolet = microUltravioletVal;
			firstAU = false;
		}
		if (!firstAU && microUltravioletVal >= maxUltraviolet) {
			meanAboveUltraviolet = (meanAboveUltraviolet + microUltravioletVal) / 2;
		}

		if (firstBU && microUltravioletVal <= minUltraviolet) {
			meanBelowUltraviolet = microUltravioletVal;
			firstBU = false;
		}
		if (!firstBU && microUltravioletVal <= minUltraviolet) {
			meanBelowUltraviolet = (meanBelowUltraviolet + microUltravioletVal) / 2;
		}
	}

	public void showUltravioletInfo() {
		summary.text = "Ultraviolet summary: ";
		currentMeasurement.text = microUltravioletVal.ToString();
		if (!firstAU && microUltravioletVal >= maxUltraviolet) {
			UpperVariance.text = (meanAboveUltraviolet - maxUltraviolet).ToString ();
			UpperTime.text = System.DateTime.Now.ToString ();
		}
		if (!firstBU && microUltravioletVal <= minUltraviolet) {
			LowerVariance.text = (minUltraviolet - meanBelowUltraviolet).ToString ();
			LowerTime.text = System.DateTime.Now.ToString ();
		}
		mean.text = meanUltraviolet.ToString ();
		UpperLimit.text = maxUltraviolet.ToString ();
		LowerLimit.text = minUltraviolet.ToString ();


		showUltraviolet = true;
	}

	public void resetBool() {
		LowerVariance.text = "n/a";
		UpperVariance.text = "n/a";
		LowerTime.text = "n/a";
		UpperTime.text = "n/a";
		showTemperature = false;
		showLight = false;
		showHumidity = false;
		showUltraviolet = false;
	}

	private void checkTemperature() {
		index = 0;
		if (microTemperatureVal >= minTemp && microTemperatureVal <= maxTemp) // Green Color - temperature is ok
			greenButtonColor (index);// (Buttons[0]);
		if (microTemperatureVal > maxTemp) // Red Color - temperature is too Hot
			orangeButtonColor(index);
		if (microTemperatureVal < minTemp) // Blue Color - temperature is too Cold
			blueButtonColor (index);
	}

	private void checkHumidity() {
		index = 1;
		if (microHumidityVal >= 50.00 && microHumidityVal <= 70.00) // Green Color -
			greenButtonColor (index);// (Buttons[0]);
		if (microHumidityVal > 70.00) // Red Color - 
			orangeButtonColor(index);
		if (microHumidityVal < 50.00) // Blue Color - 
			blueButtonColor (index);
	}

	private void checkPhotoresistor() {
		index = 2;

		if (microPhotoresistorVal >= minLight && microPhotoresistorVal <= maxLight) // Green Color - 
			greenButtonColor (index);// (Buttons[0]);
		if (microPhotoresistorVal > maxLight) // Red Color - 
			orangeButtonColor(index);
		if (microPhotoresistorVal < minLight) // Blue Color - 
			blueButtonColor (index);
	}

	private void checkMotion() {
		index = 3;
		if (motionDetectedBool == true) {
			orangeButtonColor (index);
			MotionText.text = "DETECTED!";
		}
		if (motionDetectedBool == false) {
			greenButtonColor (index);
			MotionText.text = "N/A";
		}
	}
		
	private void checkUltraviolet() {
		index = 4;
		if (microUltravioletVal >= 60.00 && microUltravioletVal <= 90.00) // Green Color - 
			greenButtonColor (index);// (Buttons[0]);
		if (microUltravioletVal > 90.00) // Red Color - 
			orangeButtonColor(index);
		if (microUltravioletVal < 60.00) // Blue Color - 
			blueButtonColor (index);
	}

	private void greenButtonColor (int i) { 
		//Debug.Log ("Green");
		Color greenColor = new Color (0.00f, 0.94f, 0.12f, 1.0f);
		Button b = Buttons[i].GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = greenColor;
		b.colors = cb;
		//EffectColor.GetComponent<LineRenderer> ().material.SetColor ("_TintColor",greenColor);
	}
	private void orangeButtonColor (int i) {
		//Debug.Log ("Orange"); // yellow
		Color redColor = new Color (1.0f, 0.48f, 0.16f, 1.0f);
		Button b = Buttons[i].GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = redColor;
		b.colors = cb;
	}

	private void blueButtonColor (int i) {
		//Debug.Log ("Blue");
		Color blueColor = new Color (0.27f, 0.43f, 1.0f, 1.0f);
		Button b = Buttons[i].GetComponent<Button> (); 
		ColorBlock cb = b.colors;
		cb.normalColor = blueColor;
		b.colors = cb;
	}


	void SelectionUI() {

	}

	public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			targetFound = true; //when target is found
		} else {
			targetFound = false; //when target is lost
		}
	}

}
