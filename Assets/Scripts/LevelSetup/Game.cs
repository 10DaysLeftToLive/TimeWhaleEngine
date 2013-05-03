using UnityEngine;
using System.Collections;

/// <summary>
/// Game.cs
/// 	Used for all high level functions
/// </summary>
public class Game  {
	public static void Reset(){
	}
	
	public static void Quit(){
		Debug.Log("Quiting");
		Application.Quit();
	}	
}
