using UnityEngine;
using System.Collections;

public class CarpenterSon : NPC {
	string whatToSay;
	
	protected override void Init() {
		base.Init();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
	}
	protected override EmotionState GetInitEmotionState(){
		return (new CarpenterSonIntroEmotionState(this));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new Schedule(this);

		Task standAround = new Task(new IdleState(this));
		
		schedule.Add(standAround);
		
		return (schedule);
	}
	
	protected override void LeftButtonCallback(string choice){
		Debug.Log(this.name + " left callback for choice " + choice);
		// TODO? this is for a chat dialoge
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCChoiceInteraction(this.gameObject, choice));
	}
	
	protected override void RightButtonCallback(){
		Debug.Log(this.name + " right callback");
		GameObject item = player.Inventory.GetItem();
		DoReaction(item);
	}
	
	protected override void DoReaction(GameObject itemToReactTo){
		
	}
	
	//public void FatherGivenTools(){
	//	this.textToSay = "Why did you give him that?  Now, he's gonna make me build stuff instead of letting me go fishing!";
	//}
	
	public class CarpenterSonIntroEmotionState : EmotionState{
		public CarpenterSonIntroEmotionState(NPC toControl) : base(toControl, "I wish I had a fishing rod, so I can go fishing instead of building this treehouse."){
			_choices.Add(new Choice("Fishing", "My dad says that I have to follow in our families footsteps and become a carpenter, but I just want to go fishing."));
			_choices.Add(new Choice("Apples", "My dad is very protective of our property"));
			_acceptableItems.Add("Tools");
			_acceptableItems.Add("FishingRod");
		}
		public GameObject treeHouse;
		public bool hasGivenTools = false;
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "CarpenterSon"){
				Debug.Log(npc + " is reacting to: ");
				switch (item.name){
					case "Tools":
						_npcInState.UpdateChat("Thanks for the tools, now I can build my treehouse.");
						// Tree house to be built
						treeHouse = GameObject.Find("Treehouse");
						treeHouse.SetActiveRecursively(true);
						// Update father's dialogue
						//Carpenter carpenterScript = GetComponent<Carpenter>();
						//carpenterScript.currentEmotion._textToSay = "Oh nice, you found my old tools. Now my son can start on that tree house. Have some apples as a reward.";
						break;
					case "FishingRod":
						// Tree house to be built
						treeHouse = GameObject.Find("Treehouse");
						treeHouse.SetActiveRecursively(true);
						_npcInState.UpdateChat("Fishing is gonna be so much fun!");
						break;
					default:
						break;
				}
			}
		}
		
		public override void ReactToChoiceInteraction(string npc, string choice){			

		}
		
		public override void ReactToEnviromentInteraction(string npc, string enviromentAction){
			
		}
		
		public override void ReactToItemPickedUp(GameObject item){
			if(item.name == "Apple[Carpenter]"){
				// change chat to anger
				this._textToSay = "My dad said you stole our apple! You're not my friend anymore!";
				
				//CarpenterSon carpenterSonScript = GetComponent<CarpenterSon>();
				//carpenterSonScript.currentEmotion._textToSay = "My dad said you stole our apple! You're not my friend anymore!";
					
				// toggle chat
				//_npcInState.ToggleChat();
			}
		}
	}
}


