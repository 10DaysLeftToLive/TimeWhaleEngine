using UnityEngine;
using System.Collections;

public class NPCClassContainer : LinkObjectContainer<npcClass> {
	public void UpdateDisposition(float newDisposition){
		Get(CharacterAgeState.YOUNG).SetDisposition(newDisposition);
		Get(CharacterAgeState.MIDDLE).SetDisposition(newDisposition);
		Get(CharacterAgeState.OLD).SetDisposition(newDisposition);
	}
}
