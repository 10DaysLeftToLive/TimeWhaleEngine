using UnityEngine;
using System.Collections;

public class Player : Character {
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToMove);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
	}
	
	// We want to be able to switch to move at any state when the player clicks
	// Sudden transisitions will be handled by the state's OnExit()
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		Debug.Log("OnClickToMove");
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		
		EnterState(new MoveState(this, pos));
    }
}
