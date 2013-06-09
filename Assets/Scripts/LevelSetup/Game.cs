using UnityEngine;
using System.Collections;

/// <summary>
/// Game.cs
/// 	Used for all high level functions
/// </summary>
public class Game  {
	public static void Reset(){
		Application.LoadLevel(Application.loadedLevel);
		GUIManager.Instance.HidePauseMenu();//just in case
	}
	
	public static void GoToMainMenu(){
		Debug.Log("MainMenu");
		Application.LoadLevel(Strings.TitleMenu);
	}
	
	public static void Quit(){
		Debug.Log("Quiting");
		Application.Quit();
	}	
}
