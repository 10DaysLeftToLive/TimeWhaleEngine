using UnityEngine;
using System.Collections;

public class NPCClassContainer : LinkObjectContainer<npcClass> {
	public override void Perform(){
		
	}
	
	public void UpdateAll(int newDispostion){
		foreach(CharacterAgeState state in linkedObjects.Keys){
			Get(state).SetDisposition(newDispostion);
		}
	}
}
