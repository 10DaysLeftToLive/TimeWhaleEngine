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
		//ChangeToTalkingState.AddAction(new NPCEmotionUpdateAction(this, new MeetFamily(this, "")));
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
		
		
	
		public MeetFamily(NPC toControl, string currentDialogue) : base(toControl, "Hey..."){
			WhatDoYouLikeToDoChoice = new Choice("What do you like to do?", "I dunno...stuff.");
			WhatDoYouLikeToDoReaction = new Reaction();
			WhatDoYouLikeToDoReaction.AddAction(new NPCCallbackAction(UpdateWhatDoYouLikeToDo));
			WhatDoYouLikeToDoReaction.AddAction(new UpdateCurrentTextAction(toControl, "I dunno...stuff."));
			
			WhereDidYouUseToLiveChoice = new Choice("Where did you use to live?", "Nowhere.");
			WhereDidYouUseToLiveReaction = new Reaction();
			WhereDidYouUseToLiveReaction.AddAction(new NPCCallbackAction(UpdateWhereDidYouUseToLive));
			WhereDidYouUseToLiveReaction.AddAction(new UpdateCurrentTextAction(toControl, "Nowhere"));
			
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
			
			NoChoice = new Choice("No", "You're just like the people back home!  I don't want to talk to you again!");
			NoReaction = new Reaction();
			NoReaction.AddAction(new NPCCallbackAction(UpdateNo));
			NoReaction.AddAction(new UpdateCurrentTextAction(toControl, "You're just like the people back home!  I don't want to talk to you again!"));
			
			PoetryIsSillyChoice = new Choice("Poetry is silly.", "You're just like the people back home!  I don't want to talk to you again!");
			PoetryIsSillyReaction = new Reaction();
			PoetryIsSillyReaction.AddAction(new NPCCallbackAction(UpdatePoetryIsSilly));
			PoetryIsSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "You're just like the people back home!  I don't want to talk to you again!"));
			
			ThatSoundsCoolChoice = new Choice("That sounds cool!", "It is!  My dad used to read it to me before... Never mind..I think I hear my mom calling me.");
			ThatSoundsCoolReaction = new Reaction();
			ThatSoundsCoolReaction.AddAction(new NPCCallbackAction(UpdateThatSoundsCool));
			ThatSoundsCoolReaction.AddAction(new UpdateCurrentTextAction(toControl, "It is!  My dad used to read it to me before... Never mind..I think I hear my mom calling me."));
			
			ILikePoetryChoice = new Choice("I like poetry!", "Yeah Poetry is awesome! Maybe this new village isn't that bad...");
			ILikePoetryReaction = new Reaction();
			ILikePoetryReaction.AddAction(new NPCCallbackAction(UpdateILikePoetry));
			ILikePoetryReaction.AddAction(new UpdateCurrentTextAction(toControl, "Yeah Poetry is awesome! Maybe this new village isn't that bad..."));
		
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
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Visit the Island with you and CastleMan Friends		
	private class VisitAsFriend : EmotionState{
	
	
		public VisitAsFriend(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Visit without being friends
	private class VisitNotAsFriend : EmotionState{
	
	
		public VisitNotAsFriend(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
