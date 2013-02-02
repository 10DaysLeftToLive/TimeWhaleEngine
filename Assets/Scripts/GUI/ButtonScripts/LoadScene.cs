using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject nguiRoot;
	
	// Use this for initialization
	public void Load () {
		PlayerPrefs.SetString(Strings.NextLevel, Strings.Level1);
		Application.LoadLevel(Strings.LoadingScreen);
		nguiRoot.SetActiveRecursively(false);
	}
}