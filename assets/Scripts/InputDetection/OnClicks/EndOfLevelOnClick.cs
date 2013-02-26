using UnityEngine;
using System.Collections;

/*
 * EndOfLevelOnClick.cs
 * 	Implements DoClickNextToPlayer and will change the level
 * TODO: the next level should be set by the level manager
 */

public class EndOfLevelOnClick : OnClickNextToPlayer {
	public string levelToGoto;
	
	protected override void DoClickNextToPlayer(){
		Application.LoadLevel(levelToGoto);
	}
}
