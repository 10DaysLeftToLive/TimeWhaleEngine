using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	void Start () {
		string levelToLoad = PlayerPrefs.GetString(Strings.NextLevel);
		Application.LoadLevel(levelToLoad);	
	}
}
