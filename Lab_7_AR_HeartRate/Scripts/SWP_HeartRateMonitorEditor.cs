// --------------ABOUT AND COPYRIGHT----------------------
//  Copyright Â© 2013 SketchWork Productions Limited
//        support@sketchworkproductions.com
// -------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SWP_HeartRateMonitor))]
public class SWP_HeartRateMonitorEditor : Editor
{


    /// 
	static public bool ShowHeader = true;
	static public bool ShowTitles = true;
	static public bool ShowQuickDebugControls = true;

    void Update()
    {
         SWP_HeartRateMonitor _HeartRateMonitorScript = (SWP_HeartRateMonitor)target;
        _HeartRateMonitorScript.BeatsPerMinute = _HeartRateMonitorScript.heartrate;
    }

    public override void OnInspectorGUI()
	{
		SWP_HeartRateMonitor _HeartRateMonitorScript = (SWP_HeartRateMonitor)target;  
		
		#region GLOBAL STATIC CONTROLS
		if (SWP_HeartRateMonitorEditor.ShowHeader)
			GetHeader();
		
		if (SWP_HeartRateMonitorEditor.ShowTitles)
		{
			EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
			EditorGUILayout.LabelField("Heart Rate/Beat Globals");
			EditorGUILayout.EndHorizontal();
		}
		
		#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
		EditorGUILayout.BeginVertical(EditorStyles.miniButtonMid);
		SWP_HeartRateMonitorEditor.ShowHeader = EditorGUILayout.ToggleLeft("Show Editor Header", SWP_HeartRateMonitorEditor.ShowHeader);
		SWP_HeartRateMonitorEditor.ShowTitles = EditorGUILayout.ToggleLeft("Show Editor Titles", SWP_HeartRateMonitorEditor.ShowTitles);
		SWP_HeartRateMonitorEditor.ShowQuickDebugControls = EditorGUILayout.ToggleLeft("Show Debug Controls", SWP_HeartRateMonitorEditor.ShowQuickDebugControls);
		#else
		EditorGUILayout.BeginVertical();
		SWP_HeartRateMonitorEditor.ShowHeader = EditorGUILayout.Toggle("Show Editor Header", SWP_HeartRateMonitorEditor.ShowHeader);
		SWP_HeartRateMonitorEditor.ShowTitles = EditorGUILayout.Toggle("Show Editor Titles", SWP_HeartRateMonitorEditor.ShowTitles);
		SWP_HeartRateMonitorEditor.ShowQuickDebugControls = EditorGUILayout.Toggle("Show Debug Controls", SWP_HeartRateMonitorEditor.ShowQuickDebugControls);
		#endif
		EditorGUILayout.EndVertical();
		#endregion

		#region SOUND CONTROLS
		EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
		#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
		_HeartRateMonitorScript.EnableSound = EditorGUILayout.ToggleLeft("Enable Sound", _HeartRateMonitorScript.EnableSound);
		#else
		_HeartRateMonitorScript.EnableSound = EditorGUILayout.Toggle("Enable Sound", _HeartRateMonitorScript.EnableSound);
		#endif
		EditorGUILayout.EndHorizontal();
		
		if (_HeartRateMonitorScript.EnableSound)
		{
			#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
			EditorGUILayout.BeginVertical(EditorStyles.miniButtonMid);
			#else
			EditorGUILayout.BeginVertical();
			#endif
			_HeartRateMonitorScript.SoundVolume = EditorGUILayout.Slider("Sound Volume", _HeartRateMonitorScript.SoundVolume, 0f, 1f);
			_HeartRateMonitorScript.Heart1Sound = (AudioClip) EditorGUILayout.ObjectField("Heart 1 Sound", _HeartRateMonitorScript.Heart1Sound, typeof(AudioClip), false);
			_HeartRateMonitorScript.Heart2Sound = (AudioClip) EditorGUILayout.ObjectField("Heart 2 Sound", _HeartRateMonitorScript.Heart2Sound, typeof(AudioClip), false);
			_HeartRateMonitorScript.FlatlineSound = (AudioClip) EditorGUILayout.ObjectField("Flatline Sound", _HeartRateMonitorScript.FlatlineSound, typeof(AudioClip), false);
			EditorGUILayout.EndVertical();
		}
		#endregion

		#region MAIN CONTROLLER SETTINGS
		if (SWP_HeartRateMonitorEditor.ShowTitles)  
		{
			EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
			EditorGUILayout.LabelField("Heart Rate/Beat Controls");
			EditorGUILayout.EndHorizontal();
		}
		
		#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
		EditorGUILayout.BeginVertical(EditorStyles.miniButtonMid);
		#else
		EditorGUILayout.BeginVertical();
#endif
        //heartrate = Heart_Text.GetComponent<Heart_Text>().heartrate;
        // _HeartRateMonitorScript.BeatsPerMinute = EditorGUILayout.IntSlider("Beats Per Minute", _HeartRateMonitorScript.BeatsPerMinute, 0, 240);
        
        _HeartRateMonitorScript.BeatsPerMinute = EditorGUILayout.IntSlider("Beats Per Minute", _HeartRateMonitorScript.heartrate, 0, 240);		
		
		#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
		_HeartRateMonitorScript.FlatLine = EditorGUILayout.ToggleLeft("Flatline", _HeartRateMonitorScript.FlatLine);
		EditorGUILayout.Separator();
		_HeartRateMonitorScript.ShowBlip = EditorGUILayout.ToggleLeft("Show Leading Blip", _HeartRateMonitorScript.ShowBlip);
		#else
		_HeartRateMonitorScript.FlatLine = EditorGUILayout.Toggle("Flatline", _HeartRateMonitorScript.FlatLine);
		EditorGUILayout.Separator();
		_HeartRateMonitorScript.ShowBlip = EditorGUILayout.Toggle("Show Leading Blip", _HeartRateMonitorScript.ShowBlip);
		#endif

		_HeartRateMonitorScript.Blip = (GameObject) EditorGUILayout.ObjectField("Blip Prefab", _HeartRateMonitorScript.Blip, typeof(GameObject), false);
		_HeartRateMonitorScript.BlipSize = EditorGUILayout.Slider("Blip Size", _HeartRateMonitorScript.BlipSize, 0.1f, 10f);
		_HeartRateMonitorScript.BlipTrailStartSize = EditorGUILayout.Slider("Blip Trail Start Size", _HeartRateMonitorScript.BlipTrailStartSize, 0.1f, 10f);
		_HeartRateMonitorScript.BlipTrailEndSize = EditorGUILayout.Slider("Blip Trail End Size", _HeartRateMonitorScript.BlipTrailEndSize, 0f, 10f);
		EditorGUILayout.Separator();
		_HeartRateMonitorScript.BlipMonitorWidth = EditorGUILayout.FloatField("Blip Width", _HeartRateMonitorScript.BlipMonitorWidth);
		_HeartRateMonitorScript.BlipMonitorHeightModifier = EditorGUILayout.FloatField("Blip Height Modifier", _HeartRateMonitorScript.BlipMonitorHeightModifier);

		_HeartRateMonitorScript.MainMaterial = (Material) EditorGUILayout.ObjectField("Main Material", _HeartRateMonitorScript.MainMaterial, typeof(Material), false);
		
		EditorGUILayout.EndVertical();
		#endregion
		
		#region VISUAL CONTROLS
		if (SWP_HeartRateMonitorEditor.ShowTitles)  
		{
			EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
			EditorGUILayout.LabelField("Visual Controls");
			EditorGUILayout.EndHorizontal();
		}
		
		#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
		EditorGUILayout.BeginVertical(EditorStyles.miniButtonMid);
		#else
		EditorGUILayout.BeginVertical();
		#endif   
		
		_HeartRateMonitorScript.NormalColour = EditorGUILayout.ColorField("Normal Colour", _HeartRateMonitorScript.NormalColour);
		_HeartRateMonitorScript.MediumColour = EditorGUILayout.ColorField("Medium Colour", _HeartRateMonitorScript.MediumColour);
		_HeartRateMonitorScript.BadColour = EditorGUILayout.ColorField("Bad Colour", _HeartRateMonitorScript.BadColour);
		_HeartRateMonitorScript.FlatlineColour = EditorGUILayout.ColorField("Flatline Colour", _HeartRateMonitorScript.FlatlineColour);
				
		EditorGUILayout.EndVertical();	
		#endregion
		
		#region DEBUG SECTION CONTROLS
		if (SWP_HeartRateMonitorEditor.ShowQuickDebugControls)
		{ 
			if (SWP_HeartRateMonitorEditor.ShowTitles)  
			{
				EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
				EditorGUILayout.LabelField("Quick Debug Controls (" + (_HeartRateMonitorScript.FlatLine ? "FLATLINE" : (_HeartRateMonitorScript.BeatsPerMinute + "BPM")) + ")");
				EditorGUILayout.EndHorizontal();
			}
			
			#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
			EditorGUILayout.BeginVertical(EditorStyles.miniButtonMid);
			#else
			EditorGUILayout.BeginVertical();
			#endif   
			EditorGUILayout.BeginHorizontal();
			
			if (GUILayout.Button("Normal") && Application.isPlaying)
				_HeartRateMonitorScript.FlatLine = false;
			
			if (GUILayout.Button("Flatline") && Application.isPlaying)
				_HeartRateMonitorScript.FlatLine = true;
			
			EditorGUILayout.EndHorizontal();		
			
			EditorGUILayout.BeginHorizontal(); 
			
			if (GUILayout.Button("-10 BPM") && Application.isPlaying)
				_HeartRateMonitorScript.BeatsPerMinute = 150;
			
			if (GUILayout.Button("+10 BPM") && Application.isPlaying)
				_HeartRateMonitorScript.BeatsPerMinute += 10;
			
			EditorGUILayout.EndHorizontal();		
		    			
			EditorGUILayout.BeginHorizontal(); 
			
			EditorGUILayout.LabelField("Test Colours:", GUILayout.MaxWidth(90));
			
			if (GUILayout.Button("Normal") && Application.isPlaying)
				_HeartRateMonitorScript.SetHeartRateColour(_HeartRateMonitorScript.NormalColour);
			
			if (GUILayout.Button("Medium") && Application.isPlaying)
				_HeartRateMonitorScript.SetHeartRateColour(_HeartRateMonitorScript.MediumColour);

			if (GUILayout.Button("Bad") && Application.isPlaying)
				_HeartRateMonitorScript.SetHeartRateColour(_HeartRateMonitorScript.BadColour);
			
			if (GUILayout.Button("Flatline") && Application.isPlaying)
				_HeartRateMonitorScript.SetHeartRateColour(_HeartRateMonitorScript.FlatlineColour);
			
			EditorGUILayout.EndHorizontal();		
			
			EditorGUILayout.EndVertical();	
		}
		#endregion
		
		if (GUI.changed)
			EditorUtility.SetDirty(_HeartRateMonitorScript);
		
		this.Repaint();
	}

	void GetHeader()
	{  
		Texture thisTextureHeader = (Texture) AssetDatabase.LoadAssetAtPath("Assets/SWP_HeartRateMonitor/Editor/Textures/SketchWorkHeader.png", typeof(Texture));
		
		if (thisTextureHeader != null)
		{
			Rect thisRect = GUILayoutUtility.GetRect(0f, 0f);
			thisRect.width = thisTextureHeader.width;
			thisRect.height = thisTextureHeader.height;
			GUILayout.Space(thisRect.height);
			GUI.DrawTexture(thisRect, thisTextureHeader);
		}
	}
}
