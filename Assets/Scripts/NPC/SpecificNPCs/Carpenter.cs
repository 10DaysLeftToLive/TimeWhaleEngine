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
		public CarpenterIntroEmotionState(NPC toControl) : base(toControl, "You can play with my son when he finishes building his treehouse. Now where did I place my old tools?"){
			_choices.Add(new Choice("Apples", "I will give you some of our apples if you help me find my old tools."));
			_choices.Add(new Choice("Son", "He is going to be a great carpenter like his father and father's father one day."));
			_acceptableItems.Add("ToolBox");
			_acceptableItems.Add("FishingRod");
			_acceptableItems.Add("Apple");
			_acceptableItems.Add("Apple[Carpenter]");
			
			//EventManager.instance.mOnPlayerPickupItemEvent += new EventManager.mOnPlayerPickupItemDelegate (OnPickedUpItem);
			//EventManager.instance.mOnPlayerPickupItemEvent += new EventManager.mOnPlayerPickupItemDelegate (OnCarpenterApplePickedUp);
		}
		public bool hasStolenApple = false;
		public bool hasReturnedApple = false;
		public bool hasGivenTools = false;
		public GameObject treeHouse;
		public GameObject apple;
		
		public override void ReactToItemInteraction(string npc, GameObject item){
			if (item != null && npc == "Carpenter[SWITCH_SPRITES]"){
				Debug.Log(npc + " is reacting to: ");
				switch (item.name){
					case "ToolBox":
						_acceptableItems.Remove("Apple[Carpenter]");
						_npcInState.UpdateChat("These will be great for my son. Have some apples as a reward.");
						this._textToSay = "I'm excited for what my son will build with his tools!";
						// Tree house to be built
						//apple = GameObject.Find("Apple[Carpenter]");
						//apple.SetActiveRecursively(true);
						hasGivenTools = true;
						// Update son's dialogue
						//CarpenterSon carpenterSonScript = GetComponent<CarpenterSon>();
						//carpenterSonScript.currentEmotion._textToSay = "Why did you give him that?  Now, he's gonna make me build stuff instead of letting me go fishing!";
					//carpenterSonScript.FatherGivenTools();
						break;
					case "FishingRod":
						Debug.Log("NPC: " +npc + " Item: " +item.name + " in mom");
						// Tree house to be built
						//treeHouse = GameObject.Find("Treehouse");
						//treeHouse.SetActiveRecursively(true);
						// set new son dialogue
						_npcInState.UpdateChat("Ah, I guess there isn't too much harm in it. I'll take him fishing later, but the tree house comes first.");
						this._textToSay = "I hope my son does good work, even with a new fishing rod to distract him.";
						break;
					case "Apple":
						_npcInState.UpdateChat("I already have enough apples");
						break;
					case "Apple[Carpenter]":
						if (hasGivenTools){
							_npcInState.UpdateChat("It is yours to keep.");
						}
						else{
							this._textToSay = "You can play with my son when he finishes building his treehouse. Now where did I place my old tools?";
							_npcInState.UpdateChat("Thanks.  I'm sure it was just a harmless mistake.");
							_choices.Add(new Choice("Apples", "I will give you some of our apples if you help me find my old tools."));
							_choices.Add(new Choice("Son", "He is going to be a great carpenter like his father and father's father one day."));
							// TODO - set disposition back
						}
						break;
					default:
						break;
				}
			}
			
			if (item != null && npc == "CarpenterSon[SWITCH_SPRITES]"){
				switch (item.name){
					case "ToolBox":
						this._textToSay = "Thank you for finding my old tools for my son.";
						_acceptableItems.Remove("Apple[Carpenter]");
						hasGivenTools = true;
						//apple = GameObject.Find("Apple[Carpenter]");
						//apple.SetActiveRecursively(true);
						// Tree house to be built
						//treeHouse = GameObject.Find("Treehouse");
						//treeHouse.SetActiveRecursively(true);
						// Update father's dialogue
						//Carpenter carpenterScript = GetComponent<Carpenter>();
						//carpenterScript.currentEmotion._textToSay = "Oh nice, you found my old tools. Now my son can start on that tree house. Have some apples as a reward.";
						break;
					case "FishingRod":
						// Tree house to be built
						//treeHouse = GameObject.Find("Treehouse");
						//treeHouse.SetActiveRecursively(true);
						this._textToSay = "Oh great!  Now I'm gonna have to spend my time fishing my son out of the river!";
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
			if(!hasGivenTools) {
				if(item.name == "Apple[Carpenter]"){
					// change chat to anger
					//_npcInState.UpdateChat("That is my apple. Give it back!");
					//this._textToSay = "That is my apple. Give it back!";
					
					_choices.Clear();
					
					//CarpenterSon carpenterSonScript = GetComponent<CarpenterSon>();
					//carpenterSonScript.currentEmotion._textToSay = "My dad said you stole our apple! You're not my friend anymore!";
						
					// toggle chat
					//_npcInState.ToggleChat();
					_npcInState.OpenChat();
					//_npcInState.UpdateChat("It is yours to keep.");
					this._textToSay = "That is my apple. Give it back!";
					_npcInState.UpdateChat("That is my apple. Give it back!");
				}
			}
		}
	}
}


