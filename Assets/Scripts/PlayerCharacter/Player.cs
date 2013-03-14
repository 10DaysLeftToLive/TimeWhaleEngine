using UnityEngine;
using System.Collections;

public class Player : Character {
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToInteract);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
		EventManager.instance.mOnClickOnPlayerEvent += new EventManager.mOnClickOnPlayerDelegate (OnClickOnPlayer);
	}
	
	// We want to be able to switch to move at any state when the player clicks
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		
		Debug.Log("Click on no object  at point " + pos);
		// Will need to be changed with later refactoring
		if (currentState.GetType() == typeof(IdleState) || currentState.GetType() == typeof(ClimbIdleState)){ // if we are idled or climbing idled
			EnterState(new MoveState(this, pos)); // move normaly
		} else if (currentState.GetType() == typeof(GrabIdleState)){ // if we are attached to an object 
			EnterState(new GrabMoveState(this, pos));
		} else if (currentState.GetType() == typeof(MoveState)){
			EnterState(new MoveState(this, pos));
		} else if (currentState.GetType() == typeof(GrabMoveState)){
			EnterState(new GrabMoveState(this, pos));
		}
    }
	
	private void OnClickToInteract(EventManager EM, ClickedObjectArgs e){
		Debug.Log("Click on " + e.clickedObject.name + " with tag " + e.clickedObject.tag + " at point " + e.clickedObject.transform.position);
		
		string tag = e.clickedObject.tag;
		
		Vector3 goal = e.clickedObject.transform.position;
		goal.z = this.transform.position.z;
		
		if (tag == Strings.tag_CarriableItem){
			EnterState(new MoveThenDoState(this, goal, new PickUpItemState(this, e.clickedObject)));
		} else if (tag == Strings.tag_Pushable){
			if (currentState.GetType() == typeof(GrabIdleState)){
				EnterState(new LetGoOfState(this, e.clickedObject));
			} else {
				if (Inventory.HasItem()){
					Inventory.DropItem(GetFeet());
				}
				EnterState(new MoveThenDoState(this, goal, new GrabOntoState(this, e.clickedObject)));
			}
		} else if (tag == Strings.tag_NPC){
			NPC toTalkWith = (NPC)e.clickedObject.gameObject.GetComponent<NPC>();
			Vector3 currentPos = this.transform.position;
			Vector3 goalPosInfront = Utils.GetPointInfrontOf(currentPos, toTalkWith.gameObject);
			Debug.Log("Goal was " + toTalkWith.transform.position + " infront = " + goalPosInfront);
			EnterState(new MoveThenDoState(this, goalPosInfront, new TalkState(this, toTalkWith)));
		}
	}
	
	private void OnClickOnPlayer(EventManager EM){
		if (currentState.GetType() == typeof(TalkState)){ // if we are talking exit before doing anything else.
			EnterState(new IdleState(this));
		} else {
			if (Inventory.HasItem()){
				Inventory.DropItem(GetFeet());
			}
		}
	}
}
