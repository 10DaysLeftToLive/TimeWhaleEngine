using UnityEngine;
using System.Collections;

/// <summary>
/// Farmer Father young specific scripting values
///
/// </summary>
public class FarmerFatherYoung : NPC {	
	//Known Bugs, can override two conversations by talking to him.
	//Conversation speed is currently bugged
	
	Schedule beALazyFarmer;
	
	protected void setAngry(){
		this.SetCharacterPortrait(StringsNPC.Angry);
	}
	protected void setSad(){
		this.SetCharacterPortrait(StringsNPC.Sad);
	}
	protected void setBlink(){
		this.SetCharacterPortrait(StringsNPC.Blink);
	}
	protected void setDefault(){
		this.SetCharacterPortrait(StringsNPC.Default);
	}
	protected void setHappy(){
		this.SetCharacterPortrait(StringsNPC.Smile);	
	}
	protected override void SetFlagReactions(){
		Reaction NewDialogueReaction = new Reaction();
		NewDialogueReaction.AddAction (new NPCCallbackAction(UpdateConversation));
		flagReactions.Add(FlagStrings.ConversationInMiddleFather, NewDialogueReaction);
		
		Reaction FarmerBecomesLazy = new Reaction();
		FarmerBecomesLazy.AddAction(new NPCAddScheduleAction(this, beALazyFarmer));
		flagReactions.Add(FlagStrings.FarmAfterDialogue, FarmerBecomesLazy);
		
	}
	public void UpdateBusiness(){
		currentEmotion.PassStringToEmotionState(FlagStrings.BusinessConversation);
	}
	public void UpdateConversation(){
		currentEmotion.PassStringToEmotionState(FlagStrings.ConversationInMiddleFather);
	}
	protected override EmotionState GetInitEmotionState(){
		return (new StoryEmotionState(this, "We need help running the farm!"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		beALazyFarmer = new Schedule(this);
		beALazyFarmer.Add(new Task(new AbstractAnimationState(this, "Shovel Idle")));
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
	}
	private class StoryEmotionState : EmotionState{
		bool startedConversation = false;
		Reaction GiveSeedsReaction;
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
		
		
		public StoryEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Do you have any books on you? I..I don't want to disobey my wife, but I want my daughter to grow up learning about stories of bravery..."){
			GiveSeedsReaction = new Reaction();
			GiveSeedsReaction.AddAction(new NPCCallbackAction(UpdateGiveSeeds));
			GiveSeedsReaction.AddAction(new NPCTakeItemAction(toControl));
			GiveSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks for the seeds! I thought we were in real trouble when I forgot to buy them."));
			_allItemReactions.Add(StringsItem.SunflowerSeeds, new DispositionDependentReaction(GiveSeedsReaction));
			
			//Code for giving the Portrait
			Reaction PortraitReaction = new Reaction();
			PortraitReaction.AddAction(new NPCTakeItemAction(toControl));
			PortraitReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks a ton this will make a great addition to my study. Can't let my wife see it though...she'll have me throw it out."));
			//_allItemReactions.Add(StringsItem.Portrait,  new DispositionDependentReaction(PortraitReaction));
			
			//Code for giving the seashell
			Reaction SeaShellReaction = new Reaction();
			SeaShellReaction.AddAction(new NPCTakeItemAction(toControl));
			SeaShellReaction.AddAction(new UpdateCurrentTextAction(toControl, "This looks pretty nice."));
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(SeaShellReaction));
		
			//Code for giving the Toy Puzzle
			Reaction ToyPuzzleReaction = new Reaction();
			ToyPuzzleReaction.AddAction(new NPCTakeItemAction(toControl));
			ToyPuzzleReaction.AddAction(new UpdateCurrentTextAction(toControl, "Heh... this looks like an interesting problem... I'm gonna try and solve it."));
			_allItemReactions.Add(StringsItem.ToyPuzzle,  new DispositionDependentReaction(ToyPuzzleReaction));
			
			//Code for giving the Apple
			Reaction AppleReaction = new Reaction();
			AppleReaction.AddAction(new NPCTakeItemAction(toControl));
			AppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "That tasted great. You'll have to tell me where you got it some time..."));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(AppleReaction));
			
			//Code for giving the Apple Pie
			Reaction ApplePieReaction = new Reaction();
			ApplePieReaction.AddAction(new NPCTakeItemAction(toControl));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "That tasted great. You'll have to tell me where you got it some time..."));
			_allItemReactions.Add(StringsItem.ApplePie,  new DispositionDependentReaction(ApplePieReaction));
			
			//Code for giving the Captains Log
			Reaction CaptainsLogReaction = new Reaction();
			CaptainsLogReaction.AddAction(new NPCTakeItemAction(toControl));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks! I know my daughter will love it!"));
			_allItemReactions.Add(StringsItem.CaptainLog,  new DispositionDependentReaction(CaptainsLogReaction));
			
			Reaction ToySwordReaction = new Reaction();
			ToySwordReaction.AddAction(new NPCTakeItemAction(toControl));
			ToySwordReaction.AddAction(new UpdateCurrentTextAction(toControl, "I think my daughter will like this a lot!"));
			_allItemReactions.Add(StringsItem.ToySword,  new DispositionDependentReaction(ToySwordReaction));
			
			#region Path1
			StandUpChoice = new Choice("Why don't you stand up?", "I... I can't do that... It's too scary...");
			StandUpReaction = new Reaction();
			StandUpReaction.AddAction(new NPCCallbackAction(UpdateStandUp));
			StandUpReaction.AddAction(new UpdateCurrentTextAction(toControl, "I... I can't do that... It's too scary..."));
			_allChoiceReactions.Add(StandUpChoice, new DispositionDependentReaction(StandUpReaction));
			
			NotHardChoice = new Choice("It's not hard.", "Well... if you try convincing her you'll understand...");
			NotHardReaction = new Reaction();
			NotHardReaction.AddAction(new NPCCallbackAction(UpdateNotHard));
			NotHardReaction.AddAction(new UpdateCurrentTextAction(toControl, "Well... if you try convincing her you'll understand..."));
			
			CowardChoice = new Choice("You are a coward!", "I... yes I am...");
			CowardReaction = new Reaction();
			CowardReaction.AddAction(new NPCCallbackAction(UpdateCoward));
			CowardReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...yes I am..."));
			
			DoItChoice = new Choice("I'll do it!", "Well... *sigh*... you can try...");
			DoItReaction = new Reaction();
			DoItReaction.AddAction(new NPCCallbackAction(UpdateDoIt));
			DoItReaction.AddAction(new UpdateCurrentTextAction(toControl, "Well... *sigh*... you can try..."));
			
			OnYourOwnChoice = new Choice("You're on your own!", "Thanks... for talking with me...");
			OnYourOwnReaction = new Reaction();
			OnYourOwnReaction.AddAction(new NPCCallbackAction(UpdateOnYourOwn));
			OnYourOwnReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks... for talking with me..."));
			#endregion
			
			#region PathTwo
			StoriesSillyChoice = new Choice("Stories are silly.", "You... you're probably right... but I have hope that they could be something great.");
			StoriesSillyReaction = new Reaction();
			StoriesSillyReaction.AddAction(new NPCCallbackAction(UpdateStoriesSilly));
			StoriesSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "You... you're probably right... but I have hope that they could be something great."));
			_allChoiceReactions.Add(StoriesSillyChoice, new DispositionDependentReaction(StoriesSillyReaction));
			
			DoYouMeanChoice = new Choice("What do you mean?", "Well, all my life... I've been scared to stand up to people... maybe if I tell my daughter about warriors she won't back down like... like me...");
			DoYouMeanReaction = new Reaction();
			DoYouMeanReaction.AddAction(new NPCCallbackAction(UpdateDoYouMean));
			DoYouMeanReaction.AddAction(new UpdateCurrentTextAction(toControl, "Well, all my life... I've been scared to stand up to people... maybe if I tell my daughter about warriors she won't back down like... like me..."));
			
			NoStoriesSillyChoice = new Choice("No... stories are silly.", "I... well... if you find any stories just bring them to me...");
			NoStoriesSillyReaction = new Reaction();
			NoStoriesSillyReaction.AddAction(new NPCCallbackAction(UpdateNoStoriesSilly));
			NoStoriesSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "I... well... if you find any stories just bring them to me..."));		
			#endregion
			
			AlreadyBraveChoice = new Choice ("She's already brave.", "I... I guess you're right. Maybe my daughter doesn't need stories, maybe she is already brave...thanks for your help!");
			AlreadyBraveReaction = new Reaction();
			AlreadyBraveReaction.AddAction(new NPCCallbackAction(UpdateAlreadyBrave));
			AlreadyBraveReaction.AddAction(new UpdateCurrentTextAction(toControl, "I... I guess you're right. Maybe my daughter doesn't need stories, maybe she is already brave... thanks for your help!"));
		}
		public void UpdateGiveSeeds(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allItemReactions.Remove(StringsItem.SunflowerSeeds);
			FlagManager.instance.SetFlag(FlagStrings.FarmAlive);	
		}
		public void UpdateAlreadyBrave(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			startedConversation = true;
			_npcInState.animationData.Play("Shovel Idle");
			SetDefaultText("Thanks for your help! I'm proud of my daughter");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
			FlagManager.instance.SetFlag(FlagStrings.AlreadyBrave);
		}
		#region UpdatePathOne
		public void UpdateStandUp(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
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
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Remove(NotHardChoice);
			_allChoiceReactions.Remove(CowardChoice);
			_allChoiceReactions.Add(DoItChoice, new DispositionDependentReaction(DoItReaction));
			_allChoiceReactions.Add(OnYourOwnChoice, new DispositionDependentReaction(OnYourOwnReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultDialogue();
		}
		public void UpdateCoward (){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Remove(NotHardChoice);
			_allChoiceReactions.Remove(CowardChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'd rather not talk anymore.");
			_npcInState.animationData.Play("Shovel Idle");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
			FlagManager.instance.SetFlag(FlagStrings.YourCoward);
		}
		public void UpdateDoIt(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Remove(DoItChoice);
			_allChoiceReactions.Remove(OnYourOwnChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I doubt you'll get anywhere talking with my wife.");
			//_npcInState.animationData.Play("Shovel Idle");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
			FlagManager.instance.SetFlag(FlagStrings.IllDoIt);
		}
		public void UpdateOnYourOwn(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allChoiceReactions.Remove(DoItChoice);
			_allChoiceReactions.Remove(OnYourOwnChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Thanks for what little you could do.");
			//_npcInState.animationData.Play("Shovel Idle");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
		}
		#endregion
		
		#region UpdatePathTwo
		public void UpdateStoriesSilly(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Clear();
			startedConversation = true;
			_allChoiceReactions.Add(DoYouMeanChoice, new DispositionDependentReaction(DoYouMeanReaction));
			_allChoiceReactions.Add(NoStoriesSillyChoice, new DispositionDependentReaction(NoStoriesSillyReaction));
			GUIManager.Instance.RefreshInteraction();
			FlagManager.instance.SetFlag(FlagStrings.HusbandSillyStories);
			//SetDefaultDialogue();
		}
		public void UpdateDoYouMean(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
_npcInState.ChangeFacialExpression(StringsNPC.Angry);
			_allChoiceReactions.Remove(DoYouMeanChoice);
			_allChoiceReactions.Add(StandUpChoice, new DispositionDependentReaction(StandUpReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultDialogue();
		}
		public void UpdateNoStoriesSilly(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("If you find a story tell me...");
			FlagManager.instance.SetFlag(FlagStrings.BusinessTimer);
		}
		#endregion
		public override void PassStringToEmotionState(string text){
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
