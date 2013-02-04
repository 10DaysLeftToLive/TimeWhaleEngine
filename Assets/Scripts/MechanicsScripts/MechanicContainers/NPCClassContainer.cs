using UnityEngine;
using System.Collections;

public class NPCClassContainer : LinkObjectContainer<npcClass> {
	public void UpdateDisposition(float newDisposition){
		npcClass young = Get (CharacterAgeState.YOUNG);
		npcClass middle = Get (CharacterAgeState.MIDDLE);
		npcClass old = Get (CharacterAgeState.OLD);
		
		if (young != null){
			young.SetDisposition(newDisposition);
		}
		if (middle != null) {
			middle.SetDisposition(newDisposition);
		} 
		if (old != null){
			middle.SetDisposition(newDisposition);
		}
	}
}
