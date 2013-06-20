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
	public static Schedule whittleSchedule;
	public static Schedule whittleDoneSchedule;
	public bool madeFishingRod = false;
	internal static string encourageString = "...and there. Done.";
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
		BeginDayChat.SetCanInteract(false);
		BeginDayWithDad = new Schedule(this, Schedule.priorityEnum.DoNow);
		TimeTask BeginDayTimeTask = new TimeTask(10, new WaitTillPlayerCloseState(this, ref player));
  		BeginDayTimeTask.AddFlagToSet(FlagStrings.carpenterSonYoungConvoWithDadFinished);
		BeginDayWithDad.Add(BeginDayTimeTask);
  		this.AddSchedule(BeginDayWithDad);
		this.AddSchedule(BeginDayChat);
	
	
		Reaction ReactToMeetingCSON = new Reaction();
		//ReactToMeetingCSON.AddAction(new NPCCallbackAction(UpdateReactToMeetingCSON));
		flagReactions.Add(FlagStrings.InitialConversationWithCSONNOTFriend, ReactToMeetingCSON);
		
		Reaction CarpenterSonCompleteWhittle = new Reaction();
	}
	
	public void UpdateReactToMeetingCSON(){
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
		Task whittleTask = new TimeTask(30, new AbstractAnimationState(this, "Whittle"));
		whittleTask.AddFlagToSet(FlagStrings.carpenterSonYoungCompletedWhittling);
			
		whittleSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		whittleSchedule.SetCanInteract(false);
		whittleSchedule.Add(whittleTask);
		
		Task whittleDoneTask = new Task(new IdleState(this));
		whittleDoneSchedule = new Schedule(this, Schedule.priorityEnum.Medium);
		whittleDoneSchedule.SetCanInteract(true);
		whittleDoneSchedule.Add(whittleDoneTask);
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
		
		Choice makeDollChoice = new Choice("Make Doll.", "That's kind of lame, but it's something.");
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
			giveFishingRodReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedFishing));
			giveFishingRodReaction.AddAction(new NPCEmotionUpdateAction(toControl, new GaveFishingRodEmotionState(toControl, gaveFishingRodDialogue)));
			_allItemReactions.Add(StringsItem.FishingRod, new DispositionDependentReaction(giveFishingRodReaction));
			
			giveToolsReaction.AddAction(new NPCCallbackAction(GiveToolsToCarpenterSon));
			giveToolsReaction.AddAction(new NPCTakeItemAction(toControl));
			giveToolsReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonYoungGottenTools));
			recieveItemReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.EncourageEmotionState(toControl, encourageString)));
			
			_allItemReactions.Add(StringsItem.Toolbox,  new DispositionDependentReaction(giveToolsReaction));
			transferEmotionStateReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.EncourageEmotionState(toControl, encourageString)));
		}
		
		private void GiveToolsToCarpenterSon(){
			string completeRodText = "...and there. Done.";
			string completeSwordText = "I could sharpen it but I think my dad would get mad.";
			string completeDollText = "Wow, I have no idea how that happened.";
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			NPC toControl = NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung);
			_allChoiceReactions.Remove(giveToolsChoice);
			SetDefaultText("Hey, now that I have my tools back I need to make something. Do you have any suggestions?");
			GUIManager.Instance.RefreshInteraction();
			
			makeFishingRodReaction.AddAction(new NPCCallbackAction(TellToMakeFishingRod));
			makeFishingRodReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.MadeFishingRodEmotionState(toControl, completeRodText)));
			makeFishingRodReaction.AddAction(new ShowOneOffChatAction(toControl, "Come back later, I'll be working on this for a while."));
			makeFishingRodReaction.AddAction(new NPCAddScheduleAction(toControl, whittleSchedule));
			makeFishingRodReaction.AddAction(new NPCAddScheduleAction(toControl, whittleDoneSchedule));
			
			makeSwordReaction.AddAction(new NPCCallbackAction(TellToMakeSword));
			makeSwordReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.EncourageEmotionState(toControl, completeSwordText)));
			makeSwordReaction.AddAction(new ShowOneOffChatAction(toControl, "This is going to take some time, the wait will be worth it though."));
			makeSwordReaction.AddAction(new NPCAddScheduleAction(toControl, whittleSchedule));
			makeSwordReaction.AddAction(new NPCAddScheduleAction(toControl, whittleDoneSchedule));
			
			makeDollReaction.AddAction(new NPCCallbackAction(TellToMakeDoll));
			makeDollReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterSonYoung.GaveDollEmotionState(toControl, completeDollText)));
			makeDollReaction.AddAction(new ShowOneOffChatAction(toControl, "I got a bad feeling about this, but swing by later to come see it."));
			makeDollReaction.AddAction(new NPCAddScheduleAction(toControl, whittleSchedule));
			makeDollReaction.AddAction(new NPCAddScheduleAction(toControl, whittleDoneSchedule));
			
			_allChoiceReactions.Add(makeFishingRodChoice, new DispositionDependentReaction(makeFishingRodReaction));
			_allChoiceReactions.Add(makeSwordChoice, new DispositionDependentReaction(makeSwordReaction));
			_allChoiceReactions.Add(makeDollChoice, new DispositionDependentReaction(makeDollReaction));
			
			//Need carpenter to come back, to start scriptd schedule chat
			
		}
				
		private void TellToMakeFishingRod(){
			_npcInState.SetCharacterPortrait(StringsNPC.Smile);
            _npcInState.ChangeFacialExpression(StringsNPC.Smile);
			((CarpenterSonYoung)_npcInState).madeFishingRod = true;
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			SetDefaultText("I wonder how it will turn out.");
			CompleteCommand();
		}
			
		private void TellToMakeSword(){
			_npcInState.SetCharacterPortrait(StringsNPC.Smile);
            _npcInState.ChangeFacialExpression(StringsNPC.Smile);
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			SetDefaultText("This is going to be so cool when I'm done.");
			CompleteCommand();
		}
		
		private void TellToMakeDoll(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(makeFishingRodChoice);
			_allChoiceReactions.Remove(makeSwordChoice);
			_allChoiceReactions.Remove(makeDollChoice);
			//_allItemReactions.Remove(giveToolsChoice);
			SetDefaultText("The more I whittle this... the creepier it looks.");
			CompleteCommand();
		}
		
		private void CompleteCommand(){
			GUIManager.Instance.RefreshInteraction();
			GUIManager.Instance.CloseInteractionMenu();
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
			giveFishingRodReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedFishing));
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
			_npcInState.SetCharacterPortrait(StringsNPC.Smile);
            _npcInState.ChangeFacialExpression(StringsNPC.Smile);
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(EncourageFishingChoice, new DispositionDependentReaction(EncourageFishingReaction));
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			SetDefaultText("What do you think?");
			GUIManager.Instance.RefreshInteraction();
			_npcInState.PlayAnimation(Strings.animation_stand);
		}
		
		private void EncouragedCarpentryResult(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			SetDefaultText("Alright, now I just need to work on my carpentry.");
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void EncouragedFishingResult(){
			SetDefaultText("I think I'll try it sometime. Thanks!");
			_allChoiceReactions.Clear();
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
		Choice ComplimentWorkChoice = new Choice("At least it looks nice", "Yeah, I guess.");
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
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Clear();
			_npcInState.PlayAnimation(Strings.animation_stand);
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			GUIManager.Instance.RefreshInteraction();

		}
				
		private void EncouragedCarpentryResult(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_npcInState.ChangeFacialExpression(StringsNPC.Default);
			SetDefaultText("I'll make sure the next thing I make is made of wood.");
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
	}
	#endregion
	#region EncourageEmotionState
	private class EncourageEmotionState : EmotionState{
		Choice ComplimentWorkChoice = new Choice("Oh man this is so cool", "I know right?");
		Choice CritisizeWorkChoice = new Choice("You could use some work", "Uh, alright. Guess I'll just have to make myself better.");
		Reaction EncourageCarpentryReaction = new Reaction();
		
		Choice CritisizeNameChoice = new Choice("That name is kind of lame", "Well I think it's cool.");
		Reaction CritisizeNameReaction = new Reaction();
		
		Choice RecieveItemChoice = new Choice("Can I see it?", "Check it out. Everyone will now know you as \"Swordwielder\".");
		Reaction RecieveItemReaction = new Reaction();
	
		public EncourageEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			EncourageCarpentryReaction.AddAction(new SetOffFlagAction(FlagStrings.carpenterSonEncouragedCarpentry));
			EncourageCarpentryReaction.AddAction(new NPCCallbackAction(EncouragedCarpentryResult));
			
			RecieveItemReaction.AddAction(new NPCCallbackAction(RecieveItemResult));
			RecieveItemReaction.AddAction(new NPCGiveItemAction(toControl, StringsItem.ToySword));
			_allChoiceReactions.Add(RecieveItemChoice, new DispositionDependentReaction(RecieveItemReaction));
		}
		
		private void RecieveItemResult(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allChoiceReactions.Clear();
			_npcInState.PlayAnimation(Strings.animation_stand);
			CritisizeNameReaction.AddAction(new NPCCallbackAction(CritisizeNameResult));
			Action giveSwordAction = new NPCGiveItemAction (NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), StringsItem.ToySword);
			Action giveDollAction = new NPCGiveItemAction (NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), StringsItem.TimeWhale);
			_allChoiceReactions.Add(ComplimentWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeWorkChoice, new DispositionDependentReaction(EncourageCarpentryReaction));
			_allChoiceReactions.Add(CritisizeNameChoice, new DispositionDependentReaction(CritisizeNameReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("What do you think?");
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void EncouragedCarpentryResult(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_npcInState.ChangeFacialExpression(StringsNPC.Default);
			SetDefaultText("Alright, now I just need to work on my carpentry.");
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void CritisizeNameResult(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_npcInState.ChangeFacialExpression(StringsNPC.Angry);
			SetDefaultText("\"Swordwielder\" is an awesome name.");
			_allChoiceReactions.Remove(CritisizeNameChoice);
			GUIManager.Instance.RefreshInteraction();
		}
	}
	#endregion
	#endregion
}
