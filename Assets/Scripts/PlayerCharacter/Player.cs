using UnityEngine;
using System.Collections;

public class Player : Character {
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToInteract);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
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
		
		if (tag == Strings.tag_CarriableItem){
			EnterState(new MoveThenDoState(this, e.clickedObject.transform.position, new PickUpItemState(this, e.clickedObject)));
		} else if (tag == Strings.tag_Pushable){
			EnterState(new MoveThenDoState(this, e.clickedObject.transform.position, new GrabOntoState(this, e.clickedObject)));
		}
	}
}
