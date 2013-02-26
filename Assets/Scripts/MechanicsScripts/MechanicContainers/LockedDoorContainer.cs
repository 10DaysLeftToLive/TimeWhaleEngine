using UnityEngine;
using System.Collections;

public class LockedDoorContainer : LinkObjectContainer<LockedDoor> {
	public override void Perform(){
		UnlockAll();
	}
	
	public void UnlockAll(){
		foreach(CharacterAgeState state in linkedObjects.Keys){
			Get(state).Unlock();
		}
	}
}
