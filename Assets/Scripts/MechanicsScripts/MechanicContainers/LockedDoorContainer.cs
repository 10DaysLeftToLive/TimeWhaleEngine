using UnityEngine;
using System.Collections;

public class LockedDoorContainer : LinkObjectContainer<LockedDoor> {
	public void UnlockLinked(){
		LockedDoor young = Get (CharacterAgeState.YOUNG);
		LockedDoor middle = Get (CharacterAgeState.MIDDLE);
		LockedDoor old = Get (CharacterAgeState.OLD);
		
		if (young != null){
			young.Unlock();
		}
		if (middle != null) {
			middle.Unlock();
		} 
		if (old != null){
			old.Unlock();
		}
	}
}
