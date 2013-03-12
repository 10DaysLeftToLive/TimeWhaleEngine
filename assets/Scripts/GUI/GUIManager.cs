using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	private Rect ageUp;
	private Rect ageDown;
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
		ageUp = ScreenRectangle.NewRect(.01f,0f);
		ageDown = ScreenRectangle.NewRect(.01f,.15f);
		
		levelManager = FindObjectOfType(typeof(LevelManager)) as LevelManager;
	}
	
	void OnGUI(){
		if (GUI.Button(ageUp, "Shift Up")){
		 	levelManager.ShiftUpAge();
		}
		if (GUI.Button(ageDown, "Shift Down")){
		 	levelManager.ShiftDownAge();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
