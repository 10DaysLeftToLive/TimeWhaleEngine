using UnityEngine;
using System.Collections;

public class NPCClassContainer  : LinkObjectContainer<NPC> {
	private static int LOWERDISPLIMIT = 0;
	private static int UPPERDISPLIMIT = 10;
	
	public override void Perform(){
		
	}
	
	public void UpdateAll(int deltaDisposition){
		int currentDisposition;
		foreach(CharacterAgeState state in linkedObjects.Keys){
			currentDisposition = Get(state).GetDisposition();
			
			// Make sure the new disposition is within bounds
			if(currentDisposition < LOWERDISPLIMIT) {
				currentDisposition = LOWERDISPLIMIT;
			}
			else if(currentDisposition > UPPERDISPLIMIT) {
				currentDisposition = UPPERDISPLIMIT;	
			}
			
			Get(state).SetDisposition(currentDisposition + deltaDisposition);
		}
	}
}
