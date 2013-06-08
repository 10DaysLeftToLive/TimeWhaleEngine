using UnityEngine;
using System.Collections;

/// <summary>
/// SeaCaptainMiddle specific scripting values
/// </summary>
public class SeaCaptainMiddle : NPC {	
	private SeaCaptainTreasureHuntSchedule treasureHuntSched;
	private NPCConvoSchedule talkToFortuneTellerFirstSched;
	private NPCConvoSchedule talkToFortuneTellerSecondSched;
	private Schedule returnToDockSchedOne;
	private Schedule returnToDockSchedTwo;
	Reaction fishingRodStolenReaction = new Reaction();
	Reaction treasureHuntBeginsReaction = new Reaction();
	Reaction talkToFortuenTellerFirstReaction = new Reaction();
	Reaction talkToFortuenTellerSecondReaction = new Reaction();
	private Vector3 startingPos = new Vector3(74f, -3.09f + LevelManager.levelYOffSetFromCenter, 0f);
	NPCConvoSchedule TalktoCarpenterSon;
	Schedule AfterTalkToCarpenterSon;
	protected override void Init() {
		id = NPCIDs.SEA_CAPTAIN;
		base.Init();
		startingPos = this.transform.position;
	}
	
	protected override void SetFlagReactions(){
		#region Fishing rod stolen
		fishingRodStolenReaction.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Don't forget to bring my fishing rod back.")));
		fishingRodStolenReaction.AddAction(new ShowOneOffChatAction(this, "Make sure to bring that back in one piece", 2f));
		flagReactions.Add(FlagStrings.StolenFishingRod, fishingRodStolenReaction);
		#endregion
		
		treasureHuntBeginsReaction.AddAction(new NPCAddScheduleAction(this, treasureHuntSched));
		treasureHuntBeginsReaction.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Alas! All has been for naught. Well, I guess I shall leave with haste as soon as I can go!")));
		flagReactions.Add(FlagStrings.TreasureHuntBegin, treasureHuntBeginsReaction);
		
		talkToFortuenTellerFirstReaction.AddAction(new NPCAddScheduleAction(this, returnToDockSchedOne));
		talkToFortuenTellerFirstReaction.AddAction(new NPCAddScheduleAction(this, talkToFortuneTellerFirstSched));
		AddTimeReaction(1000, talkToFortuenTellerFirstReaction);
		
		talkToFortuenTellerSecondReaction.AddAction(new NPCAddScheduleAction(this, returnToDockSchedTwo));
		talkToFortuenTellerSecondReaction.AddAction(new NPCAddScheduleAction(this, talkToFortuneTellerSecondSched));
		AddTimeReaction(1500, talkToFortuenTellerSecondReaction);
		
		Reaction rebuildShip = new Reaction();
		rebuildShip.AddAction(new NPCAddScheduleAction(this, TalktoCarpenterSon));
		rebuildShip.AddAction(new NPCAddScheduleAction(this, AfterTalkToCarpenterSon));
		flagReactions.Add(FlagStrings.StartConversationWithSeaCaptainAboutBuildingShip, rebuildShip);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Ahoy matey! I am the legendeary sea captain of the six seas, or was it seven..."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		treasureHuntSched = new SeaCaptainTreasureHuntSchedule(this);
		
		talkToFortuneTellerFirstSched = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FortuneTellerMiddle), new MiddleSeaCaptainFortuneTellerFirstConvo(), Schedule.priorityEnum.Medium, true);
		talkToFortuneTellerSecondSched = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FortuneTellerMiddle), new MiddleSeaCaptainFortuneTellerSecondConvo(), Schedule.priorityEnum.Medium, true);
		TalktoCarpenterSon = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonMiddle), new MiddleSeaCaptainToCarpenterSon(), Schedule.priorityEnum.DoConvo, true);
		TalktoCarpenterSon.SetCanNotInteractWithPlayer();
		
		returnToDockSchedOne = new Schedule(this);
		returnToDockSchedOne.Add(new Task(new MoveThenMarkDoneState(this, startingPos)));
		
		returnToDockSchedTwo = new Schedule(this);
		returnToDockSchedTwo.Add(new Task(new MoveThenMarkDoneState(this, startingPos)));
		
		AfterTalkToCarpenterSon = new Schedule(this, Schedule.priorityEnum.High);
		Task FinishedTalking  = new TimeTask(0f, new IdleState(this));
		FinishedTalking.AddFlagToSet(FlagStrings.AfterConversationAboutBuildingShip);
		AfterTalkToCarpenterSon.Add (FinishedTalking);
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
		Reaction seaShellReaction;
		Reaction portraitReaction;
	
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
			shovelReaction.AddAction(new NPCCallbackAction(UpdateTreasureHuntBegins));
			_allItemReactions.Add(StringsItem.Shovel,  new DispositionDependentReaction(shovelReaction));
			
			Reaction toyShipReaction = new Reaction();
			toyShipReaction.AddAction(new NPCTakeItemAction(toControl));
			toyShipReaction.AddAction(new UpdateCurrentTextAction(toControl, "Ahhh... what a grand vessel! Reminds me of me own ship. Thanks laddie!"));
			_allItemReactions.Add(StringsItem.ToyBoat,  new DispositionDependentReaction(toyShipReaction));
			
			Reaction seaShellReaction = new Reaction();
			seaShellReaction.AddAction(new NPCTakeItemAction(toControl));
			seaShellReaction.AddAction(new UpdateCurrentTextAction(toControl, "What is this? There is a pearl inside! My search has not been in vain."));
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(seaShellReaction));
			
			Reaction portraitReaction = new Reaction();
			portraitReaction.AddAction(new NPCTakeItemAction(toControl));
			portraitReaction.AddAction(new UpdateCurrentTextAction(toControl, "I don't know what use I have for a portrait, but I guess I should take it since I found it."));
			_allItemReactions.Add(StringsItem.Portrait,  new DispositionDependentReaction(portraitReaction));
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
		
		public void UpdateTreasureHuntBegins() {
			FlagManager.instance.SetFlag(FlagStrings.TreasureHuntBegin);
		}
		#endregion
	
	}
	#endregion
	#endregion
}
