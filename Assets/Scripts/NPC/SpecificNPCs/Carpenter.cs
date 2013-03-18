using UnityEngine;
using System.Collections;

public class Carpenter : NPC {
	string whatToSay;
	
	protected override void Init() {
		base.Init();
		animationData = GetComponent<SmoothMoves.BoneAnimation>();
	}
	protected override EmotionState GetInitEmotionState(){
		return (new CarpenterIntroEmotionState(this));
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
	
	public class CarpenterIntroEmotionState : EmotionState{
		public CarpenterIntroEmotionState(NPC toControl) : base(toControl, "You can play with my son when he finishes building his treehouse. Now where did I place my old tools."){
			_choices.Add(new Choice("Apples", "I will give you some of our apples if you help me find my old tools."));
			_choices.Add(new Choice("Son", "He is going to be a great carpenter like his father and father's father one day."));
			_acceptableItems.Add("Tools");
			_acceptableItems.Add("FishingRod");
			_acceptableItems.Add("Apple[Carpenter]");
		}
		public bool hasStolenApple = false;
		public bool hasReturnedApple = false;
		public GameObject treeHouse;
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Carpenter"){
				Debug.Log(npc + " is reacting to: ");
				switch (item.name){
					case "Tools":
						_npcInState.UpdateChat("These will be great for my son. Have some apples as a reward.");
						// Tree house to be built
						treeHouse = GameObject.Find("Treehouse");
						treeHouse.SetActiveRecursively(true);
						// TODO - Update son's dialogue
						//CarpenterSon carpenterSonScript = GetComponent<CarpenterSon>();
						//carpenterSonScript.FatherGivenTools();
						break;
					case "Fishing Rod":
						Debug.Log("NPC: " +npc + " Item: " +item.name + " in mom");
						// Tree house to be built
						treeHouse = GameObject.Find("Treehouse");
						treeHouse.SetActiveRecursively(true);
						_npcInState.UpdateChat("Ah, I guess there isn't too much harm in it. I'll take him fishing later, but the tree house comes first.");
						break;
					case "Apple[Carpenter]":
						if (hasStolenApple){
							_npcInState.UpdateChat("Thanks.  I'm sure it was just a harmless mistake.");
							// TODO - set disposition back
						}
						else{
							_npcInState.UpdateChat("It is yours to keep.");
						}
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
	}
}


