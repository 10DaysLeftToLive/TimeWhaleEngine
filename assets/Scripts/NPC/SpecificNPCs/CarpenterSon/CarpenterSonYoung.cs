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
	public bool madeFishingRod = false;
	internal static string encourageString = "Hey, now that I have my tools back I need to make something. Do you have any suggestions?";
	public string itemCarpenterMakes = "None";
	Schedule BeginDayWithDad;
	NPCConvoSchedule BeginDayChat;
	protected override void SetFlagReactions(){
		
		Reaction ReactToCastleMan = new Reaction();
		ReactToCastleMan.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends , ReactToCastleMan);
		
		Reaction ReactToCastleManNotFriends = new Reaction();
		ReactToCastleManNotFriends.AddAction(new NPCAddScheduleAction(this, TalkWithCastlemanNotFriend));
		flagReactions.Add(FlagStrings.PlayerAndCastleNOTFriends , ReactToCastleManNotFriends);
		
		CreatedFishingRod = new Reaction();
		//flagReactions.Add(FlagStrings.carpenterSonMakesFishingRod, CreatedFishingRod);
		//CreatedFishingRod.Add
		BeginDayChat = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterYoung), new YoungCarpenterSonToCarpenterConvo());

		BeginDayWithDad = new Schedule(this, Schedule.priorityEnum.DoNow);
  		BeginDayWithDad.Add(new TimeTask(10, new WaitTillPlayerCloseState(this, ref player)));
  		this.AddSchedule(BeginDayWithDad);
		this.AddSchedule(BeginDayChat);
	
	
		Reaction ReactToMeetingCSON = new Reaction();
		//ReactToMeetingCSON.AddAction(new NPCCallbackAction(UpdateReactToMeetingCSON));
		flagReactions.Add(FlagStrings.InitialConversationWithCSONNOTFriend, ReactToMeetingCSON);
		
	}
	public void UpdateReactToMeetingCSON(){
		//Debug.Log("This should go away!");
		this.RemoveScheduleWithFlag("CSONMEETCASTLEMAN");	
	}
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey! Can you help me find my tools? I swear I left them around here somewhere."));
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
		
		//Reaction whittlingItem = new Reaction();
		
		Reaction gaveTools = new Reaction();
		
		Choice giveToolsChoice = new Choice("Give Tools.", "Thanks for finding my tools!");
		Reaction giveToolsReaction = new Reaction();
		
		Choice makeFishingRodChoice = new Choice("Make Fishing Rod.", "You know, I always wanted to try fishing");
		Reaction makeFishingRodReaction = new Reaction();
		
		Choice makeSwordChoice = new Choice("Make Sword.", "That sounds cool, I'll get right on it!");
		Reaction makeSwordReaction = new Reaction();
		
		Choice makeDollChoice = new Choice("Make Doll", "That's kind of lame, but it's something");
		Reaction makeDollReaction = new Reaction();
		
		Choice recieveItemChoice = new Choice("Are you done yet?", "Yep!");
		Reaction recieveItemReaction = new Reaction();
		
		Reaction transferEmotionStateReaction = new Reaction();
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
			recieveItemReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.EncourageEmotionState(toControl, encourageString)));
			
			//EncourageCarpentryReaction.AddAction(SetDefaultText("I'll be sure to work more on my carpentry."));

			_allItemReactions.Add(StringsItem.Toolbox,  new DispositionDependentReaction(giveToolsReaction));
			//giveToolsReaction.AddAction
			transferEmotionStateReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.EncourageEmotionState(toControl, encourageString)));
			

		}
		
		
		public override void UpdateEmotionState(){
			
		}
		/*private void GaveToolbox() {
			_allChoiceReactions.Remove(createToolboxChoice);
			GUIManager.Instance.RefreshInteraction();
		}*/
		
		private void GiveToolsToCarpenterSon(){
			NPC toControl = NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung);
			_allChoiceReactions.Remove(giveToolsChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Hey, now that I have my tools back I need to make something. Do you have any suggestions?");
			
			
			makeFishingRodReaction.AddAction(new NPCCallbackAction(TellToMakeFishingRod));
			makeFishingRodReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.MadeFishingRodEmotionState(toControl, encourageString)));
			makeSwordReaction.AddAction(new NPCCallbackAction(TellToMakeSword));
			makeSwordReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.EncourageEmotionState(toControl, encourageString)));
			makeDollReaction.AddAction(new NPCCallbackAction(TellToMakeDoll));
			makeDollReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.GaveDollEmotionState(toControl, encourageString)));
			
			_allChoiceReactions.Add(makeFishingRodChoice, new DispositionDependentReaction(makeFishingRodReaction));
			_allChoiceReactions.Add(makeSwordChoice, new DispositionDependentReaction(makeSwordReaction));
			_allChoiceReactions.Add(makeDollChoice, new DispositionDependentReaction(makeDollReaction));
			
			//Need carpenter to come back, to start scriptd schedule chat
			
		}
				
		private void TellToMakeFishingRod(){
			((CarpenterSonYoung)_npcInState).madeFishingRod = true;
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allChoiceReactions.Clear();
			SetDefaultText("I wonder how it will turn out.");
			GUIManager.Instance.RefreshInteraction();
			//_allItemReactions.Remove(giveToolsChoice);
//FlagManager.instance.SetFlag(FlagStrings.carpenterSonMakesFishingRod);
			((CarpenterSonYoung)_npcInState).itemCarpenterMakes = "FishingRod";
			WhittleItem();
		}
			
		private void TellToMakeSword(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			SetDefaultText("This is going to be so cool when I'm done.");
			GUIManager.Instance.RefreshInteraction();
			((CarpenterSonYoung)_npcInState).itemCarpenterMakes = "Sword";
			WhittleItem();
		}
		
		private void TellToMakeDoll(){
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			SetDefaultText("The more I whittle this, the creepier it looks.");
			GUIManager.Instance.RefreshInteraction();
			((CarpenterSonYoung)_npcInState).itemCarpenterMakes = "Doll";
			WhittleItem();
		}
		
		private void WhittleItem(){
			//TODO: Whittling Animation
			DebugManager.print("WHITTLING AWW YEEEAAAAAH");
			_allChoiceReactions.Add(recieveItemChoice, new DispositionDependentReaction(recieveItemReaction));
		}
		
		
	}
	#endregion
	#region GivenFishingRod
	private class MadeFishingRodEmotionState : EmotionState{
	
		string gaveFishingRodDialogue = "Oh man, thanks so much!";
		Choice EncourageFishingChoice = new Choice("Try fishing with it", "Huh? Oh yeah, I totally should.");
		Reaction EncourageFishingReaction = new Reaction();
		
		Choice ComplimentWorkChoice = new Choice("Oh man this is so cool", "I know right?");
		Choice CritisizeWorkChoice = new Choice("You could use some work", "Uh, alright. Guess I'll just have to make myself better.");
		Reaction EncourageCarpentryReaction = new Reaction();
		
		Choice RecieveItemChoice = new Choice("Can I see it?", "Do you like it?");
		Reaction RecieveItemReaction = new Reaction();
	
		Reaction giveFishingRodReaction = new Reaction();
		
		public MadeFishingRodEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			giveFishingRodReaction.AddAction(new NPCTakeItemAction(toControl));
			giveFishingRodReaction.AddAction(new SetOffFlagAction(FlagStrings.gaveFishingRodToCarpenterSon));
			giveFishingRodReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveFishingRodEmotionState(toControl, gaveFishingRodDialogue)));
			_allItemReactions.Add(StringsItem.FishingRod, new DispositionDependentReaction(giveFishingRodReaction));
			
			EncourageCarpentryReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedCarpentry));
			EncourageCarpentryReaction.AddAction(new NPCCallbackAction(EncouragedCarpentryResult));
			
			//EncourageFishingReaction.AddAction(SetDefaultText("I think I'll go try out fishing tomorrow."));
			EncourageFishingReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedFishing));
			EncourageFishingReaction.AddAction(new NPCCallbackAction(EncouragedFishingResult));
			//DebugManager.print(itemCarpenterMakes);
			//DebugManager.print(((CarpenterSonYoung)_npcInState).madeFishingRod);
			RecieveItemReaction.AddAction(new NPCCallbackAction(RecieveItemResult));
			RecieveItemReaction.AddAction(new NPCGiveItemAction(toControl, StringsItem.FishingRod));
			_allChoiceReactions.Add(RecieveItemChoice, new DispositionDependentReaction(RecieveItemReaction));
			//DebugManager.print("Inside Emotion State Encourage");
			
			//_allChoiceReactions.Clear();
			//_allChoiceReactions.Add(, new DispositionDependentReaction(makeFishingRodReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
		
		private void RecieveItemResult(){
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(EncourageFishingChoice, new DispositionDependentReaction(EncourageFishingReaction));
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("What do you think?");
		}
		
		private void EncouragedCarpentryResult(){
			SetDefaultText("Alright, now I just need to work on my carpentry.");
			_allChoiceReactions.Clear();
			DebugManager.print("Inside Carpentry Result");
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void EncouragedFishingResult(){
			SetDefaultText("I think I'll try it sometime. Thanks!");
			_allChoiceReactions.Clear();
			DebugManager.print("Inside Fishing Result");
			GUIManager.Instance.RefreshInteraction();
		}
	}
	
	#endregion
	#region Carpenter Son Gains Fishing Rod
	private class GaveFishingRodEmotionState : EmotionState{
		
		public GaveFishingRodEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		}
		
		public override void UpdateEmotionState(){
			
		}
	}
	#endregion
	#region GivenDoll
	private class GaveDollEmotionState : EmotionState{
		
		Choice ComplimentWorkChoice = new Choice("At least it looks nice", "Yeah, I guess");
		Choice CritisizeWorkChoice = new Choice("Did you use wood?", "Yeah I did, but, it just kind of turned out like this.");
		Reaction EncourageCarpentryReaction = new Reaction();
		
		Choice RecieveItemChoice = new Choice("Is it ready?", "Uh yeah, kinda. I'm not sure how I did that.");
		Reaction RecieveItemReaction = new Reaction();
		
		public GaveDollEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			EncourageCarpentryReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedCarpentry));
			EncourageCarpentryReaction.AddAction(new NPCCallbackAction(EncouragedCarpentryResult));
			
			RecieveItemReaction.AddAction(new NPCCallbackAction(RecieveItemResult));
			RecieveItemReaction.AddAction(new NPCGiveItemAction(toControl, StringsItem.TimeWhale));
			_allChoiceReactions.Add(RecieveItemChoice, new DispositionDependentReaction(RecieveItemReaction));
		}
		public override void UpdateEmotionState(){
			
		}
		
		private void RecieveItemResult(){
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
		}
				
		private void EncouragedCarpentryResult(){
			SetDefaultText("I'll make sure the next thing I make is made of wood.");
			GUIManager.Instance.RefreshInteraction();
			_allChoiceReactions.Clear();
		}
	}
	#endregion
	#region EncourageEmotionState
	private class EncourageEmotionState : EmotionState{
				
		Choice ComplimentWorkChoice = new Choice("Oh man this is so cool", "I know right?");
		Choice CritisizeWorkChoice = new Choice("You could use some work", "Uh, alright. Guess I'll just have to make myself better.");
		Reaction EncourageCarpentryReaction = new Reaction();
		
		Choice RecieveItemChoice = new Choice("Can I see it?", "Do you like it?");
		Reaction RecieveItemReaction = new Reaction();
	
		
		public EncourageEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			EncourageCarpentryReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedCarpentry));
			EncourageCarpentryReaction.AddAction(new NPCCallbackAction(EncouragedCarpentryResult));
			
			//EncourageFishingReaction.AddAction(SetDefaultText("I think I'll go try out fishing tomorrow."));
			//DebugManager.print(itemCarpenterMakes);
			//DebugManager.print(((CarpenterSonYoung)_npcInState).madeFishingRod);
			RecieveItemReaction.AddAction(new NPCCallbackAction(RecieveItemResult));
			_allChoiceReactions.Add(RecieveItemChoice, new DispositionDependentReaction(RecieveItemReaction));
			//DebugManager.print("Inside Emotion State Encourage");
			
			//_allChoiceReactions.Clear();
			//_allChoiceReactions.Add(, new DispositionDependentReaction(makeFishingRodReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
		
		private void RecieveItemResult(){
			_allChoiceReactions.Clear();
			Action giveSwordAction = new NPCGiveItemAction (NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), StringsItem.ToySword);
			Action giveDollAction = new NPCGiveItemAction (NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), StringsItem.TimeWhale);
			if (((CarpenterSonYoung)_npcInState).itemCarpenterMakes == "Sword") giveSwordAction.Perform();
			if (((CarpenterSonYoung)_npcInState).itemCarpenterMakes == "Doll") giveDollAction.Perform();
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("What do you think?");
		}
		
		private void EncouragedCarpentryResult(){
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Alright, now I just need to work on my carpentry.");
			_allChoiceReactions.Clear();
			DebugManager.print("Inside Carpentry Result");
		}
		
	}
	#endregion
	#endregion
}
