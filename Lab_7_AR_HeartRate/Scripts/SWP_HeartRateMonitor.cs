//--------------ABOUT AND COPYRIGHT----------------------
// Copyright © 2013 SketchWork Productions Limited
//       support@sketchworkdev.com
//-------------------------------------------------------
using UnityEngine;
using System.Collections;


/// <summary>
/// This is the Heart Rate Monitor main script and controls every element of the control.
/// </summary>
public class SWP_HeartRateMonitor : MonoBehaviour
{
    public GameObject hand;
    public int heartrate;
    enum SoundType {HeartBeat1, HeartBeat2, Flatline};
    /// <HeartRateControl>
    //public GameObject Heart_Text;
    
    /// </HeartRateControl>
	public int BeatsPerMinute =10; // Beats per minute.
	public bool FlatLine = false; // Initialise a flat line.
    
    public bool ShowBlip = true; // Show the blip circle at the front of the monitor line.
	public GameObject Blip; // The blip game object.
	public float BlipSize = 1f; // The size of the blip circle at the front of the line.
	public float BlipTrailStartSize = 0.2f; // The size of the monitor trail line nearest to the blip circle.
	public float BlipTrailEndSize = 0.1f; // The size of the monitor line at the end before it fades out.
	public float BlipMonitorWidth = 40f; // The actual width of the entire monitor control.
	public float BlipMonitorHeightModifier = 1f; // The actual height of the entire monitor control.
		
	public bool EnableSound = true;
	public float SoundVolume = 1f;
	public AudioClip Heart1Sound;
	public AudioClip Heart2Sound;
	public AudioClip FlatlineSound;
	private bool bFlatLinePlayed = false;

	private float LineSpeed = 0.3f;
	private GameObject NewClone;
	private float TrailTime;
	private float BeatsPerSecond;
	private float LastUpdate = 0f;
	private Vector3 BlipOffset = Vector3.zero;
	private float DisplayXEnd;
	
	public Material MainMaterial;
	
	public Color NormalColour = new Color(0f, 1f, 0f, 1f);
	public Color MediumColour = new Color(1f, 1f, 0f, 1f);	
	public Color BadColour = new Color(1f, 0f, 0f, 1f);
	public Color FlatlineColour = new Color(1f, 0f, 0f, 1f); // Automatic when BeatsPerMinute is Zero
	
	// Use this for initialization
	void Start()
	{

        BeatsPerSecond = 60f / heartrate;
		BlipOffset = new Vector3 (transform.position.x - (BlipMonitorWidth / 2), transform.position.y, transform.position.z);
		DisplayXEnd = BlipOffset.x + BlipMonitorWidth;
		CreateClone();
		TrailTime = NewClone.GetComponentInChildren<TrailRenderer>().time;
	}
	
	// Update is called once per frame
	void Update()
	{

        hand = GameObject.FindWithTag("Text_BPM");
        heartrate = hand.GetComponent<Heart_Text>().heartrateInput;

        //Debug.Log("Received new amount: " + heartrate);

        BeatsPerSecond = 60f / heartrate;


		BlipOffset = new Vector3 (transform.position.x - (BlipMonitorWidth / 2), transform.position.y, transform.position.z);
		DisplayXEnd = BlipOffset.x + BlipMonitorWidth;
		
		if (NewClone.transform.position.x > DisplayXEnd)
		{			
			if (NewClone != null)
			{
				GameObject OldClone = NewClone;
				StartCoroutine(WaitThenDestroy(OldClone));
			}
			
			CreateClone();
		}
		else if (!FlatLine)
			NewClone.transform.position += new Vector3(BlipMonitorWidth * Time.deltaTime * LineSpeed, Random.Range(-0.05f, 0.05f), 0);
		else
		{
			NewClone.transform.position += new Vector3(BlipMonitorWidth * Time.deltaTime * LineSpeed, 0, 0);
			
			if (!bFlatLinePlayed)
			{
				PlayHeartSound(SoundType.Flatline, SoundVolume);
				bFlatLinePlayed = true;
			}
		}
		
		if (BeatsPerMinute <= 0 || FlatLine)
			LastUpdate = Time.time;
		else if (Time.time - LastUpdate >= BeatsPerSecond)
		{			
			LastUpdate = Time.time;
			StartCoroutine(PerformBlip());
		}
	}
	
	IEnumerator PerformBlip()
	{
		if (bFlatLinePlayed)
			bFlatLinePlayed = false;
		
		if (!bFlatLinePlayed)
			PlayHeartSound(SoundType.HeartBeat1, SoundVolume);
		
		NewClone.transform.position = new Vector3(NewClone.transform.position.x, (10f * BlipMonitorHeightModifier) + Random.Range(0f, (2f * BlipMonitorHeightModifier)) + BlipOffset.y, BlipOffset.z);
		yield return new WaitForSeconds(0.03f);		
		NewClone.transform.position = new Vector3(NewClone.transform.position.x, (-5f * BlipMonitorHeightModifier) - Random.Range(0f, (3f * BlipMonitorHeightModifier)) + BlipOffset.y, BlipOffset.z);
		yield return new WaitForSeconds(0.02f);		
		NewClone.transform.position = new Vector3(NewClone.transform.position.x, (3f * BlipMonitorHeightModifier) + Random.Range(0f, (2f * BlipMonitorHeightModifier)) + BlipOffset.y, BlipOffset.z);
		yield return new WaitForSeconds(0.02f);		
		NewClone.transform.position = new Vector3(NewClone.transform.position.x, (2f * BlipMonitorHeightModifier) + Random.Range(0f, (1f * BlipMonitorHeightModifier)) + BlipOffset.y, BlipOffset.z);
		yield return new WaitForSeconds(0.02f);		
		
		NewClone.transform.position = new Vector3(NewClone.transform.position.x, 0f + BlipOffset.y, BlipOffset.z);

		yield return new WaitForSeconds(0.2f);		
		
		if (!bFlatLinePlayed)
			PlayHeartSound(SoundType.HeartBeat2, SoundVolume);
	}
	
	void CreateClone()
	{
		NewClone = Instantiate(Blip, new Vector3(BlipOffset.x, BlipOffset.y, BlipOffset.z), Quaternion.identity) as GameObject;
		NewClone.transform.parent = gameObject.transform;
		
		NewClone.GetComponentInChildren<TrailRenderer>().startWidth = BlipTrailStartSize;
		NewClone.GetComponentInChildren<TrailRenderer>().endWidth = BlipTrailEndSize;
		NewClone.transform.localScale = new Vector3(BlipSize,BlipSize,BlipSize);

		if (ShowBlip)
			NewClone.GetComponent<MeshRenderer>().enabled = true;
		else
			NewClone.GetComponent<MeshRenderer>().enabled = false;
	}
	
	IEnumerator WaitThenDestroy(GameObject OldClone)
	{
		OldClone.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(TrailTime);
		DestroyObject(OldClone);
	}
	
	void PlayHeartSound(SoundType _SoundType, float fSoundVolume)
	{
		if (!EnableSound)
			return;
		
		if (_SoundType == SoundType.HeartBeat1)
			GetComponent<AudioSource>().PlayOneShot(Heart1Sound, fSoundVolume);
		else if (_SoundType == SoundType.HeartBeat2)
			GetComponent<AudioSource>().PlayOneShot(Heart2Sound, fSoundVolume);
		else if (_SoundType == SoundType.Flatline)
			GetComponent<AudioSource>().PlayOneShot(FlatlineSound, fSoundVolume);
	}
		
	public void SetHeartRateColour(Color _NewColour)
	{
		if (MainMaterial == null)
			throw new System.ArgumentException("You are trying to change the colour without having the 'MainMaterial' set in the control.  It must be set to the 'BlipMaterial' in order to use the colour changer.");
		
		MainMaterial.SetColor("_TintColor", _NewColour);
	}
}
