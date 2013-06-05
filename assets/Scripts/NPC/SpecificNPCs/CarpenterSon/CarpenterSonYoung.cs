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
	Schedule Woodworking;
	Reaction CreatedFishingRod;
	protected override void SetFlagReactions(){
		
		Reaction ReactToCastleMan = new Reaction();
		ReactToCastleMan.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends , ReactToCastleMan);
		
		Reaction ReactToCastleManNotFriends = new Reaction();
		ReactToCastleManNotFriends.AddAction(new NPCAddScheduleAction(this, TalkWithCastlemanNotFriend));
		flagReactions.Add(FlagStrings.PlayerAndCastleNOTFriends , ReactToCastleManNotFriends);
		
		CreatedFishingRod = new Reaction();
		flagReactions.Add(FlagStrings.carpenterSonMakesFishingRod, CreatedFishingRod);
		//CreatedFishingRod.Add
		
		Reaction ReactToMeetingCSON = new Reaction();
		//ReactToMeetingCSON.AddAction(new NPCCallbackAction(UpdateReactToMeetingCSON));
		flagReactions.Add(FlagStrings.InitialConversationWithCSONNOTFriend, ReactToMeetingCSON);
		
	}
	public void UpdateReactToMeetingCSON(){
		//Debug.Log("This should go away!");
		this.RemoveScheduleWithFlag("CSONMEETCASTLEMAN");	
	}
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey! Can you help me find my tools?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		TalkWithCastleman = new Schedule (this, Schedule.priorityEnum.High);
		TalkWithCastleman.Add(new TimeTask(300, new WaitTillPlayerCloseState(this, ref player)));
		Task setFlag = (new TimeTask(2f, new IdleState(this)));
		setFlag.AddFlagToSet(FlagStrings.InitialConversationWithCSONFriend);
		TalkWithCastleman.Add(setFlag);
		TalkWithCastleman.AddFlagGroup("CSONMEETCASTLEMAN");
		
		
		TalkWithCastlemanNotFriend = new Schedule (this, Schedule.priorityEnum.High);
		TalkWithCastlemanNotFriend.Add(new TimeTask(300, new WaitTillPlayerCloseState(this, ref player)));
		Task setFlagNOT = (new TimeTask(2f, new IdleState(this)));
		setFlagNOT.AddFlagToSet(FlagStrings.InitialConversationWithCSONNOTFriend);
		TalkWithCastlemanNotFriend.Add(setFlagNOT);
		TalkWithCastleman.AddFlagGroup("CSONMEETCASTLEMAN");
		
		
		Woodworking = new Schedule( this, Schedule.priorityEnum.Medium);
		//Woodworking.Add (new TimeTask(300, new WaitState(this)));
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	
	private class InitialEmotionState : EmotionState{
		
		
		Reaction giveFishingRodReaction = new Reaction();
		string gaveFishingRodDialogue = "Oh man, thanks so much!";
		
		Reaction whittlingItem = new Reaction();
		
		Reaction gaveTools = new Reaction();
		
		Choice giveToolsChoice = new Choice("Give Tools.", "Thanks for finding my tools!");
		Reaction giveToolsReaction = new Reaction();
		
		Choice makeFishingRodChoice = new Choice("Make Fishing Rod.", "You know, I always wanted to try fishing");
		Reaction makeFishingRodReaction = new Reaction();
		
		Choice makeSwordChoice = new Choice("Make Sword.", "That sounds cool, I'll get right on it!");
		Reaction makeSwordReaction = new Reaction();
		
		Choice makeDollChoice = new Choice("Make Doll", "That's kind of lame, but it's something");
		Reaction makeDollReaction = new Reaction();
		
		Choice EncourageFishingChoice = new Choice("Try fishing with it", "Huh? Oh yeah, I totally should.");
		Reaction EncourageFishingReaction = new Reaction();
		
		Choice ComplimentWorkChoice = new Choice("Oh man this is so cool", "I know right?");
		Choice CritisizeWorkChoice = new Choice("You could use some work", "Uh, alright. Guess I'll just have to make myself better.");
		Reaction EncourageCarpentryReaction = new Reaction();
		/*
		Choice createToolboxChoice = new Choice ("You Have it.", "You caught me.");
		Reaction createToolboxReaction = new Reaction();
		*/
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			giveFishingRodReaction.AddAction(new NPCTakeItemAction(toControl));
			giveFishingRodReaction.AddAction(new SetOffFlagAction(FlagStrings.gaveFishingRodToCarpenterSon));
			giveFishingRodReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveFishingRodEmotionState(toControl, gaveFishingRodDialogue)));
			_allItemReactions.Add(StringsItem.FishingRod, new DispositionDependentReaction(giveFishingRodReaction));
			
			giveToolsReaction.AddAction(new NPCCallbackAction(GiveToolsToCarpenterSon));
			giveToolsReaction.AddAction(new NPCTakeItemAction(toControl));
			//giveToolsReaction.AddAction(new ShowOneOffChatAction(NPCManager.instance.getNPC(StringsNPC.CarpenterYoung),
			//	"Oh! You founds your tools? " +
			//	"You should get started on something then. You'll need to practice if you want to be a Carpenter one day."));
			
			//EncourageCarpentryReaction.AddAction(SetDefaultText("I'll be sure to work more on my carpentry."));
			EncourageCarpentryReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedCarpentry));
			EncourageCarpentryReaction.AddAction(new NPCCallbackAction(EncouragedCarpentryResult));
			
			//EncourageFishingReaction.AddAction(SetDefaultText("I think I'll go try out fishing tomorrow."));
			EncourageFishingReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedFishing));
			EncourageFishingReaction.AddAction(new NPCCallbackAction(EncouragedFishingResult));

			_allItemReactions.Add(StringsItem.Toolbox,  new DispositionDependentReaction(giveToolsReaction));
			//giveToolsReaction.AddAction
			
			

		}
		
		
		public override void UpdateEmotionState(){
			
		}
		/*private void GaveToolbox() {
			_allChoiceReactions.Remove(createToolboxChoice);
			GUIManager.Instance.RefreshInteraction();
		}*/
		
		private void GiveToolsToCarpenterSon(){
			_allChoiceReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Hey, now that I have my tools back I need to make something. Do you have any suggestions?");
			
			makeFishingRodReaction.AddAction(new NPCCallbackAction(TellToMakeFishingRod));
			makeSwordReaction.AddAction(new NPCCallbackAction(TellToMakeSword));
			makeDollReaction.AddAction(new NPCCallbackAction(TellToMakeDoll));
			
			_allChoiceReactions.Add(makeFishingRodChoice, new DispositionDependentReaction(makeFishingRodReaction));
			_allChoiceReactions.Add(makeSwordChoice, new DispositionDependentReaction(makeSwordReaction));
			_allChoiceReactions.Add(makeDollChoice, new DispositionDependentReaction(makeDollReaction));
			
			//Need carpenter to come back, to start scriptd schedule chat
			
		}
				
		private void TellToMakeFishingRod(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			WhittleItem();
			FlagManager.instance.SetFlag(FlagStrings.carpenterSonMakesFishingRod);
			SetDefaultText("I wonder how it will turn out.");
		}
			
		private void TellToMakeSword(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			WhittleItem();
			SetDefaultText("This is going to be so cool when I'm done.");
		}
		
		private void TellToMakeDoll(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			WhittleItem();
			SetDefaultText("The more I whittle this, the creepier it looks.");
		}
		
		private void WhittleItem(){
			//Whittling Animation for a period of time
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
		}
		
		private void EncouragedCarpentryResult(){
			_allChoiceReactions.Remove(CritisizeWorkChoice);
			_allChoiceReactions.Remove(ComplimentWorkChoice);
			SetDefaultText("Alright, now I just need to work on my carpentry.");
		}
		
		private void EncouragedFishingResult(){
			_allChoiceReactions.Remove(CritisizeWorkChoice);
			_allChoiceReactions.Remove(ComplimentWorkChoice);
			_allChoiceReactions.Remove(EncourageFishingChoice);
			SetDefaultText("I think I'll try to go fishing tomorrow. Thanks!");
		}
		
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
