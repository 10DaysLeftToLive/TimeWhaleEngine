using UnityEngine;
using System.Collections;

public class InteractableOnClick : OnClickNextToPlayer {
	protected override void DoClickNextToPlayer(){
		playerCharacter.InteractWithObject(this.gameObject);
	}
	
	public void Enable(){
		EventManager.instance.mOnClickEvent += 	delagate;
	}
	
	public void Disable(){
		EventManager.instance.mOnClickEvent -= 	delagate;
	}
}
