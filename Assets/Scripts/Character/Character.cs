using UnityEngine;
using System.Collections;
using SmoothMoves;

/*
 *  Character.cs
 * 		The base class for both player and NPC
 * 		This should not be edited for specific characters put any specific code in the children classes.
 */
public abstract class Character : PauseObject {
	#region Static Fields
	private static float RIGHT = 1;
	private static float LEFT = -1;
	#endregion
	
	#region Fields
	protected State currentState;
	private Inventory invenory;
	private GrabableObject attachedObject = null;
	public BoneAnimation animationData;
	#endregion
	
	public GrabableObject AttachedObject {
		get {return (attachedObject);}
	}
	
	public Inventory Inventory{
		get { return invenory; }
	}
	
	void Start () {
		currentState = new IdleState(this);
		EnterState(new IdleState(this));
		invenory = new Inventory(this.transform);// should give the right hand of the bone animation
		Init();
	}
	
	protected abstract void Init();
	protected virtual void CharacterUpdate(){}
	
	protected override void UpdateObject(){
		currentState.Update();
		CharacterUpdate();
	}
	
	public void EnterState(State newState){		
		currentState.OnExit(); // Exit the current state
		currentState = newState; // Update the current state
		newState.OnEnter(); // Enter the new state
	}
	
	public void LookRight(){
		this.transform.localScale = new Vector3(RIGHT, 1, 1);
	}
	
	public void LookLeft(){
		this.transform.localScale = new Vector3(LEFT, 1, 1);
	}
	
	public void AttachTo(GameObject toAttachTo){
		attachedObject = toAttachTo.GetComponent<GrabableObject>();
		attachedObject.AttachToPlayer(this.gameObject);
	}
	
	public void DetachFrom(GameObject toDetachFrom){
		attachedObject = null;
		toDetachFrom.GetComponent<GrabableObject>().DetachFromPlayer();
	}
	
	public void ForceChangeToState(State newState){
		// TODO need to enter the correct idle state the change to the new one.
		//EnterState 
	}
	
	public void PlayAnimation(string animation){
		animationData.Play(animation);
	}
	
	public Vector3 GetFeet(){
		Vector3 feetPos = this.transform.position;
		feetPos.y = this.transform.position.y - this.collider.bounds.size.y/2;
		return (feetPos);
	}
}
