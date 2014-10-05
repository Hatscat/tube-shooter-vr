using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	public GUISkin customSkin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		GUI.skin = customSkin;

		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			Application.LoadLevel(2);
		}

		// We provide the name of the Style we want to use as the last argument of the Control function
		GUILayout.Button ("I am a custom styled Button", "MyCustomControl");
		
		// We can also ignore the Custom Style, and use the Skin's default Button Style
		GUILayout.Button ("I am the Skin's Button Style");

	}
}
