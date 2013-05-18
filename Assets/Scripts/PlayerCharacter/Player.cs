using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent(typeof(CharacterController))]
public class Player : Character {
	public float walkSpeed = 2.0f;
	
	public ParticleSystem touchParticleEmitter;
	
	public Capsule smallHitBox;
	public Capsule bigHitbox;
	
	public NPC npcTalkingWith;
	
	private float timeSinceLastHold = 1; // start moving at first moment
	private static float TIMEBETWEENHOLDMOVES = .35f;
	private static float HOLDMINDISTANCEX = 2; 
	private static float HOLDMINDISTANCEY = 1; 
	
	// Use this for initialization
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToInteract);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
		EventManager.instance.mOnClickOnPlayerEvent += new EventManager.mOnClickOnPlayerDelegate (OnClickOnPlayer);
		EventManager.instance.mOnClickHoldEvent += new EventManager.mOnClickHoldDelegate (OnHoldClick);
		EventManager.instance.mOnClickHoldReleaseEvent += new EventManager.mOnClickHoldReleaseDelegate(OnHoldRelease);
		
		AgeSwapMover.instance.SetPlayer(this);
	}
	
	private Vector3 pos;
	// We want to be able to switch to move at any state when the player clicks
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		if (currentState.GetType() == typeof(TalkState)) return;
		
		// Will need to be changed with later refactoring
		if (currentState.GetType() == typeof(IdleState) || currentState.GetType() == typeof(ClimbIdleState)){ // if we are idled or climbing idled
			EnterState(new MoveState(this, pos)); // move normaly
		} else if (currentState.GetType() == typeof(MoveState)){
			EnterState(new MoveState(this, pos));
		} 
		touchParticleEmitter.transform.localPosition = pos;
		touchParticleEmitter.Play();
    }
	
	private void OnClickToInteract(EventManager EM, ClickedObjectArgs e){
		string tag = e.clickedObject.tag;
		Vector3 goal = e.clickedObject.transform.position;
		goal.z = this.transform.position.z;
		
		if (tag == Strings.tag_CarriableItem){
			EnterState(new MoveThenDoState(this, goal, new PickUpItemState(this, e.clickedObject)));
		} else if (tag == Strings.tag_NPC){
			NPC toTalkWith = (NPC)e.clickedObject.gameObject.GetComponent<NPC>();
			Vector3 currentPos = this.transform.position;
			Vector3 goalPosInfront = Utils.GetPointInfrontOf(currentPos, toTalkWith.gameObject);
			EnterState(new MoveThenDoState(this, goalPosInfront, new TalkState(this, toTalkWith)));
		}
	}
	
	private void OnClickOnPlayer(EventManager EM){
		if (currentState.GetType() == typeof(TalkState)){ // if we are talking exit before doing anything else.
			CloseInteraction();
		} else {
			if (Inventory.HasItem()){
				Inventory.DropItem(GetFeet());
			}
		}
	}
	
	private void OnHoldClick(EventManager EM, ClickPositionArgs e){
		pos = Camera.main.ScreenToWorldPoint(e.position);
		if (currentState.GetType() == typeof(TalkState) || HoldIsTooClose(pos)){
			timeSinceLastHold = 1;
			return;
		}
		if (timeSinceLastHold > TIMEBETWEENHOLDMOVES){
			pos.z = this.transform.position.z;
			
			if (!(currentState is MoveState)){
				EnterState(new MoveState(this, pos)); // move normaly
			} else {
				((MoveState) currentState).UpdateGoal(pos);
			}
			
			timeSinceLastHold = 0;
		} else {
			timeSinceLastHold += Time.deltaTime;
		}
	}
	
	Vector3 playerScreenPos;
	private bool HoldIsTooClose(Vector3 clickPos){
		playerScreenPos = transform.position;
		return (Utils.CalcDistance(playerScreenPos.x, clickPos.x) < HOLDMINDISTANCEX && Utils.CalcDistance(playerScreenPos.y, clickPos.y) < HOLDMINDISTANCEY);
	}
	
	private void OnHoldRelease(EventManager EM){
		timeSinceLastHold = 1; // Make it so on the next hold we start right up
		EnterState(new IdleState(this));
	}
	
	public void ChangeAge(CharacterAge newAge, CharacterAge previousAge){
		if (IsInteracting()){
			CloseInteraction();	
		}
		if (currentState.GetType() == typeof(MoveState)){
			EnterState(new IdleState(this));
		}
		
		AgeSwapMover.instance.ChangeAgePosition(newAge, previousAge);
		
		ChangeHitBox(newAge, previousAge);
		ChangeAnimation(newAge.boneAnimation);
		Inventory.SwapItemWithCurrentAge(newAge.boneAnimation);
	}
	
	private void ChangeHitBox(CharacterAge newAge, CharacterAge previousAge){
		CharacterController charControl = GetComponent<CharacterController>();	
		charControl.radius = newAge.capsule.radius;
		charControl.height = newAge.capsule.height;
		charControl.center = newAge.capsule.center;
		
		if(previousAge.stateName == CharacterAgeState.YOUNG){
			if(newAge.stateName == CharacterAgeState.MIDDLE || newAge.stateName == CharacterAgeState.OLD){
				transform.position = new Vector3(transform.position.x, transform.position.y + newAge.capsule.height/2, + transform.position.z);
			}
		}
	}
	
	public void PickUpObject(GameObject toPickUp){
		//Debug.Log ("Player transform: " + animationData.mLocalTransform);
		Inventory.PickUpObject(toPickUp);
	}
	
	public void DisableHeldItem(){
		Inventory.DisableHeldItem();
	}
	
	public bool CheckTransitionPositionSuccess(CharacterAge newAge, CharacterAge previousAge){
		return AgeSwapMover.instance.CheckTransitionPositionSuccess(newAge, previousAge);
	}
	
	public void ChangeAnimation(BoneAnimation newAnimation){
		if (animationData != null){
			Utils.SetActiveRecursively(animationData.gameObject, false);
		}
		
		animationData = newAnimation;
		Utils.SetActiveRecursively(animationData.gameObject, true);
		if (Inventory != null) {
			Transform rightHand = animationData.GetSpriteTransform("Right Hand");
			Inventory.ChangeRightHand(rightHand);
		}
	}
	
	private bool IsInteracting(){
		return (currentState is TalkState);	
	}
	
	private void CloseInteraction(){
		GUIManager.Instance.CloseInteractionMenu();
	}
	
	public void LeaveInteraction(){
		EnterState(new IdleState(this));
	}
}
