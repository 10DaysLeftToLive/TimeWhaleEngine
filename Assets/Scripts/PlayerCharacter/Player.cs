using UnityEngine;
using System.Collections;
using SmoothMoves;

public class Player : Character {
	public float walkSpeed = 2.0f;
	
	public ParticleSystem touchParticleEmitter;
	
	public Capsule smallHitBox;
	public Capsule bigHitbox;
	
	public NPC npcTalkingWith;
	private Inventory inventory;
	
	private float timeSinceLastHold = 1; // start moving at first moment
	private static float TIMEBETWEENHOLDMOVES = .35f;
	private static float HOLDMINDISTANCEX = 2; 
	private static float HOLDMINDISTANCEY = 1;
	
	public Inventory Inventory {
		get { return inventory; }
	}
	
	// Use this for initialization
	protected override void Init(){
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
		EventManager.instance.mOnClickHoldEvent += new EventManager.mOnClickHoldDelegate (OnHoldClick);
		EventManager.instance.mOnClickHoldReleaseEvent += new EventManager.mOnClickHoldReleaseDelegate(OnHoldRelease);
		EventManager.instance.mOnClickObjectEvent += new EventManager.mOnClickedObjectDelegate(OnObjectClick);
		
		AgeSwapMover.instance.SetPlayer(this);
		
		Transform leftHand = animationData.GetSpriteTransform("Left Hand");
		inventory = new Inventory(leftHand);
	}
	
	#region Click Handling
	public void OnObjectClick(EventManager EM, ClickedObjectArgs clickedObject){
		if (isGamePaused()) return;
		
		string tag = clickedObject.clickedObject.tag;
		
		if (tag == Strings.tag_Player){
			DoClickOnPlayer();
		} else if (tag == Strings.tag_NPC){
			DoClickOnNPC(clickedObject.clickedObject);
		} else if (tag == Strings.tag_CarriableItem){
			DoClickOnItem(clickedObject.clickedObject);
		}
	}
	
	private Vector3 pos;
	// We want to be able to switch to move at any state when the player clicks
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		if (isGamePaused()) return;
		if (currentState.GetType() == typeof(TalkState)){
			GUIManager.Instance.CloseInteractionMenu();
		}
		pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		
		if (currentState.GetType() == typeof(IdleState) || currentState.GetType().IsSubclassOf(typeof(MoveState))){ 
			EnterState(new MoveState(this, pos)); // If we are idling of in a movethen do state cancel and move to the new position
		} else if (currentState.GetType() == typeof(MoveState)){
			((MoveState) currentState).UpdateGoal(pos);
		}
		touchParticleEmitter.transform.localPosition = pos;
		touchParticleEmitter.Play();
    }
	
	private void DoClickOnPlayer(){
		if (Inventory.HasItem()){
			if (IsInteracting()){
				GUIManager.Instance.CloseInteractionMenu();
			}
			EnterState(new DropItemState(this));
		}
	}
	
	private void DoClickOnItem(GameObject item){
		if (IsInteracting()){
			GUIManager.Instance.CloseInteractionMenu();
		}
		if (Inventory.HasItem() && Inventory.GetItem() == item){
			EnterState(new DropItemState(this));
		} else {
			EnterState(new MoveThenDoState(this, item.transform.position, new PickUpItemState(this, item)));
		}
	}
	
	private void DoClickOnNPC(GameObject npc){
		NPC toTalkWith = (NPC)npc.GetComponent<NPC>();
		if (IsInteracting()){
			if (toTalkWith == npcTalkingWith){
				GUIManager.Instance.CloseInteractionMenu();
				return;
			}
			GUIManager.Instance.CloseInteractionMenu();
		}
		GoToInteractWithNPC(toTalkWith);
	}
	
	/// <summary>
	/// Goes to interact with the given npc. It will move if necessary
	/// </summary>
	private void GoToInteractWithNPC(NPC npcToInteractWith){
		Vector3 currentPos = this.transform.position;
		Vector3 goalPosInfront = Utils.GetPointInfrontOf(currentPos, npcToInteractWith.gameObject);
		EnterState(new MoveThenDoState(this, goalPosInfront, new TalkState(this, npcToInteractWith)));
	}
	#endregion
	
	#region Hold To Move
	private void OnHoldClick(EventManager EM, ClickPositionArgs e){
		
		if (isGamePaused()) return;
		if (currentState.GetType() == typeof(TalkState)){
			GUIManager.Instance.CloseInteractionMenu();
		}
		
		pos = Camera.main.ScreenToWorldPoint(e.position);
		
		if (HoldIsTooClose(pos)) {
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
	#endregion
	
	#region Age Changing
	public void ChangeAge(CharacterAge newAge, CharacterAge previousAge){
		if (IsInteracting()){
			CloseInteraction();	
		}
		if (currentState.GetType() == typeof(MoveState)) {
			EnterState(new IdleState(this));
		}
		
		ChangeAnimation(newAge.boneAnimation);
		AgeSwapMover.instance.ChangeAgePosition(newAge, previousAge);
		DebugManager.instance.Log("Transition to: " + newAge.stateName + ", from " + previousAge.stateName, "Player");
		//ChangeHitBox(newAge, previousAge);
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
	
	public void ChangeAnimation(BoneAnimation newAnimation){
		if (animationData != null) {
			Utils.SetActiveRecursively(animationData.gameObject, false);
		}
		
		animationData = newAnimation;
		Utils.SetActiveRecursively(animationData.gameObject, true);
		if (Inventory != null) {
			Transform leftHand = animationData.GetSpriteTransform("Right Hand");
			Inventory.ChangeRightHand(leftHand);
		}
	}
	#endregion
	
	#region Inventory
	public void PickUpObject(GameObject toPickUp){
		Inventory.PickUpObject(toPickUp);
	}
	
	public void DisableHeldItem(){
		Inventory.DisableHeldItem();
	}
	#endregion
	
	#region Interaction
	private bool IsInteracting() {
		return (currentState is TalkState);	
	}
	
	private void CloseInteraction() {
		GUIManager.Instance.CloseInteractionMenu();
	}
	
	public void LeaveInteraction() {
		npcTalkingWith = null;
		EnterState(new IdleState(this));
	}
	#endregion
}
