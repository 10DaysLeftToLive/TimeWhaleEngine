using UnityEngine;
using System.Collections;

public class LockedDoorContainer : LinkObjectContainer<LockedDoor> {
	public void UnlockLinked(){
		Get(CharacterAgeState.YOUNG).Unlock();
		Get(CharacterAgeState.MIDDLE).Unlock();
		Get(CharacterAgeState.OLD).Unlock();
	}
}
