using UnityEngine;
using System.Collections;

public class EndOfLevelOnClick : OnClickNextToPlayer {
	public string levelToGoto;
	
	protected override void DoClickNextToPlayer(){
		Application.LoadLevel(levelToGoto);
	}
}
