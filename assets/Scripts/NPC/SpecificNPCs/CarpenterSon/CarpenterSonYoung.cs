using UnityEngine;
using System.Collections;

/// <summary>
/// Carpenter Son young specific scripting values
/// </summary>
public class CarpenterSonYoung : NPC {
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		base.Init();
	}
	Schedule TalkWithCastleman;
	Schedule TalkWithCastlemanNotFriend;
	protected override void SetFlagReactions(){
		
		Reaction ReactToCastleMan = new Reaction();
		ReactToCastleMan.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends , ReactToCastleMan);
		
		Reaction ReactToCastleManNotFriends = new Reaction();
		ReactToCastleManNotFriends.AddAction(new NPCAddScheduleAction(this, TalkWithCastlemanNotFriend));
		flagReactions.Add(FlagStrings.PlayerAndCastleNOTFriends , ReactToCastleManNotFriends);
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "I know my dad wants me to learn carpentry, but I think it's boring. Don't tell him this, but I want to try and be a fisherman. If only I had a fishing rod."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		TalkWithCastleman = new Schedule (this, Schedule.priorityEnum.High);
		TalkWithCastleman.Add(new TimeTask(300, new WaitTillPlayerCloseState(this, player)));
		Task setFlag = (new Task(new MoveThenDoState(this, this.gameObject.transform.position, new MarkTaskDone(this))));
		setFlag.AddFlagToSet(FlagStrings.InitialConversationWithCSONFriend);
		TalkWithCastleman.Add(setFlag);
		//TalkWithCastleman.AddFlagGroup("Talk with Castleman");
		
		
		TalkWithCastlemanNotFriend = new Schedule (this, Schedule.priorityEnum.High);
		TalkWithCastlemanNotFriend.Add(new TimeTask(300, new WaitTillPlayerCloseState(this, player)));
		Task setFlagNOT = (new Task(new MoveThenDoState(this, this.gameObject.transform.position, new MarkTaskDone(this))));
		setFlagNOT.AddFlagToSet(FlagStrings.InitialConversationWithCSONNOTFriend);
		TalkWithCastlemanNotFriend.Add(setFlagNOT);
		//TalkWithCastleman.AddFlagGroup("Talk with Castleman NOT Friends");
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	
	private class InitialEmotionState : EmotionState{
	
		Reaction giveFishingRodReaction = new Reaction();
		string gaveFishingRodDialogue = "Oh man, thanks so much!";
		/*
		Choice giveToolsChoice = new Choice("Give Tools.", "|||| Thanks for helpping me out broseidon!");
		Reaction giveToolsReaction = new Reaction();
		
		Choice makeFishingRodChoice = new Choice("Make Fishing Rod.", "|||| Chyeah, I'm going for it G Skillet. Fish-E-I-AH");
		Reaction makeFishingRodReaction = new Reaction();
		
		Choice makeTreeHouseChoice = new Choice("Make TreeHouse.", "|||| Chu be right broski, I maka ze treehouz");
		Reaction makeTreeHouseReaction = new Reaction();
	
		Choice createToolboxChoice = new Choice ("You Have it.", "You caught me.");
		Reaction createToolboxReaction = new Reaction();
		*/
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			giveFishingRodReaction.AddAction(new NPCTakeItemAction(toControl));
			giveFishingRodReaction.AddAction(new SetOffFlagAction(FlagStrings.gaveFishingRodToCarpenterSon));
			giveFishingRodReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveFishingRodEmotionState(toControl, gaveFishingRodDialogue)));
			_allItemReactions.Add(StringsItem.FishingRod, new DispositionDependentReaction(giveFishingRodReaction));
			/*giveToolsReaction.AddAction(new NPCCallbackAction(GiveToolsToCarpenterSon));
			giveToolsReaction.AddAction(new ShowOneOffChatAction(NPCManager.instance.getNPC(StringsNPC.CarpenterYoung), 
				"Oh good you found my old tools! " +
				"Now if  you are to actually start becoming a great carpenter like my father and his before him then you need to start practicing on your own. " +
				"Why don't you start with a treehouse?"));	
			
			giveToolsReaction.AddAction(new NPCGiveItemAction(NPCManager.instance.getNPC(StringsNPC.CarpenterYoung),"apple"));
			
			_allChoiceReactions.Add(giveToolsChoice,new DispositionDependentReaction(giveToolsReaction));
			//TEMP
			createToolboxReaction.AddAction(new NPCGiveItemAction(toControl,"toolbox"));
			createToolboxReaction.AddAction(new NPCCallbackAction(GaveToolbox));
			_allChoiceReactions.Add (createToolboxChoice, new DispositionDependentReaction(createToolboxReaction));
		*/
		}
		
		
		public override void UpdateEmotionState(){
			
		}
	/*
		private void GaveToolbox() {
			_allChoiceReactions.Remove(createToolboxChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void GiveToolsToCarpenterSon(){
			_allChoiceReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Man I wish I could build a fishing rod. Everyone in the town has one...but I'm not good enough yet.");
			
			makeFishingRodReaction.AddAction(new NPCCallbackAction(TellToMakeFishingRod));
			makeTreeHouseReaction.AddAction(new NPCCallbackAction(TellToMakeTreeHouse));
			
			_allChoiceReactions.Add(makeFishingRodChoice, new DispositionDependentReaction(makeFishingRodReaction));
			_allChoiceReactions.Add(makeTreeHouseChoice, new DispositionDependentReaction(makeTreeHouseReaction));
			
			//Need carpenter to come back, to start scriptd schedule chat
			
		}
				
		private void TellToMakeFishingRod(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove (makeTreeHouseChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("|||| MO FISH MO PROBLEMS");
		}
			
		private void TellToMakeTreeHouse(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove (makeTreeHouseChoice);	
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("|||| MO TREEHOUZ MO PROBLEMS");
		}*/
		
	}
	#endregion
	#region GivenFishingRod
	private class GaveFishingRodEmotionState : EmotionState{
	
		Reaction giveFishingRodReaction;
		public GaveFishingRodEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){

		}
		
		public override void UpdateEmotionState(){
			
		}
	}
	#endregion
	#endregion
}
