using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BazzarmanYoung specific scripting values
/// </summary>
public class BazzarmanYoung : NPC {
	InitialEmotionState state;
	protected override void Init() {
		id = NPCIDs.BAZAAR_MAN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		state  = new InitialEmotionState(this, "Eric is adding stuff here!");
		state.PassStringToEmotionState("We have the best deals in town!");
		state.PassStringToEmotionState("Come one, come all to the best prices!");
		state.PassStringToEmotionState("We take anything!");
		state.PassStringToEmotionState("Hurry, hurry! These deals won't last forever!");
		return (state);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		string[] stringList = new string[30];
		int stringCounter = 0;
		Queue<string> inventory = new Queue<string>();
		int startingInventory = 3;
		int currentInventory = 0;
		Reaction randomMessage;
		Reaction gavePendant;
		Reaction gaveSunflowerSeed;
		Reaction gaveApple, gaveTools, gaveSeaShell, gaveRose, gaveCaptainsLog, gaveToySword, gavePortrait, gaveRope, gaveVegetable;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			randomMessage = new Reaction();
			gavePendant = new Reaction();
			gaveSunflowerSeed = new Reaction();
			gaveApple = new Reaction();
			gaveTools = new Reaction();
			gaveSeaShell = new Reaction();
			gaveRose = new Reaction();
			gaveCaptainsLog = new Reaction();
			gaveToySword = new Reaction();
			gavePortrait = new Reaction();
			gaveRope = new Reaction();
			gaveVegetable = new Reaction();
			
			SetupInventory();
			
			#region Item Swaping
			gaveApple.AddAction(new NPCTakeItemAction(toControl));
			gaveApple.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "Where did you find such a good apple?"));
			gaveApple.AddAction(new NPCGiveItemAction(toControl, GiveItem)); // gives random item
			gaveApple.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(gaveApple));
			
			
			gaveTools.AddAction(new NPCTakeItemAction(toControl));
			gaveTools.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "The construction work going on on the other side of the island really appreciated having such fine tools to work with!"));
			gaveTools.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveTools.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Toolbox,  new DispositionDependentReaction(gaveTools));
			
			gaveSeaShell.AddAction(new NPCTakeItemAction(toControl));
			gaveSeaShell.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "Some mighty fine sea shells you found!"));
			gaveSeaShell.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveSeaShell.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(gaveSeaShell));
			
			gaveRose.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "Unfortunately that rose wilted before I could trade it....oh well you win some and lose some..."));
			gaveRose.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveRose.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Rose,  new DispositionDependentReaction(gaveRose));
			
			gaveCaptainsLog.AddAction(new NPCTakeItemAction(toControl));
			gaveCaptainsLog.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "The sea capatin has had quite an adventure"));
			gaveCaptainsLog.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveCaptainsLog.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.CaptainLog,  new DispositionDependentReaction(gaveCaptainsLog));
			
			gaveToySword.AddAction(new NPCTakeItemAction(toControl));
			gaveToySword.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "That toy sword has brought back some fond memories"));
			gaveToySword.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveToySword.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.ToySword,  new DispositionDependentReaction(gaveToySword));
			/*
			gavePortrait.AddAction(new NPCTakeItemAction(toControl));
			gavePortrait.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "That portrait you gave me earlier...It's a fake!"));
			gavePortrait.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gavePortrait.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Portrait,  new DispositionDependentReaction(gavePortrait));
			*/
			gaveRope.AddAction(new NPCTakeItemAction(toControl));
			gaveRope.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "All I need now is a hat to go with the rope you gave me!"));
			gaveRope.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveRope.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Rope,  new DispositionDependentReaction(gaveRope));
			
			gaveVegetable.AddAction(new NPCTakeItemAction(toControl));
			gaveVegetable.AddAction(new NPCCallbackSetStringAction(AddTextToList, toControl, "Those families over on the other side of the island really made good use of these vegetales"));
			gaveVegetable.AddAction(new NPCGiveItemAction(toControl,GiveItem)); // gives random item
			gaveVegetable.AddAction(new UpdateCurrentTextAction(toControl, "A pleasure doing business with you my friend!"));
			_allItemReactions.Add(StringsItem.Vegetable,  new DispositionDependentReaction(gaveVegetable));
			#endregion
			
			gavePendant.AddAction(new NPCTakeItemAction(toControl));
			//gavePendant.AddAction(new NPCCallbackAction(SetPendant)); 
			gavePendant.AddAction(new NPCGiveItemAction(toControl,StringsItem.Pendant)); // CHANGE APPLE TO TULIP SEEDS
			gavePendant.AddAction(new UpdateCurrentTextAction(toControl, "Your mother wants some seeds to plant flowers? Well I have just the thing! Tulip's! They are sure to liven her day up!"));
			_allItemReactions.Add(StringsItem.Pendant,  new DispositionDependentReaction(gavePendant));
			
			gaveSunflowerSeed.AddAction(new NPCTakeItemAction(toControl));
			//gaveSunflowerSeed.AddAction(new NPCCallbackAction(SetPendant)); 
			gaveSunflowerSeed.AddAction(new NPCGiveItemAction(toControl,StringsItem.SunflowerSeeds)); // CHANGE APPLE TO TULIP SEEDS
			gaveSunflowerSeed.AddAction(new UpdateCurrentTextAction(toControl, "You don't like sunflower's? Well i have an idea! Have some tulip seeds, much better on the eyes in my opinion!"));
			_allItemReactions.Add(StringsItem.SunflowerSeeds,  new DispositionDependentReaction(gaveSunflowerSeed));
			
			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));
		
		}
		
		public string GiveItem(){
			Debug.Log("Bazaarman trying to give the item: " + inventory.Peek());
			return inventory.Dequeue();
		}
		
		public void AddTextToList(NPC npc, string text){
			stringList[stringCounter] = text;
			stringCounter++;
		}
		
		public void SetupInventory(){
			do{
				switch((int)Random.Range(0,4)){
				case 0: 
					if (!inventory.Contains(StringsItem.Apple)){
						inventory.Enqueue(StringsItem.Apple);
						currentInventory++;
					}
					break;
				case 1: 
					if (!inventory.Contains(StringsItem.FishingRod)){
						inventory.Enqueue(StringsItem.FishingRod);
						currentInventory++;
					}
					break;
				case 2: 
					if (!inventory.Contains(StringsItem.Toolbox)){
						inventory.Enqueue(StringsItem.Toolbox);
						currentInventory++;
					}
					break;
				case 3:
					if (!inventory.Contains(StringsItem.TimeWhale)){
						inventory.Enqueue(StringsItem.TimeWhale);
						currentInventory++;
					}
					break;
				}
			}while (currentInventory < startingInventory);
		}
		
		public void RandomMessage(){
			SetDefaultText(stringList[(int)Random.Range(0,stringCounter)]);
			if (currentInventory == 0) SetupInventory();
		}
		
		public override void UpdateEmotionState(){
		}
		
		public override void PassStringToEmotionState(string text){
			stringList[stringCounter] = text;
			stringCounter++;
		}
		
		
	
	}
	#endregion
	#endregion
}
