using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {
	
	void OnEnable() {
		Application.LoadLevelAsync(Strings.LevelBase);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
