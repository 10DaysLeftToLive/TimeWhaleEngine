using UnityEngine;
using System.Collections;

// To be attached to the player to allow for detecting a click on the player
public class OnPlayerClick : OnClick {
	protected override void DoClick(ClickPositionArgs e){
		EventManager.instance.RiseOnClickOnPlayerEvent();
	}
}
