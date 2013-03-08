using UnityEngine;
using System.Collections;
using SmoothMoves;

public abstract class Character : PauseObject {
	protected State currentState;
	private static float RIGHT = 1;
	private static float LEFT = -1;
	private Inventory invenory;
	private GrabableObject attachedObject = null;
	
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
	
	protected override void UpdateObject(){
		currentState.Update();
	}
	
	public void EnterState(State newState){		
		currentState.OnExit(); // Exit the current state
		currentState = newState; // Update the current state
		newState.OnEnter(); // Enter the new state
	}
	
	public bool AnimationPlaying(){
		return (true); //TODO
	}
	
	public void ChangeAnimation(string newAnimation){
		Debug.Log("Chaning animation to " + newAnimation);
		// TODO
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
	
	public Vector3 GetFeet(){
		Vector3 feetPos = this.transform.position;
		feetPos.y = this.transform.position.y - this.collider.bounds.size.y/2;
		return (feetPos);
	}
}
