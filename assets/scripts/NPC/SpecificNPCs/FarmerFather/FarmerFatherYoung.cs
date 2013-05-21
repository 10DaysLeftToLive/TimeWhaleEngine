using UnityEngine;
using System.Collections;

/// <summary>
/// Farmer Father young specific scripting values
///
/// </summary>
public class FarmerFatherYoung : NPC {	
	//Known Bugs, can override two conversations by talking to him.
	//Conversation speed is currently bugged
	protected override void Init() {
		id = NPCIDs.FARMER_FATHER;
		base.Init();
	}
	
	private Schedule BusinessTimerSchedule;
	private NPCConvoSchedule BusinessTalk;
	
	protected override void SetFlagReactions(){
		Reaction NewDialogueReaction = new Reaction();
		NewDialogueReaction.AddAction (new NPCCallbackAction(UpdateConversation));
		flagReactions.Add(FlagStrings.ConversationInMiddleFather, NewDialogueReaction);
		
		Reaction Business = new Reaction();
		Business.AddAction(new NPCAddScheduleAction(this, BusinessTalk));
		Business.AddAction(new NPCCallbackAction(UpdateBusiness));
		flagReactions.Add(FlagStrings.BusinessConversation, Business);
		
		Reaction timer = new Reaction();
		timer.AddAction(new NPCAddScheduleAction(this, BusinessTimerSchedule));
		flagReactions.Add(FlagStrings.BusinessTimer, timer);
	}
	public void UpdateBusiness(){
		currentEmotion.PassStringToEmotionState(FlagStrings.BusinessConversation);
	}
	public void UpdateConversation(){
		currentEmotion.PassStringToEmotionState(FlagStrings.ConversationInMiddleFather);
	}
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "||||PUT IT UP PUT IT UP YO"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		BusinessTimerSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setOffBusinessConvoFlag = (new Task(new MoveThenDoState(this, this.gameObject.transform.position , new MarkTaskDone(this))));
		setOffBusinessConvoFlag.AddFlagToSet(FlagStrings.BusinessConversation);
		BusinessTimerSchedule.Add(setOffBusinessConvoFlag);
		BusinessTimerSchedule.Add(new TimeTask(1, new WaitTillPlayerCloseState(this, player)));
		BusinessTimerSchedule.Add(new TimeTask(1, new IdleState(this)));
		
		//BusinessTalk = new NPCConvoSchedule();
		BusinessTalk = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerMotherYoung), 
			new YoungFarmerFatherToFarmerMotherBusinessTalk(),Schedule.priorityEnum.High);
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		bool startedConversation = false;
		Reaction ToyPuzzleReaction;
		Reaction SeaShellReaction;
		Reaction AppleReaction;
		Reaction ApplePieReaction;
		Reaction CaptainsLogReaction;
		Reaction PortraitReaction;
		Reaction ToySwordReaction;
		
		
		Choice StandUpChoice;
		Reaction StandUpReaction;
		Choice NotHardChoice;
		Reaction NotHardReaction;
		Choice CowardChoice;
		Reaction CowardReaction;
		Choice DoItChoice;
		Reaction DoItReaction;
		Choice OnYourOwnChoice;
		Reaction OnYourOwnReaction;
		
		Choice StoriesSillyChoice;
		Reaction StoriesSillyReaction;
		Choice DoYouMeanChoice;
		Reaction DoYouMeanReaction;
		Choice NoStoriesSillyChoice;
		Reaction NoStoriesSillyReaction;
		Choice WhatMeanChoice;
		Reaction WhatMeanReaction;
		
		Choice AlreadyBraveChoice;
		Reaction AlreadyBraveReaction;
		
		Choice HowsBusinessChoice;
		Reaction HowsBusinessReaction;
		Choice IllDoItForYouChoice;
		Reaction IllDoItForYouReaction;
		Choice WhyNotChoice;
		Reaction WhyNotReaction;
		Choice TheyllUnderstandChoice;
		Reaction TheyllUnderstandReaction;
		Choice IDontHateYouChoice;
		Reaction IDontHateYouReaction;
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Do you have any books on you?  I..I don't want to disobey my wife, but I want my daughter to grow up learning about stories of bravery..."){
			//Code for giving the Portrait
			Reaction PortraitReaction = new Reaction();
			PortraitReaction.AddAction(new NPCTakeItemAction(toControl));
			PortraitReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks a ton this will make a great addition to my study.  Can't let my wife see it though...she'll have me throw it out."));
			_allItemReactions.Add(StringsItem.Portrait,  new DispositionDependentReaction(PortraitReaction));
			
			//Code for giving the seashell
			Reaction SeaShellReaction = new Reaction();
			SeaShellReaction.AddAction(new NPCTakeItemAction(toControl));
			SeaShellReaction.AddAction(new UpdateCurrentTextAction(toControl, "This looks pretty nice.."));
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(SeaShellReaction));
		
			//Code for giving the Toy Puzzle
			Reaction ToyPuzzleReaction = new Reaction();
			ToyPuzzleReaction.AddAction(new NPCTakeItemAction(toControl));
			ToyPuzzleReaction.AddAction(new UpdateCurrentTextAction(toControl, "Heh...this looks like an interesting problem...I'm gonna try and solve it."));
			_allItemReactions.Add(StringsItem.ToyPuzzle,  new DispositionDependentReaction(ToyPuzzleReaction));
			
			//Code for giving the Apple
			Reaction AppleReaction = new Reaction();
			AppleReaction.AddAction(new NPCTakeItemAction(toControl));
			AppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "That tasted great.  You'll have to tell me where you got it some time..."));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(AppleReaction));
			
			//Code for giving the Apple Pie
			Reaction ApplePieReaction = new Reaction();
			ApplePieReaction.AddAction(new NPCTakeItemAction(toControl));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "That tasted great.  You'll have to tell me where you got it some time..."));
			_allItemReactions.Add(StringsItem.ApplePie,  new DispositionDependentReaction(ApplePieReaction));
			
			//Code for giving the Captains Log
			Reaction CaptainsLogReaction = new Reaction();
			CaptainsLogReaction.AddAction(new NPCTakeItemAction(toControl));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks!  I know my daughter will love it!"));
			_allItemReactions.Add(StringsItem.CaptainsLog,  new DispositionDependentReaction(CaptainsLogReaction));
			
			Reaction ToySwordReaction = new Reaction();
			ToySwordReaction.AddAction(new NPCTakeItemAction(toControl));
			ToySwordReaction.AddAction(new UpdateCurrentTextAction(toControl, "I think my daughter will like this a lot!"));
			_allItemReactions.Add(StringsItem.ToySword,  new DispositionDependentReaction(ToySwordReaction));
			
			#region Path1
			StandUpChoice = new Choice("Why don't you stand up?", "I...I can't do that...It's too scary...");
			StandUpReaction = new Reaction();
			StandUpReaction.AddAction(new NPCCallbackAction(UpdateStandUp));
			StandUpReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...I can't do that...It's too scary..."));
			_allChoiceReactions.Add(StandUpChoice, new DispositionDependentReaction(StandUpReaction));
			
			NotHardChoice = new Choice("It's not hard.", "Well...if you try convincing her you'll understand...");
			NotHardReaction = new Reaction();
			NotHardReaction.AddAction(new NPCCallbackAction(UpdateNotHard));
			NotHardReaction.AddAction(new UpdateCurrentTextAction(toControl, "Well...if you try convincing her you'll understand..."));
			
			CowardChoice = new Choice("You are a coward!", "I...yes I am...");
			CowardReaction = new Reaction();
			CowardReaction.AddAction(new NPCCallbackAction(UpdateCoward));
			CowardReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...yes I am..."));
			
			DoItChoice = new Choice("I'll do it!", "Well...*sigh*...you can try...");
			DoItReaction = new Reaction();
			DoItReaction.AddAction(new NPCCallbackAction(UpdateDoIt));
			DoItReaction.AddAction(new UpdateCurrentTextAction(toControl, "Well...*sigh*...you can try..."));
			
			OnYourOwnChoice = new Choice("You're on your own!", "Thanks..for talking with me...");
			OnYourOwnReaction = new Reaction();
			OnYourOwnReaction.AddAction(new NPCCallbackAction(UpdateOnYourOwn));
			OnYourOwnReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks..for talking with me..."));
			#endregion
			
			#region PathTwo
			StoriesSillyChoice = new Choice("Stories are silly.", "You...you're probably right...but I have hope that they could be something great.");
			StoriesSillyReaction = new Reaction();
			StoriesSillyReaction.AddAction(new NPCCallbackAction(UpdateStoriesSilly));
			StoriesSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "You...you're probably right...but I have hope that they could be something great."));
			_allChoiceReactions.Add(StoriesSillyChoice, new DispositionDependentReaction(StoriesSillyReaction));
			
			DoYouMeanChoice = new Choice("What do you mean?", "Well, all my life...I've been scared to stand up to people...Maybe if I tell my daughter about warriors she won't back down like...like me...");
			DoYouMeanReaction = new Reaction();
			DoYouMeanReaction.AddAction(new NPCCallbackAction(UpdateDoYouMean));
			DoYouMeanReaction.AddAction(new UpdateCurrentTextAction(toControl, "Well, all my life...I've been scared to stand up to people...Maybe if I tell my daughter about warriors she won't back down like...like me..."));
			
			NoStoriesSillyChoice = new Choice("No...stories are silly.", "I...well...if you find any stories just bring them to me...");
			NoStoriesSillyReaction = new Reaction();
			NoStoriesSillyReaction.AddAction(new NPCCallbackAction(UpdateNoStoriesSilly));
			NoStoriesSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...well...if you find any stories just bring them to me..."));		
			#endregion
			
			AlreadyBraveChoice = new Choice ("She's already brave", "I...I guess you're right.  Maybe my daughter doesn't need stories, maybe she is already brave...thanks for your help!");
			AlreadyBraveReaction = new Reaction();
			AlreadyBraveReaction.AddAction(new NPCCallbackAction(UpdateAlreadyBrave));
			AlreadyBraveReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...I guess you're right.  Maybe my daughter doesn't need stories, maybe she is already brave...thanks for your help!"));
			
			#region Business Talk
			HowsBusinessChoice = new Choice ("How's business?", "*sigh*  It's not going well...whenever I go to the market I can never find the strength to charge enough.");
			HowsBusinessReaction = new Reaction();
			HowsBusinessReaction.AddAction(new NPCCallbackAction(UpdateHowsBusiness));
			HowsBusinessReaction.AddAction(new UpdateCurrentTextAction(toControl, "*sigh*  It's not going well...whenever I go to the market I can never find the strength to charge enough."));
			
			IllDoItForYouChoice = new Choice ("I'll do it for you!", "Really?  You sure?  Okay...take these to the market and see if you can get replacement shovel!  Ours broke.");
			IllDoItForYouReaction = new Reaction();
			IllDoItForYouReaction.AddAction(new NPCCallbackAction(UpdateIllDoItForYou));
			IllDoItForYouReaction.AddAction(new NPCGiveItemAction(toControl,StringsItem.Vegetable));
			IllDoItForYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "Really?  You sure?  Okay...take these to the market and see if you can get replacement shovel!  Ours broke."));
			
			WhyNotChoice = new Choice ("Why not?", "Because...I...I'm too scared of people hating me if I haggle");
			WhyNotReaction = new Reaction();
			WhyNotReaction.AddAction(new NPCCallbackAction(UpdateWhyNot));
			WhyNotReaction.AddAction(new UpdateCurrentTextAction(toControl, "Because...I...I'm too scared of people hating me if I haggle"));
			
			TheyllUnderstandChoice = new Choice ("They'll understand.", " I...I can't do it...");
			TheyllUnderstandReaction = new Reaction();
			TheyllUnderstandReaction.AddAction(new NPCCallbackAction(UpdateTheyllUnderstand));
			TheyllUnderstandReaction.AddAction(new UpdateCurrentTextAction(toControl, " I...I can't do it..."));
			
			IDontHateYouChoice = new Choice ("I don't hate you.", "Thanks...I don't think it changes anything though..");
			IDontHateYouReaction = new Reaction();
			IDontHateYouReaction.AddAction(new NPCCallbackAction(UpdateIDontHateYou));
			IDontHateYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks...I don't think it changes anything though.."));
			
			
			
			#endregion Business Talk
		}
		
		public void UpdateHowsBusiness(){
			_allChoiceReactions.Remove(HowsBusinessChoice);
			_allChoiceReactions.Add(IllDoItForYouChoice, new DispositionDependentReaction(IllDoItForYouReaction));
			_allChoiceReactions.Add(WhyNotChoice, new DispositionDependentReaction(WhyNotReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateIllDoItForYou(){
			_allChoiceReactions.Remove(IllDoItForYouChoice);
			_allChoiceReactions.Remove(WhyNotChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateWhyNot(){
			_allChoiceReactions.Remove(IllDoItForYouChoice);
			_allChoiceReactions.Remove(WhyNotChoice);
			_allChoiceReactions.Add(TheyllUnderstandChoice, new DispositionDependentReaction(TheyllUnderstandReaction));
			_allChoiceReactions.Add(IDontHateYouChoice, new DispositionDependentReaction(IDontHateYouReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateTheyllUnderstand(){
			_allChoiceReactions.Remove(TheyllUnderstandChoice);
			_allChoiceReactions.Remove(IDontHateYouChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateIDontHateYou(){
			_allChoiceReactions.Remove(TheyllUnderstandChoice);
			_allChoiceReactions.Remove(IDontHateYouChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateConversationOptions(){
				
		}
		public void UpdateAlreadyBrave(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			startedConversation = true;
			SetDefaultText("Thanks for your help!  I'm proud of my daughter");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
		}
		#region UpdatePathOne
		public void UpdateStandUp(){
			//_allChoiceReactions.Remove(StandUpChoice);
			//_allChoiceReactions.Remove(StoriesSillyChoice);
			_allChoiceReactions.Clear();
			startedConversation = true;
			_allChoiceReactions.Add(NotHardChoice, new DispositionDependentReaction(NotHardReaction));
			_allChoiceReactions.Add(CowardChoice, new DispositionDependentReaction(CowardReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultDialogue();
		}
		public void UpdateNotHard(){
			_allChoiceReactions.Remove(NotHardChoice);
			_allChoiceReactions.Remove(CowardChoice);
			_allChoiceReactions.Add(DoItChoice, new DispositionDependentReaction(DoItReaction));
			_allChoiceReactions.Add(OnYourOwnChoice, new DispositionDependentReaction(OnYourOwnReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultDialogue();
		}
		public void UpdateCoward (){
			_allChoiceReactions.Remove(NotHardChoice);
			_allChoiceReactions.Remove(CowardChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'd rather not talk anymore.");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
			
		}
		public void UpdateDoIt(){
			_allChoiceReactions.Remove(DoItChoice);
			_allChoiceReactions.Remove(OnYourOwnChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I doubt you'll get anywhere talking with my wife.");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
		}
		public void UpdateOnYourOwn(){
			_allChoiceReactions.Remove(DoItChoice);
			_allChoiceReactions.Remove(OnYourOwnChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Thanks for what little you could do.");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
		}
		#endregion
		
		#region UpdatePathTwo
		public void UpdateStoriesSilly(){
			_allChoiceReactions.Clear();
			startedConversation = true;
			_allChoiceReactions.Add(DoYouMeanChoice, new DispositionDependentReaction(DoYouMeanReaction));
			_allChoiceReactions.Add(NoStoriesSillyChoice, new DispositionDependentReaction(NoStoriesSillyReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultDialogue();
		}
		public void UpdateDoYouMean(){
			_allChoiceReactions.Remove(DoYouMeanChoice);
			_allChoiceReactions.Add(StandUpChoice, new DispositionDependentReaction(StandUpReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultDialogue();
		}
		public void UpdateNoStoriesSilly(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("If you find a story tell me...");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
		}
		#endregion
		public override void UpdateEmotionState(){
			
		}
		public override void PassStringToEmotionState(string text){
			if(text == FlagStrings.BusinessConversation){
				_allChoiceReactions.Add(HowsBusinessChoice, new DispositionDependentReaction(HowsBusinessReaction));
			}
			if(text == FlagStrings.ConversationInMiddleFather){
				if (startedConversation == false){
				_allChoiceReactions.Add(AlreadyBraveChoice, new DispositionDependentReaction(AlreadyBraveReaction));
				}
			}
		}
	
	}
	#endregion
	#endregion
}
