using UnityEngine;
using System.Collections;

public class PauseMenuPopUp : MonoBehaviour {
	public GameObject anchorToAddPopUp;
    public GameObject pausePrefab;
	private bool hasAddedPrefab = false;
	private bool popUpShowing = false;
	private GameObject pauseMenuObject;
	
	void TogglePopUp(){
		if (popUpShowing){
			Hide ();
		} else {
			Show();
		}
		popUpShowing = !popUpShowing;
	}
	
	void Show(){
		Debug.Log ("Showing PauseMenuPopUp");
		if (!hasAddedPrefab){
			pauseMenuObject = NGUITools.AddChild(anchorToAddPopUp, pausePrefab);
			hasAddedPrefab = true;
		} else {
			NGUITools.SetActive(pauseMenuObject, true);
		}
	}
	
	void Hide(){
		Debug.Log ("Hiding PauseMenuPopUp");
		NGUITools.SetActive(pauseMenuObject, false);
	}
}
