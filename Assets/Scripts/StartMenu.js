#pragma strict

var skin : GUISkin;

function OnGUI () {
	
	GUI.skin = skin;
	
	if (GUI.Button (Rect
			(((Screen.width / 2) - 70), 300, 140, 30), "Start game")) {
		if (GameObject.Find("VariableStorage(Clone)") != null) {
			GameObject.Destroy(GameObject.Find("VariableStorage(Clone)"));
		}
        Application.LoadLevel("level1");
    }
    
    if (GUI.Button (Rect
			(((Screen.width / 2) - 70), 350, 140, 30), "Instructions")) {
		Application.LoadLevel("info");
	}
	
	if (GUI.Button (Rect
			(((Screen.width / 2) - 70), 400, 140, 30), "Exit game")) {
		Application.Quit();
	}
}