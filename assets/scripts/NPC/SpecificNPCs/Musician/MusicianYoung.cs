using UnityEngine;
using System.Collections;

/// <summary>
/// MusicianYoung specific scripting values
/// </summary>
public class MusicianYoung : NPC {
	protected override void Init() {
		base.Init();
		this.SetCharacterPortrait(StringsNPC.Happy);
	}
	Schedule FinishedTalkingFriends;
	Schedule FinishedTalkingNOTFriends;
	NPCConvoSchedule TellSonToGoAroundIslandFriends;
	NPCConvoSchedule TellSonToGoAroundIslandNOTFriends;
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
		Choice MuteResponseChoice = new Choice("Is your son mute?", "Oh no! He's just very shy!");
		Reaction MuteResponseReaction = new Reaction();
		MuteResponseReaction.AddAction(new NPCRemoveChoiceAction(this, MuteResponseChoice));
		MuteResponseReaction.AddAction(new UpdateCurrentTextAction(this, "Oh no! He's just very shy!"));
		Reaction IsHeMuteReaction = new Reaction();
		IsHeMuteReaction.AddAction(new NPCAddChoiceAction(this, MuteResponseChoice, new DispositionDependentReaction(MuteResponseReaction)));
		flagReactions.Add(FlagStrings.MusicianCommentOnSon, IsHeMuteReaction);
		
		Reaction RespondToCMANAndYouFriends = new Reaction();
		RespondToCMANAndYouFriends.AddAction(new NPCAddScheduleAction(this, FinishedTalkingFriends));
		RespondToCMANAndYouFriends.AddAction(new NPCAddScheduleAction(this, TellSonToGoAroundIslandFriends));
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends, RespondToCMANAndYouFriends);
		
		Reaction RespondToCMANAndYouNOTFriends = new Reaction();
		RespondToCMANAndYouNOTFriends.AddAction(new NPCAddScheduleAction(this, FinishedTalkingNOTFriends));
		RespondToCMANAndYouNOTFriends.AddAction(new NPCAddScheduleAction(this, TellSonToGoAroundIslandNOTFriends));
		flagReactions.Add(FlagStrings.PlayerAndCastleNOTFriends, RespondToCMANAndYouNOTFriends);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hi there! I'm sorry I can't play you a tune, my strings broke this morning."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		FinishedTalkingFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task SetFlagFriends = (new TimeTask(2f, new IdleState(this)));
		SetFlagFriends.AddFlagToSet(FlagStrings.MusicianFinishedTalkingFriends);
		FinishedTalkingFriends.Add(SetFlagFriends);
		
		FinishedTalkingNOTFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task SetFlagNOTFriends = (new TimeTask(2f, new IdleState(this)));
		SetFlagNOTFriends.AddFlagToSet(FlagStrings.MusicianFinishedTalkingNOTFriends);
		FinishedTalkingNOTFriends.Add(SetFlagNOTFriends);
		
		TellSonToGoAroundIslandFriends = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CastlemanYoung), 
			new MusicianToCastlemanFriends(),Schedule.priorityEnum.DoConvo);
		TellSonToGoAroundIslandFriends.SetCanNotInteractWithPlayer();
		TellSonToGoAroundIslandNOTFriends = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CastlemanYoung), 
			new MusicianToCastlemanNotFriends(),Schedule.priorityEnum.DoConvo);
		TellSonToGoAroundIslandNOTFriends.SetCanNotInteractWithPlayer();
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		bool hasTalkedAboutTrip = false;
		bool hasTalkedAboutMusic = false;
		Choice WhatKindOfMusicChoice;
		Reaction WhatKindOfMusicReaction;
		Choice PlayForMeChoice;
		Reaction PlayForMeReaction;
		Choice IsThereAnyYouLikeChoice;
		Reaction IsThereAnyYouLikeReaction;
		
		Choice WhereDoYouComeFromChoice;
		Reaction WhereDoYouComeFromReaction;
		Choice WhyDidYouComeChoice;
		Reaction WhyDidYouComeReaction;
		Choice WhatWasTheTripLikeChoice;
		Reaction WhatWasTheTripLikeReaction;
		Choice WasYourSonCloseToHisFatherChoice;
		Reaction WasYourSonCloseToHisFatherReaction;
		Choice HowAreYouDoingChoice;
		Reaction HowAreYouDoingReaction;
		Choice CanIHelpChoice;
		Reaction CanIHelpReaction;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Hi! I'm the musician and that's my son over there. We're new to the island."){
			
			WhatKindOfMusicChoice = new Choice("What kind of music do you play?", "All sorts, I've tried to learn as many instruments as I can.");
			WhatKindOfMusicReaction = new Reaction();
			WhatKindOfMusicReaction.AddAction(new NPCCallbackAction(UpdateWhatKindOfMusic));
			WhatKindOfMusicReaction.AddAction(new UpdateCurrentTextAction(toControl, "All sorts, I've tried to learn as many instruments as I can."));
			_allChoiceReactions.Add(WhatKindOfMusicChoice, new DispositionDependentReaction(WhatKindOfMusicReaction));
			
			PlayForMeChoice = new Choice("Could you play something for me?", "I would play for you when I'm not tired from moving in.");
			PlayForMeReaction = new Reaction();
			PlayForMeReaction.AddAction(new NPCCallbackAction(UpdatePlayForMe));
			PlayForMeReaction.AddAction(new UpdateCurrentTextAction(toControl, "I would play for you when I'm not tired from moving in."));
			
			IsThereAnyYouLikeChoice = new Choice("What kind of musical instrument do you like?", "I like all of them, although the flute always has a special place in my heart, but I lost it at my old town.");
			IsThereAnyYouLikeReaction = new Reaction();
			IsThereAnyYouLikeReaction.AddAction(new NPCCallbackAction(UpdateIsThereAnyYouLike));
			IsThereAnyYouLikeReaction.AddAction(new UpdateCurrentTextAction(toControl, "I like all of them, although the flute always has a special place in my heart.But I lost it at my old town."));
			
			WhereDoYouComeFromChoice = new Choice("Where did you move from?", "A small town on the mainland.");
			WhereDoYouComeFromReaction = new Reaction();
			WhereDoYouComeFromReaction.AddAction(new NPCCallbackAction(UpdateWhereDoYouComeFrom));
			WhereDoYouComeFromReaction.AddAction(new UpdateCurrentTextAction(toControl, "A small town on the mainland."));
			_allChoiceReactions.Add(WhereDoYouComeFromChoice, new DispositionDependentReaction(WhereDoYouComeFromReaction));
			
			WhyDidYouComeChoice = new Choice("Why did you move?", "I figured after my husbands death, a change in scenery was needed.");
			WhyDidYouComeReaction = new Reaction();
			WhyDidYouComeReaction.AddAction(new NPCCallbackAction(UpdateWhyDidYouCome));
			WhyDidYouComeReaction.AddAction(new UpdateCurrentTextAction(toControl, "I figured after my husbands death, a change in scenery was needed."));
			
			WhatWasTheTripLikeChoice = new Choice("What was the trip like?", "It wasn't too bad. It was actually pretty pleasant!");
			WhatWasTheTripLikeReaction = new Reaction();
			WhatWasTheTripLikeReaction.AddAction(new NPCCallbackAction(UpdateWhatWasTheTripLike));
			WhatWasTheTripLikeReaction.AddAction(new UpdateCurrentTextAction(toControl, "It wasn't too bad. It was actually pretty pleasant!"));
			
			WasYourSonCloseToHisFatherChoice = new Choice("How is your son taking it?", "He's been very sad, he and his father were close. I think he just needs a friend though.");
			WasYourSonCloseToHisFatherReaction = new Reaction();
			WasYourSonCloseToHisFatherReaction.AddAction(new NPCCallbackAction(UpdateWasYourSonCloseToHisFatherReaction));
			WasYourSonCloseToHisFatherReaction.AddAction(new UpdateCurrentTextAction(toControl, "He's been very sad, he and his father were close. I think he just needs a friend though."));
			
			HowAreYouDoingChoice = new Choice("That's horrible, how are you doing?", "I... I could be better, but I think moving will help. I'm excited to meet my new neighbors!");
			HowAreYouDoingReaction = new Reaction();
			HowAreYouDoingReaction.AddAction(new NPCCallbackAction(UpdateHowAreYouDoing));
			HowAreYouDoingReaction.AddAction(new UpdateCurrentTextAction(toControl, "I... I could be better, but I think moving will help. I'm excited to meet my new neighbors!"));
			
			CanIHelpChoice = new Choice("Can I help you?", "Ha ha! I think my son and I are okay for now, you've been such a kind neighbor.I hope that we can become friends in the future!");
			CanIHelpReaction = new Reaction ();
			CanIHelpReaction.AddAction(new NPCCallbackAction(UpdateCanIHelp));
			CanIHelpReaction.AddAction(new UpdateCurrentTextAction(toControl, "Ha ha! I think we are okay, you've been such a kind neighbor.I hope that we can become friends in the future!"));
			CanIHelpReaction.AddAction(new SetOffFlagAction(FlagStrings.MusicianFriendsWithPlayer));
			
		}
		public void UpdateWhatKindOfMusic(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allChoiceReactions.Remove(WhatKindOfMusicChoice);
			if(_allChoiceReactions.ContainsKey(WhereDoYouComeFromChoice)){
				_allChoiceReactions.Remove(WhereDoYouComeFromChoice);
			}
			_allChoiceReactions.Add(PlayForMeChoice, new DispositionDependentReaction(PlayForMeReaction));
			_allChoiceReactions.Add(IsThereAnyYouLikeChoice, new DispositionDependentReaction(IsThereAnyYouLikeReaction));
			GUIManager.Instance.RefreshInteraction();
			//GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I love all kinds of music!");
		}
		public void UpdatePlayForMe(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(PlayForMeChoice);
			_allChoiceReactions.Remove(IsThereAnyYouLikeChoice);
			if(hasTalkedAboutTrip == false){
				_allChoiceReactions.Add(WhereDoYouComeFromChoice, new DispositionDependentReaction(WhereDoYouComeFromReaction));	
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I will play for you when I'm not tired from moving in.");
			hasTalkedAboutMusic = true;
			//END STATE FOR MUSIC TALK
		}
		public void UpdateIsThereAnyYouLike(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(PlayForMeChoice);
			_allChoiceReactions.Remove(IsThereAnyYouLikeChoice);
			if(hasTalkedAboutTrip == false){
				_allChoiceReactions.Add(WhereDoYouComeFromChoice, new DispositionDependentReaction(WhereDoYouComeFromReaction));	
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I really wish I had my old flute with me.");
			hasTalkedAboutMusic = true;
			//END STATE FOR MUSIC TALK
		}
		public void UpdateWhereDoYouComeFrom(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(WhereDoYouComeFromChoice);
			if(_allChoiceReactions.ContainsKey(WhatKindOfMusicChoice)){
				_allChoiceReactions.Remove(WhatKindOfMusicChoice);
			}
			_allChoiceReactions.Add(WhyDidYouComeChoice, new DispositionDependentReaction(WhyDidYouComeReaction));
			_allChoiceReactions.Add(WhatWasTheTripLikeChoice, new DispositionDependentReaction(WhatWasTheTripLikeReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'm glad I moved here from the mainland.");
		}
		public void UpdateWhyDidYouCome(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Remove(WhyDidYouComeChoice);
			_allChoiceReactions.Remove(WhatWasTheTripLikeChoice);
			_allChoiceReactions.Add (WasYourSonCloseToHisFatherChoice, new DispositionDependentReaction(WasYourSonCloseToHisFatherReaction));
			_allChoiceReactions.Add (HowAreYouDoingChoice, new DispositionDependentReaction(HowAreYouDoingReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I think its best that we moved away from the bad memories.");
		}
		public void UpdateWhatWasTheTripLike(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allChoiceReactions.Remove(WhatWasTheTripLikeChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("The trip wasn't too bad.");
		}
		public void UpdateWasYourSonCloseToHisFatherReaction(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Remove(WasYourSonCloseToHisFatherChoice);
			_allChoiceReactions.Remove(HowAreYouDoingChoice);
			_allChoiceReactions.Add(CanIHelpChoice, new DispositionDependentReaction(CanIHelpReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Hopefully my son will be okay.");
		}
		public void UpdateHowAreYouDoing(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(WasYourSonCloseToHisFatherChoice);
			_allChoiceReactions.Remove(HowAreYouDoingChoice);
			_allChoiceReactions.Add(CanIHelpChoice, new DispositionDependentReaction(CanIHelpReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I miss my husband.");
		}
		public void UpdateCanIHelp(){
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			_allChoiceReactions.Remove(CanIHelpChoice);
			GUIManager.Instance.RefreshInteraction();
			hasTalkedAboutTrip = true;
			if(hasTalkedAboutMusic == false){
				_allChoiceReactions.Add(WhatKindOfMusicChoice, new DispositionDependentReaction(WhatKindOfMusicReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I hope to see you around in the coming years!");
			//END STATE FOR MOVING TALK
		}
	}
	#endregion
	#endregion
}
