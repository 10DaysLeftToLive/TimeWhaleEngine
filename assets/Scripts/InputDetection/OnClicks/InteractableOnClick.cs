using UnityEngine;
using System.Collections;

/*
 * InteractableOnClick.cs
 * 	Implements DoClickNextToPlayer and will call the PlayerController's InteractWithObject.
 *  It can be enabled and disabled by external classes.
 */

public class InteractableOnClick : OnClickNextToPlayer {
	protected override void DoClickNextToPlayer(){
		//InteractWithPlayer();
	}
	
	protected virtual void InteractWithPlayer(){
		
	}
	
	public void Enable(){
		EventManager.instance.mOnClickEvent += 	delagate;
	}
	
	public void Disable(){
		EventManager.instance.mOnClickEvent -= 	delagate;
	}
}
