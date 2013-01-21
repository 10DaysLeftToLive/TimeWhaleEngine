using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class npcClass : MonoBehaviour {
	
	public string npcName;
	public TextMesh chat;
	public GameObject symbol;
	public GameObject player;
	public float npcDisposition;
	
	private List<Item> itemReactions;
	private int randomVariable;
	private float speed = .01f;
	private float symbolDuration = 3;
	private float timer = 6;
	private float actionTimer;
	private float distanceFromPlayer;
	private string message;
	private string outOfRangeMessage = "!";
	private Vector3 playerPos;
	private Vector3 npcPos, startPos;
	private GameObject newImg;
	private enum State {Idle, Patrol, Moving};
	private State npcState, previousState;
	
	// Use this for initialization
	void Start () {
		npcState = State.Idle;
		actionTimer = timer;
	}
	
	// Update is called once per frame
	void Update () {
		actionTimer -= Time.deltaTime;
		playerPos = player.transform.position;/*
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
		
		switch(npcState){
			case State.Idle: NpcIdle(); break;
			case State.Patrol: NpcPatrol(4); break;
			case State.Moving: NpcMove(); break;
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
			
			newImg.transform.Rotate(new Vector3(0,0,180));
			StartCoroutine(Delay());
			
		}
	}
	
	IEnumerator Delay(){
		yield return new WaitForSeconds(symbolDuration);
		DestroyObject(newImg);
		npcState = previousState;
	}
	
	IEnumerator Actions(){
		yield return new WaitForSeconds(2);
		int ran = Random.Range(1,4);
		print (ran);
		switch (ran){
		case 1: Move(0); break;
		case 2: Move(1); break;
		case 3: Move(-1); break;
		}
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
		foreach (Item item in itemReactions){
			if (item.name == itemToReactTo){
				Debug.Log(name + " reacted to " + item.name);
				UpdateDisposition(item.dispositionChange);
				MetricsRecorder.RecordInteraction(name, item.name, item.dispositionChange);
			}
		}
	}
	#endregion
	
	public void ChangeState(int num){
		switch(num){
		case 0: npcState = State.Idle; break;
		case 1: npcState = State.Patrol; startPos = this.transform.position; break;
		case 2: npcState = State.Moving; break;
		}
	}
	
	public int GetState(){
	return (int)npcState;	
	}
	
	private void NpcIdle(){
		//do some idle animation	
	}
	
	private void NpcPatrol(int distance){
		if (actionTimer <= 0){
			randomVariable = Random.Range(1,4);
			actionTimer  = timer;
			print(randomVariable);
		}else if (actionTimer <= timer/2 && randomVariable > 1){
			randomVariable = 1;
		}
			switch (randomVariable){
				case 1: 
				Move(0);
				break;
				case 2:
				if (Mathf.Abs(startPos.x - npcPos.x) < distance || npcPos.x < startPos.x){
					Move(1);
				}
				break;
				case 3:
				if (Mathf.Abs(startPos.x - npcPos.x) < distance || npcPos.x > startPos.x){
					Move(-1);
				}
				break;
			}
	}
	
	private void NpcMove(){
		//move to some position	
	}
	
	private void Move(int direction){
		npcPos.x += speed*direction;
		transform.position = npcPos;
	}
}
