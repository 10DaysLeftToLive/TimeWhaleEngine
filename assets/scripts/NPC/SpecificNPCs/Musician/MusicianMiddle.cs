using UnityEngine;
using System.Collections;

/// <summary>
/// MusicianMiddle specific scripting values
/// The Musician in middle starts out sad and unfriendly if you do not choose to interact with her in young age.
/// If you choose to do, she enters a neutral state in which the player has an easier time making her happy
/// so that she can play musical instruments.  Her reactions are based on the mood of her son, the castleman.
/// </summary>
public class MusicianMiddle : NPC {
	
	Vector3 originalLocation; //Original location if the player actions change the mood of the musician in the past.
	
	protected override void Init() {
		id = NPCIDs.MUSICIAN;
		base.Init();
		originalLocation = transform.position;
	}
	
	#region Emotion States Initialization
	
	DispleasedWithSon unhappyMusician = null;
	HappyForCastleMan happyMusician = null;
	NeturalTowardsPlayer indroducedToHappyMusician = null;
	#endregion
	
	ScheduleLoop runAway, wanderAndPlayMusic;
	Schedule playMusicAtSunset;
	
	protected override void SetFlagReactions() {
		Reaction neutralTowardsPlayer = new Reaction();
		neutralTowardsPlayer.AddAction(new NPCEmotionUpdateAction(this, new NeturalTowardsPlayer(this, "Hi, it has been a long time since we last talked.")));
		neutralTowardsPlayer.AddAction(new NPCCallbackAction(MoveToOriginalLoc));
		neutralTowardsPlayer.AddAction(new NPCCallbackAction(UpdateMoodToNeutral));
		flagReactions.Add(FlagStrings.MusicianFriendsWithPlayer, neutralTowardsPlayer);
		
		Reaction becomesHappyBecauseSonNotCrazy = new Reaction();
		becomesHappyBecauseSonNotCrazy.AddAction(new NPCEmotionUpdateAction(this, 
			new DispleasedWithSon(this, "I am very disapointed by my son's actions, would you be a dear and make sure he stays out of trouble?")));
		flagReactions.Add(FlagStrings.CSONAndCastleFriends, becomesHappyBecauseSonNotCrazy);
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends, becomesHappyBecauseSonNotCrazy);
		flagReactions.Add(FlagStrings.BeachAfterConvoGood, becomesHappyBecauseSonNotCrazy);
		
		Reaction runAwayFromPlayer = new Reaction();
		runAwayFromPlayer.AddAction(new NPCAddScheduleAction(this, runAway));
		flagReactions.Add("Scared By Player", runAwayFromPlayer);
		
		Reaction roamAroundPlayingMusic = new Reaction();
		
		Reaction makePlayerGoAway = new Reaction();
		ShowMultipartChatAction multiChat = new ShowMultipartChatAction(this);
		multiChat.AddChat("Leave me alone.", 0.2f);
		multiChat.AddChat("Don't you have any friends? Go Away.", 0.5f);
		makePlayerGoAway.AddAction(multiChat);
		flagReactions.Add(FlagStrings.MusicianYellAtPlayer, makePlayerGoAway);
	}
	
	protected override EmotionState GetInitEmotionState(){
		unhappyMusician = new DispleasedWithSon(this, "I am very disapointed by my son's actions, would you be a dear and make sure he stays out of trouble?");
		happyMusician = new HappyForCastleMan(this, "Have had a chance to talk to my son recently, he is so friendly now!");
		return unhappyMusician;
	}
	
	#region Schedules Initialization
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	protected override void SetUpSchedules() {
		
		Task detectPlayer = new Task(new WaitTillPlayerCloseState(this, ref player));
		
		TimeTask moveToForest = new TimeTask(50f, new MoveState(this,  MapLocations.MiddleOfHauntedForestMiddle));
		TimeTask moveToBridge = new TimeTask(50f, new MoveState(this, MapLocations.BridgeMiddle));
		TimeTask moveToLightHouse = new TimeTask(50f, new MoveState(this, MapLocations.LightHouseMiddle));
		TimeTask moveToReflectionTree = new TimeTask(50f, new MoveState(this, MapLocations.ReflectionTreeMiddle));
		TimeTask moveToCliff = new TimeTask(50f, new MoveState(this, MapLocations.TopOfFirstFloorStairsRightMiddle));
		TimeTask moveToWindmill =  new TimeTask(50f, new MoveState(this, MapLocations.WindmillMiddle));

		runAway = new ScheduleLoop(this);
		runAway.Add (moveToForest);
		runAway.Add (detectPlayer);
		runAway.Add (moveToBridge);
		runAway.Add (detectPlayer);
		runAway.Add (moveToLightHouse);
		runAway.Add (detectPlayer);
		runAway.Add (moveToReflectionTree);

		runAway.Add (detectPlayer);
		runAway.Add (moveToCliff);
		runAway.Add (detectPlayer);
		runAway.Add (moveToWindmill);
		
		wanderAndPlayMusic = new ScheduleLoop(this);
		wanderAndPlayMusic.Add (moveToForest);
		wanderAndPlayMusic.Add (moveToBridge);
		wanderAndPlayMusic.Add (moveToLightHouse);
		wanderAndPlayMusic.Add (moveToReflectionTree);
		wanderAndPlayMusic.Add (moveToCliff);
		wanderAndPlayMusic.Add (moveToWindmill);
	}
	
	
	#endregion
	
	
	#region EmotionStates
	
	//Musician is displeased with son but the son has not gone crazy yet 
	private class DispleasedWithSon : EmotionState {
		const int FREAKOUTBYPLAYER_MAX = 2;
		int freakedOutByPlayer;
		
		Choice reassureMusician = new Choice("Sure, I will look after him.", "Thanks!  I appreciate your consideration.");
		Choice distressMusician = new Choice("Watch over him yourself.", "How Rude!  how can you even say that?");
		Choice ambivalent = new Choice("...", "Hello..? Do you speak English?");
		
		Choice makeFunOfMusician = new Choice("You are his mother, take responsibility", "He is a grown man and should not intrude on his personal affairs.");
		Choice apologizeToMusician = new Choice("I'm sorry if I sounded mean", "It's ok, I am just concerned about his behavior.");
		Choice bitchPlease = new Choice("Go away", "With Pleasure.");
		
		//Choice questionCastleManBehavior = new Choice("What's wrong with him?", "He seems
		
		Reaction reassuredByPlayer = new Reaction();
		Reaction shunsPlayer = new Reaction();
			Reaction insultedByPlayer = new Reaction();
			Reaction apologizesToPlayer = new Reaction();
			Reaction pissOffMusician = new Reaction();
		Reaction sayNothing = new Reaction();
		
		Reaction playUnwantedInstrument = new Reaction();
		
		public DispleasedWithSon(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			toControl.SetCharacterPortrait(StringsNPC.Sad);
			toControl.ChangeFacialExpression(StringsNPC.Sad);
			playUnwantedInstrument.AddAction(new NPCCallbackAction(ClearAllChoices));
			playUnwantedInstrument.AddAction(new UpdateCurrentTextAction(toControl, "Not now, maybe later."));

			LikesThePlayerReactions(toControl);
			HatesThePlayerReactions(toControl);
			
			sayNothing.AddAction(new NPCCallbackAction(ActAsAMime));
			_allChoiceReactions.Add (ambivalent, new DispositionDependentReaction(sayNothing));
			
			_allItemReactions.Add(StringsItem.Harp, new DispositionDependentReaction(playUnwantedInstrument)); 
			_allItemReactions.Add(StringsItem.Flute, new DispositionDependentReaction(playUnwantedInstrument)); 
		
		}
				
		void LikesThePlayerReactions(NPC toControl) {
			DispositionDependentReaction reassuredByPlayerEffect = new DispositionDependentReaction(reassuredByPlayer);
			reassuredByPlayer.AddAction(new UpdateDefaultTextAction(toControl, "I hope my son gets married, I would be very greatful if he did."));
			reassuredByPlayer.AddAction(new NPCCallbackAction(BecomeNetural));
			reassuredByPlayer.AddAction(new NPCCallbackAction(ClearAllChoices));
			
			_allChoiceReactions.Add(reassureMusician, new DispositionDependentReaction(reassuredByPlayer));
		}
			
		void HatesThePlayerReactions(NPC toControl) {
				DispositionDependentReaction shunsPlayerEffect = new DispositionDependentReaction(shunsPlayer);
				DispositionDependentReaction insultedByPlayerEffect = new DispositionDependentReaction(insultedByPlayer);
				DispositionDependentReaction apologizeToPlayerEffect = new DispositionDependentReaction(apologizesToPlayer);
				
				shunsPlayer.AddAction(new UpdateDefaultTextAction(toControl, "Oh.. why hello there!"));
				shunsPlayer.AddAction(new NPCCallbackAction(AngeredByPlayer));
			
				insultedByPlayer.AddAction(new SetOffFlagAction("Scared By Player"));
				insultedByPlayer.AddAction(new UpdateDefaultTextAction(toControl, "GoodBye."));
				insultedByPlayer.AddAction(new NPCCallbackAction(ClearAllChoices));
				
				
				pissOffMusician.AddAction(new SetOffFlagAction("Scared By Player"));
				pissOffMusician.AddAction(new UpdateDefaultTextAction(toControl, "I have no business with you."));
				pissOffMusician.AddAction(new NPCCallbackAction(ClearAllChoices));
				
				apologizesToPlayer.AddAction(new NPCCallbackAction(BecomeNetural));
				apologizesToPlayer.AddAction(new NPCCallbackAction(ClearAllChoices));
				_allChoiceReactions.Add(distressMusician, shunsPlayerEffect);
		}
				
		void ClearAllChoices() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
			
		void UpdateGUI() {
			GUIManager.Instance.RefreshInteraction();
		}
		
		void ActAsAMime() {
			ClearAllChoices();
			_allChoiceReactions.Add(reassureMusician, new DispositionDependentReaction(reassuredByPlayer));
			_allChoiceReactions.Add(distressMusician, new DispositionDependentReaction(shunsPlayer));
			_allChoiceReactions.Add (ambivalent, new DispositionDependentReaction(sayNothing));
			if (freakedOutByPlayer > FREAKOUTBYPLAYER_MAX) {
				
				switch (freakedOutByPlayer) {
				case 0:
					ambivalent._reactionDialog = "Hi...?";
					break;
				case 1:
					ambivalent._reactionDialog = "Are you ok there?";
					break;
				case 2:
					ambivalent._reactionDialog = "Get away from me!";
					break;
				}
			}
			freakedOutByPlayer++;
		}
		
		void AngeredByPlayer() {
			_allChoiceReactions.Clear();
			DispositionDependentReaction insultedByPlayerEffect = new DispositionDependentReaction(insultedByPlayer);
			DispositionDependentReaction apologizeToPlayerEffect = new DispositionDependentReaction(apologizesToPlayer);
			_allChoiceReactions.Add(makeFunOfMusician, insultedByPlayerEffect);
			_allChoiceReactions.Add(apologizeToMusician, apologizeToPlayerEffect);
			_allChoiceReactions.Add(bitchPlease, new DispositionDependentReaction(pissOffMusician));
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_npcInState.ChangeFacialExpression(StringsNPC.Angry);
			GUIManager.Instance.RefreshInteraction();
		}
		
		void BecomeNetural() {
			_npcInState.SetCharacterPortrait(StringsNPC.Smile);
		}
		
		public override void UpdateEmotionState() {
			
		}
	}
	
	private class HappyForCastleMan : EmotionState {
		
		Choice askAboutSon = new Choice("How is your son doing?", "He is doing great!  He is right over there, you should go talk to him.");
		Choice playMusicialInstrument = new Choice("Could you play me a tune?", "That would lovely, if only I had something to play with.");
		
		Reaction curiousAboutSon = new Reaction();
		Reaction playMusic = new Reaction();
		Reaction askToPlayMusic = new Reaction();
		
		public HappyForCastleMan(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			toControl.SetCharacterPortrait(StringsNPC.Smile);
			toControl.ChangeFacialExpression(StringsNPC.Happy);
			
			curiousAboutSon.AddAction(new NPCCallbackAction(AskAboutSon));
			
			askToPlayMusic.AddAction(new UpdateDefaultTextAction(toControl, "I always wanted to play something compliments the seasons."));
			askToPlayMusic.AddAction(new NPCCallbackAction(ClearAllChoices));
			
			playMusic.AddAction(new NPCTakeItemAction(toControl));
			playMusic.AddAction(new UpdateCurrentTextAction(toControl, "This is perfect! I will be playing melodies around town; please, feel free to follow me."));
			playMusic.AddAction(new UpdateDefaultTextAction(toControl, "There is nothing better than serenading under the sunlight"));
			//Add schedule action here
			
			_allChoiceReactions.Add(askAboutSon, new DispositionDependentReaction(curiousAboutSon));
			_allChoiceReactions.Add(playMusicialInstrument, new DispositionDependentReaction(askToPlayMusic));
			
			_allItemReactions.Add(StringsItem.Harp, new DispositionDependentReaction(playMusic));
			_allItemReactions.Add(StringsItem.Flute, new DispositionDependentReaction(playMusic));
		}
				
		void ClearAllChoices() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
		
		void AskAboutSon() {
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
			_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			ClearAllChoices();
			_npcInState.SetCharacterPortrait(StringsNPC.Smile);
			_npcInState.ChangeFacialExpression(StringsNPC.Smile);
			_allChoiceReactions.Add(askAboutSon, new DispositionDependentReaction(curiousAboutSon));
		}
		
		public override void UpdateEmotionState(){
			
		}
	}
	
	private class NeturalTowardsPlayer : EmotionState {
		
		Choice askAboutSon = new Choice("How is your son doing?", "He has felt down ever since the farmer family moved away.");
			//Choice friendsWithMe = new Choice("
		Choice askAboutFamily = new Choice("How have you enjoyed living here so far?", "It's been great, a lot better than my old country.");
		Choice askAboutSonInsanity = new Choice("Your son has been acting strangely", "Yeah... he hasn't made a single friend yet after all these years.");
		
		Reaction respondToPlayerCurosity = new Reaction();
		Reaction respondToFamilyQuestion = new Reaction();
		Reaction respondToSonInsanity = new Reaction();
		
		public NeturalTowardsPlayer(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_allChoiceReactions.Add(askAboutSon, new DispositionDependentReaction(respondToPlayerCurosity));
			_allChoiceReactions.Add(askAboutFamily, new DispositionDependentReaction(respondToFamilyQuestion));
			_allChoiceReactions.Add(askAboutSonInsanity, new DispositionDependentReaction(respondToSonInsanity));
			
		}
				
		void ClearAllChoices() {
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
		}
		
		public override void UpdateEmotionState(){
			
		}
	}

	#endregion
		
	void MoveToOriginalLoc() {
		originalLocation.y = originalLocation.y + LevelManager.levelYOffSetFromCenter;
		transform.position = originalLocation;
	}
	
	void UpdateMoodToNeutral() {
		ChangeFacialExpression(StringsNPC.Default);
		SetCharacterPortrait(StringsNPC.Default);
		
	}
}
