using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class npcClass : MonoBehaviour {
	public string npcName;
	public TextMesh chat;
	public GameObject symbol;
	public float npcDisposition;
	
	public int id = 0;
	
	private List<Item> itemReactions;
	private int randomVariable;
	private float speed = 5f;
	private float symbolDuration = 3;
	private float timer = 6;
	private float actionTimer;
	private float distanceFromPlayer;
	private string message;
	private string outOfRangeMessage = "!";
	private Vector3 playerPos;
	private Vector3 npcPos;
	private GameObject newImg;
	private enum State {Idle, Patrol, Moving};
	private GameObject player;
	private State npcState, previousState;
	private Vector3[] path;
	private int pathIndex;
	
	// Use this for initialization
	void Start () {
		speed *= Time.deltaTime;
		npcState = State.Idle;
		actionTimer = timer;
		player = GameObject.Find(Strings.Player);	
	}
	
	// Update is called once per frame
	void Update () {/*
		actionTimer -= Time.deltaTime;
		playerPos = player.transform.position;
		npcPos = this.transform.position;
		distanceFromPlayer = Mathf.Abs(playerPos.x - npcPos.x);
		if (distanceFromPlayer < 5){
			chat.text = message;
		}else {
			chat.text = outOfRangeMessage;
		}
		
		if(Input.GetKeyDown("c") && distanceFromPlayer < 2){
			DisplayImage();
		}
		if ( npcState == State.Moving && pathIndex >= path.Length)
			npcState = State.Idle;
		
		switch(npcState){
			case State.Idle: NpcIdle(); break;
			//case State.Patrol: NpcPatrol(4); break;
			case State.Moving: Move(path[pathIndex]); break;
		}*/
	}
	
	public void UpdateText(string message){
		this.message = message;
	}
	
	public void UpdateOutOfRange(string message){
		outOfRangeMessage = message;
	}
	
	public void DisplayImage(){
		if (newImg == null){
			previousState = npcState;
			npcState = State.Idle;
			newImg = (GameObject)Instantiate(symbol,new Vector3(npcPos.x, npcPos.y + 2, npcPos.z),this.transform.rotation);
			
			if (npcName == "Charlie"){
				newImg.renderer.material.mainTextureOffset =  new Vector2(0,.5f); //happy
			}else if (npcName == "Susan"){
				newImg.renderer.material.mainTextureOffset =  new Vector2(.5f,.5f);	//sad
			}
			StartCoroutine(Delay());
			
		}
	}
	
	IEnumerator Delay(){
		yield return new WaitForSeconds(symbolDuration);
		DestroyObject(newImg);
		npcState = previousState;
	}
	
	#region disposition
	public void SetDisposition(float disp) {
		npcDisposition = disp;
	}
	
	public float GetDisposition(){
		return npcDisposition;	
	}
	
	public float UpdateDisposition(float disp) {
		npcDisposition += disp;
		return npcDisposition;
	}
	#endregion
	
	#region item interactions
	public void SetInteractions(List<Item> items){
		itemReactions = items;
	}
	#endregion
	
	#region item reaction
	public void ReactTo(string itemToReactTo){
		bool hasReacted = false;
		
		foreach (Item item in itemReactions){
			if (item.name == itemToReactTo){
				UpdateDisposition(item.dispositionChange);
				NPCDispositionManager.instance.UpdateWithId(id, GetDisposition());
				MetricsRecorder.RecordInteraction(name, item.name, item.dispositionChange);
				hasReacted = true;
				DoReaction(item.name);
				break;
			}
		}
		
		if (!hasReacted) Debug.Log("No Interaction was set between " + name + " and " + itemToReactTo);
	}
	
	protected virtual void DoReaction(string itemToReactTo){}
	#endregion
	
	public void ChangeState(int num){
		switch(num){
		case 0: npcState = State.Idle; break;
		case 1: npcState = State.Patrol; break;
		case 2: npcState = State.Moving; break;
		}
	}
	
	public int GetState(){
	return (int)npcState;	
	}
	
	private void NpcIdle(){
		//do some idle animation	
	}
	
	public void NpcMove(Vector3[] dest){
		npcState = State.Moving;
		path = dest;
		pathIndex = 1;
	}
	
	private void Move(Vector3 dest){
		if (npcPos.x < dest.x){
			npcPos.x += speed;
		}else if (npcPos.x > dest.x){
			npcPos.x -= speed;	
		}
		if (npcPos.y < dest.y){
			npcPos.y += speed;
		}else if (npcPos.y > dest.y){
			npcPos.y -= speed;	
		}
		
		if (npcPos.x < dest.x && npcPos.x + speed*1.5 > dest.x)
			npcPos.x = dest.x;
		if (npcPos.y < dest.y && npcPos.y + speed*1.5 > dest.y)
			npcPos.y = dest.y;
		transform.position = npcPos;
		
		if (NearPoint(dest)){
			pathIndex++;
			if (pathIndex >= path.Length)
				return;
		}
	}
	
	private bool NearPoint(Vector3 point){
		float difference = .1f;
		if (npcPos.x  < point.x + difference && npcPos.x > point.x - difference){
			if (npcPos.y  < point.y + difference && npcPos.y > point.y - difference)
				return true;
		}
	return false;
	}
	
	public Vector3 GetPos(){
		return npcPos;	
	}
}
