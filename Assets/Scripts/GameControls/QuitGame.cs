using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {
	void Quit() {
		Debug.Log("Quiting");
		Application.Quit();	
	}
}
