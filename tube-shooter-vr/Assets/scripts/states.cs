using UnityEngine;
using System.Collections;

public class states : MonoBehaviour {
	static public int state;
	private string text;
	static public int GAME_STATE = 1;
	static public int MENU_STATE = 0;
	static public int GOD_STAT = 42;
	// Use this for initialization
	void Start () {
		state = MENU_STATE;
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown(KeyCode.JoystickButton9)) {
//			state = state==GAME_STATE ? MENU_STATE:GAME_STATE;
//			text = "Play";
//		}
	}
//	void OnGUI() {
//		if(GUI.Button(new Rect(0, 0, 100, 50), text)) {
//			state = state==GAME_STATE ? MENU_STATE:GAME_STATE;
//		}
//		state==MENU_STATE ? 
//	}

}
