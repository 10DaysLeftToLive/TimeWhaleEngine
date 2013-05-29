using UnityEngine;
using System.Collections;

/// <summary>
/// SeaCaptainMiddle specific scripting values
/// </summary>
public class SeaCaptainMiddle : NPC {	
	Vector3 startingPosition, farmDigPos, reflectDigPos, carpenterDigPos, beachDigPos;
	Schedule treasureHuntSched;
	
	protected override void Init() {
		id = NPCIDs.SEA_CAPTAIN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		#region Fishing rod stolen
		Reaction fishingRodStolen = new Reaction();
		fishingRodStolen.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Don't forget to bring my fishing rod back.")));
		fishingRodStolen.AddAction(new ShowOneOffChatAction(this, "Make sure to bring that back in one piece", 2f));
		flagReactions.Add(FlagStrings.StolenFishingRod, fishingRodStolen);
		#endregion
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Ahoy laddie! I am the legendeary sea captain of the six seas, or was it 7..."));
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
		Choice whyHereChoice;
		Reaction whyHereReaction;
		Choice whereShipChoice;
		Reaction whereShipReaction;
		
		Reaction appleReaction;
		Reaction applePieReaction;
		Reaction veggiesReaction;
		Reaction shovelReaction;
		Reaction toyShipReaction;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			#region item reactions
			Reaction appleReaction = new Reaction();
			appleReaction.AddAction(new NPCTakeItemAction(toControl));
			appleReaction.AddAction(new UpdateCurrentTextAction(toControl, "This reminds me of when I stole an apple from the royal gardens. I barely escaped with my life!"));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(appleReaction));
			
			Reaction applePieReaction = new Reaction();
			applePieReaction.AddAction(new NPCTakeItemAction(toControl));
			applePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "I haven't eaten applie pie this good, since I impersonated a noble to eat at the Sultan's palace!"));
			_allItemReactions.Add(StringsItem.ApplePie,  new DispositionDependentReaction(applePieReaction));
			
			Reaction veggiesReaction = new Reaction();
			veggiesReaction.AddAction(new NPCTakeItemAction(toControl));
			veggiesReaction.AddAction(new UpdateCurrentTextAction(toControl, "I remember eating a strange root when I was stranded on a deserted island. It made me think that I could drink sea water."));
			_allItemReactions.Add(StringsItem.Vegetable,  new DispositionDependentReaction(veggiesReaction));
			
			Reaction shovelReaction = new Reaction();
			shovelReaction.AddAction(new NPCTakeItemAction(toControl));
			shovelReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks matey! I can now go and dig for treasure."));
			shovelReaction.AddAction(new NPCAddScheduleAction(_npcInState, new SeaCaptainTreasureHuntSchedule(_npcInState)));
			_allItemReactions.Add(StringsItem.Shovel,  new DispositionDependentReaction(shovelReaction));
			
			Reaction toyShipReaction = new Reaction();
			toyShipReaction.AddAction(new NPCTakeItemAction(toControl));
			toyShipReaction.AddAction(new UpdateCurrentTextAction(toControl, "Ahhh... what a grand vessel! Reminds me of me own ship. Thanks laddie!"));
			_allItemReactions.Add(StringsItem.ToyBoat,  new DispositionDependentReaction(toyShipReaction));
			#endregion
			
			#region choices
			whyHereChoice = new Choice("Why are you here?", "I am searching for great treasure said to be buried on this island, but I can not figure out what the map is saying.");
			whyHereReaction = new Reaction();
			whyHereReaction.AddAction(new NPCCallbackAction(UpdateWhyHere));
			whyHereReaction.AddAction(new UpdateCurrentTextAction(toControl, "How 'ave ye been matey?"));
			_allChoiceReactions.Add(whyHereChoice, new DispositionDependentReaction(whyHereReaction));
			
			whereShipChoice = new Choice("Where is your ship?", "Well... I might have forgot to drop the anchor before commin' ashore.");
			whereShipReaction = new Reaction();
			whereShipReaction.AddAction(new NPCCallbackAction(UpdateWhereShip));
			whereShipReaction.AddAction(new UpdateCurrentTextAction(toControl, "How 'ave ye been laddie?"));
			_allChoiceReactions.Add(whereShipChoice, new DispositionDependentReaction(whereShipReaction));
			#endregion
			
		}
		
		public override void UpdateEmotionState(){

		}
		
		#region update methods
		public void UpdateWhyHere(){
			_allChoiceReactions.Remove(whyHereChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateWhereShip(){
			_allChoiceReactions.Remove(whereShipChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		#endregion
	
	}
	#endregion
	#endregion
}
