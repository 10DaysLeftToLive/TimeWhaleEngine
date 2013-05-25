using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanYoung specific scripting values
/// </summary>
public class CastlemanYoung : NPC {
	MeetFamily BecomingFriends;
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction ChangeToTalkingState = new Reaction();
		ChangeToTalkingState.AddAction(new NPCEmotionUpdateAction(this, new MeetFamily(this, "")));
		flagReactions.Add(FlagStrings.MusicianCommentOnSon, ChangeToTalkingState);
	}
	
	protected override EmotionState GetInitEmotionState(){
		BecomingFriends = new MeetFamily(this, "");
		return (new InitialEmotionState(this, "..."));
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
		Reaction AppleReaction;
		Reaction FishingRodReaction;
		Reaction PlushieReaction;
		Reaction ToolBoxReaction;
		Reaction SeaShellReaction;
		Reaction RoseReaction;
		Reaction CaptainsLogReaction;
		Reaction ToySwordReaction;
		Reaction PortraitReaction;
		Reaction RopeReaction;
		Reaction VegetableReaction;
		Reaction ToyPuzzleReaction;
		Reaction ToyBoatReaction;
		Reaction FluteReaction;
		Reaction ShovelReaction;
		Reaction ApplePieReaction;
		Reaction LillySeedsReaction;
		Reaction TulipSeedsReaction;
		Reaction SunflowerSeedsReaction;
		Reaction PendantReaction;
		Reaction NoteReaction;
		Reaction HarpReaction;		
		
		Choice WhatsYourNameChoice;
		Reaction WhatsYourNameReaction;
		Choice AreYouNewChoice;
		Reaction AreYouNewReaction;
		Choice FineHaveItYourWayChoice;
		Reaction FineHaveItYourWayReaction;
		Choice DontYouSpeakChoice;
		Reaction DontYouSpeakReaction;
		Choice WellTellMeChoice;
		Reaction WellTellMeReaction;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "..."){
			AppleReaction = new Reaction();
			AppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(AppleReaction));
				
			FishingRodReaction = new Reaction();
			FishingRodReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.FishingRod, new DispositionDependentReaction(FishingRodReaction));
				
			PlushieReaction = new Reaction();
			PlushieReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.TimeWhalePlushie, new DispositionDependentReaction(PlushieReaction));
				
			ToolBoxReaction = new Reaction();
			ToolBoxReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolBoxReaction));
				
			SeaShellReaction = new Reaction();
			SeaShellReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Seashell, new DispositionDependentReaction(SeaShellReaction));
				
			RoseReaction = new Reaction();
			RoseReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Rose, new DispositionDependentReaction(RoseReaction));
				
			CaptainsLogReaction = new Reaction();
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.CaptainsLog, new DispositionDependentReaction(CaptainsLogReaction));
				
			ToySwordReaction = new Reaction();
			ToySwordReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.ToySword, new DispositionDependentReaction(ToySwordReaction));
			
			PortraitReaction = new Reaction();
			PortraitReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Portrait, new DispositionDependentReaction(PortraitReaction));
			
			RopeReaction = new Reaction();
			RopeReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Rope, new DispositionDependentReaction(RopeReaction));
			
			VegetableReaction = new Reaction();
			VegetableReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Vegetable, new DispositionDependentReaction(VegetableReaction));
			
			ToyPuzzleReaction = new Reaction();
			ToyPuzzleReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.ToyPuzzle, new DispositionDependentReaction(ToyPuzzleReaction));
			
			ToyBoatReaction = new Reaction();
			ToyBoatReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.ToyBoat, new DispositionDependentReaction(ToyBoatReaction));
			
			FluteReaction = new Reaction();
			FluteReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Flute, new DispositionDependentReaction(FluteReaction));
			
			ShovelReaction = new Reaction();
			ShovelReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Shovel, new DispositionDependentReaction(ShovelReaction));
			
			ApplePieReaction = new Reaction();
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.ApplePie, new DispositionDependentReaction(ApplePieReaction));
			
			LillySeedsReaction = new Reaction();
			LillySeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.LilySeeds, new DispositionDependentReaction(LillySeedsReaction));
				
			TulipSeedsReaction = new Reaction();
			TulipSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.TulipSeeds, new DispositionDependentReaction(TulipSeedsReaction));
				
			SunflowerSeedsReaction = new Reaction();
			SunflowerSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.SunflowerSeeds, new DispositionDependentReaction(SunflowerSeedsReaction));
				
			PendantReaction = new Reaction();
			PendantReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Pendant, new DispositionDependentReaction(PendantReaction));
				
			NoteReaction = new Reaction();
			NoteReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Note, new DispositionDependentReaction(NoteReaction));
			
			HarpReaction = new Reaction();
			HarpReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Harp, new DispositionDependentReaction(HarpReaction));
			
			
			
			WhatsYourNameChoice = new Choice("What's your name?", "......");
			WhatsYourNameReaction = new Reaction();
			WhatsYourNameReaction.AddAction(new NPCCallbackAction(UpdateWhatsYourName));
			WhatsYourNameReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allChoiceReactions.Add(WhatsYourNameChoice, new DispositionDependentReaction(WhatsYourNameReaction));
			
			AreYouNewChoice = new Choice("Are you new?", "......");
			AreYouNewReaction = new Reaction();
			AreYouNewReaction.AddAction(new NPCCallbackAction(UpdateAreYouNew));
			AreYouNewReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allChoiceReactions.Add(AreYouNewChoice, new DispositionDependentReaction(AreYouNewReaction));
			
			FineHaveItYourWayChoice = new Choice("Fine!  Have it your way!", "..................");
			FineHaveItYourWayReaction = new Reaction();
			FineHaveItYourWayReaction.AddAction(new NPCCallbackAction(UpdateFineHaveItYourWay));
			FineHaveItYourWayReaction.AddAction(new UpdateCurrentTextAction(toControl, "................."));
			
			DontYouSpeakChoice = new Choice("Don't you speak?", ".............");
			DontYouSpeakReaction = new Reaction();
			DontYouSpeakReaction.AddAction(new NPCCallbackAction(UpdateDontYouSpeak));
			DontYouSpeakReaction.AddAction(new UpdateCurrentTextAction(toControl, "............."));
			
			WellTellMeChoice = new Choice("Well tell me when you want to talk.", "....................");
			WellTellMeReaction = new Reaction();
			WellTellMeReaction.AddAction(new NPCCallbackAction(UpdateWellTellMe));
			WellTellMeReaction.AddAction(new UpdateCurrentTextAction(toControl, "...................."));
		}
		public void UpdateDontYouSpeak(){
			_allChoiceReactions.Remove(DontYouSpeakChoice);
			_allChoiceReactions.Add(WellTellMeChoice, new DispositionDependentReaction(WellTellMeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateWellTellMe(){
			_allChoiceReactions.Remove(WellTellMeChoice);
			_allChoiceReactions.Remove(FineHaveItYourWayChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateFineHaveItYourWay(){
			if (_allChoiceReactions.ContainsKey(WhatsYourNameChoice)){
				_allChoiceReactions.Remove(WhatsYourNameChoice);	
			}
			if (_allChoiceReactions.ContainsKey(DontYouSpeakChoice)){
				_allChoiceReactions.Remove(DontYouSpeakChoice);
			}
			_allChoiceReactions.Remove(FineHaveItYourWayChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateAreYouNew(){
			_allChoiceReactions.Remove(WhatsYourNameChoice);
			_allChoiceReactions.Remove(AreYouNewChoice);
			_allChoiceReactions.Add(FineHaveItYourWayChoice, new DispositionDependentReaction(FineHaveItYourWayReaction));
			_allChoiceReactions.Add(DontYouSpeakChoice, new DispositionDependentReaction(DontYouSpeakReaction));
			GUIManager.Instance.RefreshInteraction();
			FlagManager.instance.SetFlag(FlagStrings.MusicianCommentOnSon);
		}
		public void UpdateWhatsYourName(){
			_allChoiceReactions.Remove(WhatsYourNameChoice);
			_allChoiceReactions.Remove(AreYouNewChoice);
			_allChoiceReactions.Add(FineHaveItYourWayChoice, new DispositionDependentReaction(FineHaveItYourWayReaction));
			_allChoiceReactions.Add(DontYouSpeakChoice, new DispositionDependentReaction(DontYouSpeakReaction));
			GUIManager.Instance.RefreshInteraction();
			FlagManager.instance.SetFlag(FlagStrings.MusicianCommentOnSon);
		}
		
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region EmptyEmotion
	private class EmptyEmotion : EmotionState{
		Reaction CloseChatReaction;
		
		public EmptyEmotion(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			//toControl.CanTalk();
			//toControl.
			CloseChatReaction = new Reaction();
			CloseChatReaction.AddAction(new NPCCallbackAction(TestClose));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(CloseChatReaction));
		}
		public void TestClose(){
			//GUIManager.Instance.CloseInteractionMenu();
			//GUIManager.Instance.
			

		}
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Meet the Family	
	private class MeetFamily : EmotionState{
		int FriendshipTally = 0;
		Choice WhatDoYouLikeToDoChoice;
		Reaction WhatDoYouLikeToDoReaction;
		Choice WhereDidYouUseToLiveChoice;
		Reaction WhereDidYouUseToLiveReaction;
		
		Choice YouCantHaveLivedNowhereChoice;
		Reaction YouCantHaveLivedNowhereReaction;
		Choice WellIDontHateYouChoice;
		Reaction WellIDontHateYouReaction;
		Choice PeopleAreNicerHereChoice;
		Reaction PeopleAreNicerHereReaction;
		Choice PeopleHereAreMeanChoice;
		Reaction PeopleHereAreMeanReaction;
		
		Choice YouHaveToDoSomethingChoice;
		Reaction YouHaveToDoSomethingReaction;
		Choice OkayChoice;
		Reaction OkayReaction;
		Choice NoChoice;
		Reaction NoReaction;
		Choice PoetryIsSillyChoice;
		Reaction PoetryIsSillyReaction;
		Choice ThatSoundsCoolChoice;
		Reaction ThatSoundsCoolReaction;
		Choice ILikePoetryChoice;
		Reaction ILikePoetryReaction;
		
		Choice ImSorryChoice;
		Reaction ImSorryReaction;
		Choice ItsBecauseYourStupidChoice;
		Reaction ItsBecauseYourStupidReaction;
		Choice ImReallyReallySorryChoice;
		Reaction ImReallyReallySorryReaction;
		Choice YouGotMeIWasLyingChoice;
		Reaction YouGotMeIWasLyingReaction;
		public MeetFamily(NPC toControl, string currentDialogue) : base(toControl, "Hey..."){
			WhatDoYouLikeToDoChoice = new Choice("What do you like to do?", "I dunno...stuff.");
			WhatDoYouLikeToDoReaction = new Reaction();
			WhatDoYouLikeToDoReaction.AddAction(new NPCCallbackAction(UpdateWhatDoYouLikeToDo));
			WhatDoYouLikeToDoReaction.AddAction(new UpdateCurrentTextAction(toControl, "I dunno...stuff."));
			_allChoiceReactions.Add(WhatDoYouLikeToDoChoice, new DispositionDependentReaction(WhatDoYouLikeToDoReaction));
			
			WhereDidYouUseToLiveChoice = new Choice("Where did you use to live?", "Nowhere.");
			WhereDidYouUseToLiveReaction = new Reaction();
			WhereDidYouUseToLiveReaction.AddAction(new NPCCallbackAction(UpdateWhereDidYouUseToLive));
			WhereDidYouUseToLiveReaction.AddAction(new UpdateCurrentTextAction(toControl, "Nowhere"));
			_allChoiceReactions.Add(WhereDidYouUseToLiveChoice, new DispositionDependentReaction(WhereDidYouUseToLiveReaction));
			
			YouCantHaveLivedNowhereChoice = new Choice("You can't have lived nowhere!", "It was a horrible place...everyone hated me...");
			YouCantHaveLivedNowhereReaction = new Reaction();
			YouCantHaveLivedNowhereReaction.AddAction(new NPCCallbackAction(UpdateYouCantHaveLivedNowhere));
			YouCantHaveLivedNowhereReaction.AddAction(new UpdateCurrentTextAction(toControl, "It was a horrible place...everyone hated me..."));
			
			WellIDontHateYouChoice = new Choice("Well I don't hate you.", "People here don't know me that well yet...");
			WellIDontHateYouReaction = new Reaction();
			WellIDontHateYouReaction.AddAction(new NPCCallbackAction(UpdateWellIDontHateYou));
			WellIDontHateYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "People here don't know me that well yet..."));
			
			PeopleAreNicerHereChoice = new Choice("People are nicer here.", "People here don't know me that well yet...");
			PeopleAreNicerHereReaction = new Reaction();
			PeopleAreNicerHereReaction.AddAction(new NPCCallbackAction(UpdatePeopleAreNicerHere));
			PeopleAreNicerHereReaction.AddAction(new UpdateCurrentTextAction(toControl, "People here don't know me that well yet..."));
			
			PeopleHereAreMeanChoice = new Choice("People are mean here.", "I'm sure...everyone I meet hates me...");
			PeopleHereAreMeanReaction = new Reaction();
			PeopleHereAreMeanReaction.AddAction(new NPCCallbackAction(UpdatePeopleHereAreMean));
			PeopleHereAreMeanReaction.AddAction(new UpdateCurrentTextAction(toControl, "I'm sure...everyone I meet hates me..."));
			
			YouHaveToDoSomethingChoice = new Choice("You have to do something...", "...I'll only tell you if you won't laugh. Promise not to laugh kay?");
			YouHaveToDoSomethingReaction = new Reaction();
			YouHaveToDoSomethingReaction.AddAction(new NPCCallbackAction(UpdateYouHaveToDoSomething));
			YouHaveToDoSomethingReaction.AddAction(new UpdateCurrentTextAction(toControl, "...I'll only tell you if you won't laugh. Promise not to laugh kay?"));
			
			OkayChoice = new Choice("Okay", "All right...I...\nI like poetry!");
			OkayReaction = new Reaction();
			OkayReaction.AddAction(new NPCCallbackAction(UpdateOkay));
			OkayReaction.AddAction(new UpdateCurrentTextAction(toControl, "All right...I...\nI like poetry!"));
			
			NoChoice = new Choice("No", "You're mean! Just like the people back home!");
			NoReaction = new Reaction();
			NoReaction.AddAction(new NPCCallbackAction(UpdateNo));
			NoReaction.AddAction(new UpdateCurrentTextAction(toControl, "You're mean! Just like the people back home!"));
			
			PoetryIsSillyChoice = new Choice("Poetry is silly.", "You're mean! Just like the people back home!");
			PoetryIsSillyReaction = new Reaction();
			PoetryIsSillyReaction.AddAction(new NPCCallbackAction(UpdatePoetryIsSilly));
			PoetryIsSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "You're mean! Just like the people back home!"));
			
			ThatSoundsCoolChoice = new Choice("That sounds cool!", "It is!  My dad used to read it to me before... Never mind..I think I hear my mom calling me.");
			ThatSoundsCoolReaction = new Reaction();
			ThatSoundsCoolReaction.AddAction(new NPCCallbackAction(UpdateThatSoundsCool));
			ThatSoundsCoolReaction.AddAction(new UpdateCurrentTextAction(toControl, "It is!  My dad used to read it to me before... Never mind..I think I hear my mom calling me."));
			
			ILikePoetryChoice = new Choice("I like poetry!", "Yeah Poetry is awesome! Maybe this new village isn't that bad...");
			ILikePoetryReaction = new Reaction();
			ILikePoetryReaction.AddAction(new NPCCallbackAction(UpdateILikePoetry));
			ILikePoetryReaction.AddAction(new UpdateCurrentTextAction(toControl, "Yeah Poetry is awesome! Maybe this new village isn't that bad..."));
			
			//Choices for Making Fun of or apologizing to the Castleman.
			ImSorryChoice = new Choice ("I'm Sorry...", "Are you really sorry?/nYou don't sound sorry...");
			ImSorryReaction = new Reaction();
			ImSorryReaction.AddAction(new NPCCallbackAction(UpdateImSorry));
			ImSorryReaction.AddAction(new UpdateCurrentTextAction(toControl, "Are you really sorry?/nYou don't sound sorry..."));
			
			ItsBecauseYourStupidChoice = new Choice ("It's because you're stupid.","I trusted you...I'm not going to make that mistake again...");
			ItsBecauseYourStupidReaction = new Reaction();
			ImSorryReaction.AddAction(new NPCCallbackAction(UpdateItsBecauseYourStupid));
			ImSorryReaction.AddAction(new UpdateCurrentTextAction(toControl, "I trusted you...I'm not going to make that mistake again..."));
			
			ImReallyReallySorryChoice = new Choice ("I'm really really sorry.", "");
			ImReallyReallySorryReaction = new Reaction();
			ImSorryReaction.AddAction(new NPCCallbackAction(UpdateImReallyReallySorry));
			ImSorryReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
			
			YouGotMeIWasLyingChoice = new Choice ("You got me!\nI was lying.", "I trusted you...I'm not going to make that mistake again...");
			YouGotMeIWasLyingReaction = new Reaction();
			ImSorryReaction.AddAction(new NPCCallbackAction(UpdateYouGotMeIWasLying));
			ImSorryReaction.AddAction(new UpdateCurrentTextAction(toControl, "I trusted you...I'm not going to make that mistake again..."));
		}
		public void UpdateWhatDoYouLikeToDo(){
			if (_allChoiceReactions.ContainsKey(WhereDidYouUseToLiveChoice)){
				_allChoiceReactions.Remove(WhereDidYouUseToLiveChoice);	
			}
			_allChoiceReactions.Remove(WhatDoYouLikeToDoChoice);
			_allChoiceReactions.Add(YouHaveToDoSomethingChoice, new DispositionDependentReaction(YouHaveToDoSomethingReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I dunno...stuff.");
		}
		public void UpdateYouHaveToDoSomething(){
			_allChoiceReactions.Remove(YouHaveToDoSomethingChoice);
			_allChoiceReactions.Add(OkayChoice, new DispositionDependentReaction(OkayReaction));
			_allChoiceReactions.Add(NoChoice, new DispositionDependentReaction(NoReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("...I'll only tell you what I like to do if you promise you won't laugh!");
		}
		public void UpdateOkay(){
			_allChoiceReactions.Remove(OkayChoice);
			_allChoiceReactions.Remove(NoChoice);
			_allChoiceReactions.Add(ILikePoetryChoice, new DispositionDependentReaction(ILikePoetryReaction));
			_allChoiceReactions.Add(ThatSoundsCoolChoice, new DispositionDependentReaction(ThatSoundsCoolReaction));
			_allChoiceReactions.Add(PoetryIsSillyChoice, new DispositionDependentReaction(PoetryIsSillyReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("All right...I...\nI like Poetry!");
		}
		public void UpdateNo(){
			_allChoiceReactions.Remove(OkayChoice);
			_allChoiceReactions.Remove(NoChoice);
			_allChoiceReactions.Add(ImSorryChoice, new DispositionDependentReaction(ImSorryReaction));
			_allChoiceReactions.Add(ItsBecauseYourStupidChoice, new DispositionDependentReaction(ItsBecauseYourStupidReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("You're just like the people back home!");
			FriendshipTally--;
		}
		public void UpdateILikePoetry(){
			_allChoiceReactions.Remove(ILikePoetryChoice);
			_allChoiceReactions.Remove(ThatSoundsCoolChoice);
			_allChoiceReactions.Remove(PoetryIsSillyChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I love poetry!");
			FriendshipTally += 2;
		}
		public void UpdateThatSoundsCool(){
			_allChoiceReactions.Remove(ILikePoetryChoice);
			_allChoiceReactions.Remove(ThatSoundsCoolChoice);
			_allChoiceReactions.Remove(PoetryIsSillyChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I love poetry!");
			FriendshipTally += 1;
		}
		public void UpdatePoetryIsSilly(){
			_allChoiceReactions.Remove(ILikePoetryChoice);
			_allChoiceReactions.Remove(ThatSoundsCoolChoice);
			_allChoiceReactions.Remove(PoetryIsSillyChoice);
			_allChoiceReactions.Add(ImSorryChoice, new DispositionDependentReaction(ImSorryReaction));
			_allChoiceReactions.Add(ItsBecauseYourStupidChoice, new DispositionDependentReaction(ItsBecauseYourStupidReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("You're just like the people back home!");
			FriendshipTally -= 3;
		}
		public void UpdateWhereDidYouUseToLive(){
			_allChoiceReactions.Remove(WhereDidYouUseToLiveChoice);
			_allChoiceReactions.Remove(WhatDoYouLikeToDoChoice);
			_allChoiceReactions.Add(YouCantHaveLivedNowhereChoice, new DispositionDependentReaction(YouCantHaveLivedNowhereReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I don't care about where I used to live...");
		}
		public void UpdateYouCantHaveLivedNowhere(){
			_allChoiceReactions.Remove(YouCantHaveLivedNowhereChoice);
			_allChoiceReactions.Add(WellIDontHateYouChoice, new DispositionDependentReaction(WellIDontHateYouReaction));
			_allChoiceReactions.Add(PeopleAreNicerHereChoice, new DispositionDependentReaction(PeopleAreNicerHereReaction));
			_allChoiceReactions.Add(PeopleHereAreMeanChoice, new DispositionDependentReaction(PeopleHereAreMeanReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("My old home was horrible...everyone made fun of me.");
		}
		public void UpdateWellIDontHateYou(){
			_allChoiceReactions.Remove(WellIDontHateYouChoice);
			_allChoiceReactions.Remove(PeopleHereAreMeanChoice);
			_allChoiceReactions.Remove(PeopleAreNicerHereChoice);
			_allChoiceReactions.Add(WhatDoYouLikeToDoChoice, new DispositionDependentReaction(WhatDoYouLikeToDoReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'm sure you'll hate me once you get to know me...");
			FriendshipTally += 1;
		}
		public void UpdatePeopleAreNicerHere(){
			_allChoiceReactions.Remove(WellIDontHateYouChoice);
			_allChoiceReactions.Remove(PeopleHereAreMeanChoice);
			_allChoiceReactions.Remove(PeopleAreNicerHereChoice);
			_allChoiceReactions.Add(WhatDoYouLikeToDoChoice, new DispositionDependentReaction(WhatDoYouLikeToDoReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'm sure you'll hate me once you get to know me...");
			FriendshipTally += 1;
		}
		public void UpdatePeopleHereAreMean(){
			_allChoiceReactions.Remove(WellIDontHateYouChoice);
			_allChoiceReactions.Remove(PeopleHereAreMeanChoice);
			_allChoiceReactions.Remove(PeopleAreNicerHereChoice);
			_allChoiceReactions.Add(WhatDoYouLikeToDoChoice, new DispositionDependentReaction(WhatDoYouLikeToDoReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("People are always mean.");
			FriendshipTally -= 2;
		}
		public void UpdateImSorry(){
			_allChoiceReactions.Remove(ImSorryChoice);
			_allChoiceReactions.Remove(ItsBecauseYourStupidChoice);
			_allChoiceReactions.Add(ImReallyReallySorryChoice, new DispositionDependentReaction(ImReallyReallySorryReaction));
			_allChoiceReactions.Add(YouGotMeIWasLyingChoice, new DispositionDependentReaction(YouGotMeIWasLyingReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("You should apologize!");
		}
		public void UpdateItsBecauseYourStupid(){
			_allChoiceReactions.Remove(ImSorryChoice);
			_allChoiceReactions.Remove(ItsBecauseYourStupidChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I hate you!");
			//This is where we add a flag update?
		}
		public void UpdateImReallyReallySorry(){
			_allChoiceReactions.Remove(YouGotMeIWasLyingChoice);
			_allChoiceReactions.Remove(ImReallyReallySorryChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I guess I believe you...");
			FriendshipTally += 1;
		}
		public void UpdateYouGotMeIWasLying(){
			_allChoiceReactions.Remove(YouGotMeIWasLyingChoice);
			_allChoiceReactions.Remove(ImReallyReallySorryChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I hate you!");
			//Flag update goes here!!!!!!!
		}
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Visit the Island with you and CastleMan Friends		
	private class VisitCarpenterSonAsFriend : EmotionState{
		Choice ImSureHeDoesntHateYouChoice;
		Reaction ImSureHeDoesntHateYouReaction;
		Choice DontBeSoScaredChoice;
		Reaction DontBeSoScaredReaction;
		Choice JustTalkAboutFishChoice;
		Reaction JustTalkAboutFishReaction;
		Choice HeTotallyHatesYouChoice;
		Reaction HeTotallyHatesYouReaction;
		Choice GoodPointLetsMoveOnChoice;
		Reaction GoodPointLetsMoveOnReaction;
		Choice NotToHimChoice;
		Reaction NotToHimReaction;
		Choice PoetryIsntBoringToYouChoice;
		Reaction PoetryIsntBoringToYouReaction;
	
		public VisitCarpenterSonAsFriend(NPC toControl, string currentDialogue) : base(toControl, "Ugh...people here are just like back home!"){
			ImSureHeDoesntHateYouChoice = new Choice("I'm sure he doesn't hate you.", "I...I dunno...");
			ImSureHeDoesntHateYouReaction = new Reaction();
			ImSureHeDoesntHateYouReaction.AddAction(new NPCCallbackAction(UpdateImSureHeDoesntHateYou));
			ImSureHeDoesntHateYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...I dunno..."));
			_allChoiceReactions.Add(ImSureHeDoesntHateYouChoice, new DispositionDependentReaction(ImSureHeDoesntHateYouReaction));
			
			HeTotallyHatesYouChoice = new Choice("He totally hates you!", "I'm not surprised!  Everyone I meet hates me...");
			HeTotallyHatesYouReaction = new Reaction();
			HeTotallyHatesYouReaction.AddAction(new NPCCallbackAction(UpdateHeTotallyHatesYou));
			HeTotallyHatesYouReaction.AddAction(new ShowOneOffChatAction(toControl, "I'm not surprised!  Everyone I meet hates me..."));
			_allChoiceReactions.Add(HeTotallyHatesYouChoice, new DispositionDependentReaction(HeTotallyHatesYouReaction));
			
			DontBeSoScaredChoice = new Choice("Don't be so scared.", "But I don't know what to say!");
			DontBeSoScaredReaction = new Reaction ();
			DontBeSoScaredReaction.AddAction(new NPCCallbackAction(UpdateDontBeSoScared));
			DontBeSoScaredReaction.AddAction(new UpdateCurrentTextAction(toControl, "But I don't know what to say!"));
			
			JustTalkAboutFishChoice = new Choice("Just talk to him about fishing!", "But fishing is boring!");
			JustTalkAboutFishReaction = new Reaction ();
			JustTalkAboutFishReaction.AddAction(new NPCCallbackAction(UpdateJustTalkAboutFish));
			JustTalkAboutFishReaction.AddAction(new UpdateCurrentTextAction(toControl, "But fishing is boring!"));
			
			NotToHimChoice =  new Choice ("Not to him.", "I guess I'll give it another try...");
			NotToHimReaction = new Reaction ();
			NotToHimReaction.AddAction(new NPCCallbackAction(UpdateNotToHim));
			NotToHimReaction.AddAction(new ShowOneOffChatAction(toControl, "I guess I'll give it another try."));
			
			PoetryIsntBoringToYouChoice = new Choice("Poetry isnt boring to you.", "Yeah...maybe I shouldn't make fun of his interests...");
			PoetryIsntBoringToYouReaction = new Reaction ();
			PoetryIsntBoringToYouReaction.AddAction(new NPCCallbackAction(UpdatePoetryIsntBoring));
			PoetryIsntBoringToYouReaction.AddAction(new ShowOneOffChatAction(toControl, "Yeah...maybe I shouldn't make fun of his interests..."));
			
			GoodPointLetsMoveOnChoice = new Choice ("Good point! Let's move on.", "");
			GoodPointLetsMoveOnReaction = new Reaction();
			GoodPointLetsMoveOnReaction.AddAction(new NPCCallbackAction(UpdateGoodPointLetsMoveOn));
			GoodPointLetsMoveOnReaction.AddAction(new ShowOneOffChatAction(toControl, "You're right!  This is stupid anyways."));
		
		}
		
		public void UpdateImSureHeDoesntHateYou(){
			_allChoiceReactions.Remove(ImSureHeDoesntHateYouChoice);
			_allChoiceReactions.Remove (HeTotallyHatesYouChoice);
			_allChoiceReactions.Add(DontBeSoScaredChoice, new DispositionDependentReaction(DontBeSoScaredReaction));
			_allChoiceReactions.Add(JustTalkAboutFishChoice, new DispositionDependentReaction(JustTalkAboutFishReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I dunno if your right about him not hating me...");
		}
		public void UpdateHeTotallyHatesYou(){
			_allChoiceReactions.Remove(ImSureHeDoesntHateYouChoice);
			_allChoiceReactions.Remove (HeTotallyHatesYouChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I don't like this stupid island.\nLet's keep going so I can get back soon.");
			
		}
		public void UpdateDontBeSoScared(){
			_allChoiceReactions.Remove(DontBeSoScaredChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I wouldn't be scared if I knew what to say.");
		}
		public void UpdateJustTalkAboutFish(){
			if(_allChoiceReactions.ContainsKey(DontBeSoScaredChoice)){
				_allChoiceReactions.Remove(JustTalkAboutFishChoice);	
			}
			_allChoiceReactions.Remove(JustTalkAboutFishChoice);
			_allChoiceReactions.Add(NotToHimChoice, new DispositionDependentReaction(NotToHimReaction));
			_allChoiceReactions.Add(GoodPointLetsMoveOnChoice, new DispositionDependentReaction(GoodPointLetsMoveOnReaction));
			_allChoiceReactions.Add(PoetryIsntBoringToYouChoice, new DispositionDependentReaction(PoetryIsntBoringToYouReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I think fishing is stupid.");
		}
		public void UpdateNotToHim(){
			_allChoiceReactions.Remove(NotToHimChoice);
			_allChoiceReactions.Remove(GoodPointLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(PoetryIsntBoringToYouChoice)){
					_allChoiceReactions.Remove(PoetryIsntBoringToYouChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.ConvinceToTalkWithCarpenterSonRoundOne);
			SetDefaultText("I guess I'll just give it another try.");
		}
		public void UpdatePoetryIsntBoring(){
			_allChoiceReactions.Remove(NotToHimChoice);
			_allChoiceReactions.Remove(GoodPointLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(PoetryIsntBoringToYouChoice)){
					_allChoiceReactions.Remove(PoetryIsntBoringToYouChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.ConvinceToTalkWithCarpenterSonRoundOne);
			SetDefaultText("I guess I'll just give it another try.");
		}
		public void UpdateGoodPointLetsMoveOn(){
			_allChoiceReactions.Remove(NotToHimChoice);
			_allChoiceReactions.Remove(GoodPointLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(PoetryIsntBoringToYouChoice)){
					_allChoiceReactions.Remove(PoetryIsntBoringToYouChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.ConvinceToTalkWithCarpenterSonRoundOne);
			SetDefaultText("Let's go.");
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Visit without being friends
	private class VisitCarpenterSonNotAsFriend : EmotionState{
		Choice MaybeYouShouldTryTalkingToHimAgainChoice;
		Reaction MaybeYouShouldTryTalkingToHimReaction;
		Choice WhyWouldIWantToTrickYouChoice;
		Reaction WhyWouldIWantToTrickYouReaction;
		Choice JustTalkAboutFishChoice;
		Reaction JustTalkAboutFishReaction;
		Choice PrettyMuchChoice;
		Reaction PrettyMuchReaction;
		Choice UghFineLetsMoveOnChoice;
		Reaction UghFineLetsMoveOnReaction;
		Choice JustTalkToHimChoice;
		Reaction JustTalkToHimReaction;
		Choice JustTrustMeChoice;
		Reaction JustTrustMeReaction;
	
		public VisitCarpenterSonNotAsFriend(NPC toControl, string currentDialogue) : base(toControl, "Ugh...people here are just like back home!"){
			MaybeYouShouldTryTalkingToHimAgainChoice = new Choice("Maybe you should try talking to him again.", "You're just out to trick me into making a fool of myself!");
			MaybeYouShouldTryTalkingToHimReaction = new Reaction();
			MaybeYouShouldTryTalkingToHimReaction.AddAction(new NPCCallbackAction(UpdateMaybeYouShouldTryTalkingToHimAgain));
			MaybeYouShouldTryTalkingToHimReaction.AddAction(new UpdateCurrentTextAction(toControl, "You're just out to trick me into making a fool of myself!"));
			_allChoiceReactions.Add(MaybeYouShouldTryTalkingToHimAgainChoice, new DispositionDependentReaction(MaybeYouShouldTryTalkingToHimReaction));
			
			PrettyMuchChoice = new Choice("Pretty much!", "I hate all of you!");
			PrettyMuchReaction = new Reaction();
			PrettyMuchReaction.AddAction(new NPCCallbackAction(UpdatePrettyMuch));
			PrettyMuchReaction.AddAction(new ShowOneOffChatAction(toControl, "I hate all of you!"));
			_allChoiceReactions.Add(PrettyMuchChoice, new DispositionDependentReaction(PrettyMuchReaction));
			
			WhyWouldIWantToTrickYouChoice = new Choice("Why would I want to trick you?", "That's what someone trying to trick me would say!");
			WhyWouldIWantToTrickYouReaction = new Reaction ();
			WhyWouldIWantToTrickYouReaction.AddAction(new NPCCallbackAction(UpdateWhyWouldIWantToTrickYou));
			WhyWouldIWantToTrickYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "That's what someone trying to trick me would say!"));
			
			JustTalkAboutFishChoice = new Choice("Just talk to him about fishing!", "I'm not stupid!  I'm not going to fall for your trap!");
			JustTalkAboutFishReaction = new Reaction ();
			JustTalkAboutFishReaction.AddAction(new NPCCallbackAction(UpdateJustTalkAboutFish));
			JustTalkAboutFishReaction.AddAction(new UpdateCurrentTextAction(toControl, "I'm not stupid!  I'm not going to fall for your trap!"));
			
			JustTalkToHimChoice =  new Choice ("Just talk to him!", "Fine!  But if this is a joke at my expense...");
			JustTalkToHimReaction = new Reaction ();
			JustTalkToHimReaction.AddAction(new NPCCallbackAction(UpdateJustTalkToHim));
			JustTalkToHimReaction.AddAction(new ShowOneOffChatAction(toControl, "Fine!  But if this is a joke at my expense..."));
			
			JustTrustMeChoice = new Choice("Just trust me!", "Hmph fine!");
			JustTrustMeReaction = new Reaction ();
			JustTrustMeReaction.AddAction(new NPCCallbackAction(UpdateJustTrustMe));
			JustTrustMeReaction.AddAction(new ShowOneOffChatAction(toControl, "Hmmpphh!  Fine!"));
			
			UghFineLetsMoveOnChoice = new Choice ("Ugh!  Fine lets move on!", "");
			UghFineLetsMoveOnReaction = new Reaction();
			UghFineLetsMoveOnReaction.AddAction(new NPCCallbackAction(UpdateUghFineLetsMoveOn));
			UghFineLetsMoveOnReaction.AddAction(new ShowOneOffChatAction(toControl, "I hate all of you!"));
		
		}
		
		public void UpdateMaybeYouShouldTryTalkingToHimAgain(){
			_allChoiceReactions.Remove(MaybeYouShouldTryTalkingToHimAgainChoice);
			_allChoiceReactions.Remove (PrettyMuchChoice);
			_allChoiceReactions.Add(WhyWouldIWantToTrickYouChoice, new DispositionDependentReaction(WhyWouldIWantToTrickYouReaction));
			_allChoiceReactions.Add(JustTalkAboutFishChoice, new DispositionDependentReaction(JustTalkAboutFishReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Everyone is out to get me!");
		}
		public void UpdatePrettyMuch(){
			_allChoiceReactions.Remove(MaybeYouShouldTryTalkingToHimAgainChoice);
			_allChoiceReactions.Remove (PrettyMuchChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I hate all of you!");
			
		}
		public void UpdateWhyWouldIWantToTrickYou(){
			_allChoiceReactions.Remove(WhyWouldIWantToTrickYouChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I know for sure you are trying to trick me!");
		}
		public void UpdateJustTalkAboutFish(){
			if(_allChoiceReactions.ContainsKey(WhyWouldIWantToTrickYouChoice)){
				_allChoiceReactions.Remove(JustTalkAboutFishChoice);	
			}
			_allChoiceReactions.Remove(JustTalkAboutFishChoice);
			_allChoiceReactions.Add(JustTalkToHimChoice, new DispositionDependentReaction(JustTalkToHimReaction));
			_allChoiceReactions.Add(UghFineLetsMoveOnChoice, new DispositionDependentReaction(UghFineLetsMoveOnReaction));
			_allChoiceReactions.Add(JustTrustMeChoice, new DispositionDependentReaction(JustTrustMeReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I think fishing is stupid.");
		}
		public void UpdateJustTalkToHim(){
			_allChoiceReactions.Remove(JustTalkToHimChoice);
			_allChoiceReactions.Remove(UghFineLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(JustTrustMeChoice)){
					_allChoiceReactions.Remove(JustTrustMeChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.ConvinceToTalkWithCarpenterSonRoundOne);
			SetDefaultText("I guess so...");
		}
		public void UpdateJustTrustMe(){
			_allChoiceReactions.Remove(JustTalkToHimChoice);
			_allChoiceReactions.Remove(UghFineLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(JustTrustMeChoice)){
					_allChoiceReactions.Remove(JustTrustMeChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.ConvinceToTalkWithCarpenterSonRoundOne);
			SetDefaultText("Fine!  But if this is a trick...");
		}
		public void UpdateUghFineLetsMoveOn(){
			_allChoiceReactions.Remove(JustTalkToHimChoice);
			_allChoiceReactions.Remove(UghFineLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(JustTrustMeChoice)){
					_allChoiceReactions.Remove(JustTrustMeChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.ConvinceToTalkWithCarpenterSonRoundOne);
			SetDefaultText("Let's go.");
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
