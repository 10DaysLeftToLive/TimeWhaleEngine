using UnityEngine;
using System.Collections;

/// <summary>
/// Game.cs
/// 	Used for all high level functions
/// </summary>
public class Game  {
	public static void Reset(){
		Time.timeScale = 1;
		OneDayClock.Instance.Restart();
		Application.LoadLevel(Strings.LevelBase);
	}
	
	public static void GoToMainMenu(){
		Application.LoadLevel(Strings.TitleMenu);
	}
	
	public static void GoToEndScene(){
		Application.LoadLevel(Strings.EndScreen);
	}
	
	public static void Quit(){
		Application.Quit();
	}	
}
