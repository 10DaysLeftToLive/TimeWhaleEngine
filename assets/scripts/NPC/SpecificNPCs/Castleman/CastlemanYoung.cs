using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanYoung specific scripting values
/// </summary>
public class CastlemanYoung : NPC {
	MeetFamily BecomingFriends;
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		SetCharacterPortrait(StringsNPC.Sad);
		base.Init();
	}
	bool talkedToLighthouse = false;
	bool talkedToCSON = false;
	bool friends = false;
	bool prepared = false;
	bool goingDownToBeach = false;
	public string animationOnApproach = "Timid";
	int NumberAtBeach = 0;
	Schedule CastleManTalksFirstFriends;
	Schedule CastleManTalksFirstNOTFriends;
	Schedule CastleManFollowSchedule;
	Schedule SetFinishedTalkingFlagForSecondConvoFriends;
	Schedule SetFinishedTalkingFlagForThirdConvoFriends;
	Schedule SetFinishedTalkingFlagForSecondConvoNOTFriends;
	Schedule SetFinishedTalkingFlagForThirdConvoNOTFriends;
	Schedule CastlemanWalkToBeachSchedule;
	Schedule CastleManFollowScheduleVTwo;
	NPCConvoSchedule CastleManMeetsLighthouse;
	NPCConvoSchedule CastleManTalksToCSON;
	NPCConvoSchedule CastleManTalksToCSONTwice;
	NPCConvoSchedule CastleManTalksToCSONThrice;
	NPCConvoSchedule CastleManTalksToLighthouseOnBeachFriends;
	NPCConvoSchedule CastleManTalksToLighthouseOnBeachNOTFriends;
	
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
	protected void setEmbarrased(){
		animationData.Play("Embarrassed");
		this.SetCharacterPortrait(StringsNPC.Embarrassed);
	}
	protected void setHappy(){
		this.SetCharacterPortrait(StringsNPC.Happy);	
	}
	protected override void SetFlagReactions(){
		/*Reaction testOne = new Reaction();
		testOne.AddAction(new NPCAddScheduleAction(this, CastleManFollowSchedule));
		flagReactions.Add(FlagStrings.MusicianFinishedTalkingFriends, testOne);
		
		Reaction testTwo = new Reaction();
		testTwo.AddAction(new NPCAddScheduleAction(this, CastleManFollowSchedule));
		flagReactions.Add(FlagStrings.MusicianFinishedTalkingNOTFriends, testTwo);*/
		
		
		Reaction ChangeToTalkingState = new Reaction();
		ChangeToTalkingState.AddAction(new NPCEmotionUpdateAction(this, new MeetFamily(this, "")));
		//ChangeToTalkingState.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.MoveToMusician, ChangeToTalkingState);
		
		#region Asfriends
		//Schedule to start the castleman following the player when he is friends
		Reaction FriendsWithPlayer = new Reaction ();
		FriendsWithPlayer.AddAction(new NPCAddScheduleAction(this, CastleManFollowSchedule));
		FriendsWithPlayer.AddAction(new NPCEmotionUpdateAction(this, new CastleManTraveling(this, "")));
		FriendsWithPlayer.AddAction(new NPCCallbackAction(setFriends));
		FriendsWithPlayer.AddAction(new NPCCallbackAction(setHappy));
		flagReactions.Add(FlagStrings.MusicianFinishedTalkingFriends, FriendsWithPlayer);
		
		//Schedule to start talking to carpenter as friends
		Reaction IntroConversationCarpenterSonFriend = new Reaction ();
		IntroConversationCarpenterSonFriend.AddAction(new NPCAddScheduleAction(this, CastleManTalksFirstFriends));
		IntroConversationCarpenterSonFriend.AddAction(new NPCAddScheduleAction(this, CastleManTalksToCSON));
		IntroConversationCarpenterSonFriend.AddAction(new NPCCallbackAction(setSad));
		flagReactions.Add(FlagStrings.InitialConversationWithCSONFriend, IntroConversationCarpenterSonFriend);
		//Sets up the new emotion after the first conversation
		//
		Reaction AfterIntroConversationCarpenterSon = new Reaction();
		AfterIntroConversationCarpenterSon.AddAction(new NPCAddScheduleAction(this, CastleManFollowScheduleVTwo));
		AfterIntroConversationCarpenterSon.AddAction(new NPCEmotionUpdateAction(this, new VisitCarpenterSonAsFriend(this, "")));
		AfterIntroConversationCarpenterSon.AddAction(new NPCCallbackAction(setSad));
		flagReactions.Add(FlagStrings.FinishedInitialConversationWithCSONFriend, AfterIntroConversationCarpenterSon);
		//Sets up the second conversation
		Reaction StartConversationTwoFriends = new Reaction ();
		StartConversationTwoFriends.AddAction(new NPCAddScheduleAction(this, SetFinishedTalkingFlagForSecondConvoFriends));
		StartConversationTwoFriends.AddAction(new NPCAddScheduleAction(this, CastleManTalksToCSONTwice));
		StartConversationTwoFriends.AddAction(new NPCCallbackAction(setSad));
		flagReactions.Add(FlagStrings.SecondConversationWithCSONFriend, StartConversationTwoFriends);
		//Sets up the emotion after the second conversation
		//
		Reaction AfterSecondConversationCarpenterSon = new Reaction ();
		AfterSecondConversationCarpenterSon.AddAction(new NPCAddScheduleAction(this, CastleManFollowScheduleVTwo));
		AfterSecondConversationCarpenterSon.AddAction(new NPCEmotionUpdateAction(this, new TalkWithCarpenterSonAsFriendRoundTwo(this, "")));
		AfterSecondConversationCarpenterSon.AddAction(new NPCCallbackAction(setSad));
		flagReactions.Add(FlagStrings.FinishedSecondConversationWithCSONFriend, AfterSecondConversationCarpenterSon);
		
		//Sets up the third conversation
		Reaction StartConversationThreeFriends = new Reaction ();
		StartConversationThreeFriends.AddAction(new NPCAddScheduleAction(this, SetFinishedTalkingFlagForThirdConvoFriends));
		StartConversationThreeFriends.AddAction(new NPCAddScheduleAction(this, CastleManTalksToCSONThrice));
		StartConversationThreeFriends.AddAction(new NPCCallbackAction(testStartGoingToBeachAfterCarpenterSonTalk));
		StartConversationThreeFriends.AddAction(new NPCCallbackAction(setSad));
		flagReactions.Add(FlagStrings.ThirdConvoWithCSONFriend, StartConversationThreeFriends);
		//Moves the Castleman to the beach
		//
		Reaction ReadyForBeachFriends = new Reaction ();
		ReadyForBeachFriends.AddAction(new NPCEmotionUpdateAction(this, new WaitingAtBeachFriend(this, "")));
		ReadyForBeachFriends.AddAction(new NPCAddScheduleAction(this, CastlemanWalkToBeachSchedule));
		ReadyForBeachFriends.AddAction(new NPCCallbackAction(setEmbarrased));
		ReadyForBeachFriends.AddAction(new NPCCallbackAction(setGoingToBeach));
		flagReactions.Add(FlagStrings.BeachBeforeConvoFriendsString, ReadyForBeachFriends);
		#endregion
		
		#region NOT Friends with Castleman
		//Schedule to start the Castleman following the player When not friends.
		Reaction NOTFriendsWithPlayer = new Reaction ();
		NOTFriendsWithPlayer.AddAction(new NPCCallbackAction(testFunction));
		NOTFriendsWithPlayer.AddAction(new NPCEmotionUpdateAction(this, new CastleManTraveling(this, "")));
		NOTFriendsWithPlayer.AddAction(new NPCAddScheduleAction(this, CastleManFollowSchedule));
		NOTFriendsWithPlayer.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.MusicianFinishedTalkingNOTFriends, NOTFriendsWithPlayer);
		//Schedule for first convo with carpenter not friends
		Reaction IntroConversationCarpenterSonNOTFriend = new Reaction ();
		IntroConversationCarpenterSonNOTFriend.AddAction(new NPCAddScheduleAction(this, CastleManTalksFirstNOTFriends));
		IntroConversationCarpenterSonNOTFriend.AddAction(new NPCAddScheduleAction(this, CastleManTalksToCSON));
		IntroConversationCarpenterSonNOTFriend.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.InitialConversationWithCSONNOTFriend, IntroConversationCarpenterSonNOTFriend);
		//Schedule for the second emotion state
		Reaction AfterIntroConversationNOTFriendsCarpenterSon = new Reaction();
		AfterIntroConversationNOTFriendsCarpenterSon.AddAction(new NPCAddScheduleAction(this, CastleManFollowScheduleVTwo));
		AfterIntroConversationNOTFriendsCarpenterSon.AddAction(new NPCEmotionUpdateAction(this, new VisitCarpenterSonNotAsFriend(this, "")));
		AfterIntroConversationNOTFriendsCarpenterSon.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.FinishedInitialConversationWithCSONNOTFriend, AfterIntroConversationNOTFriendsCarpenterSon);
		//Schedule for the second conversation
		Reaction StartConversationTwoNOTFriends = new Reaction ();
		StartConversationTwoNOTFriends.AddAction(new NPCAddScheduleAction(this, SetFinishedTalkingFlagForSecondConvoNOTFriends));
		StartConversationTwoNOTFriends.AddAction(new NPCAddScheduleAction(this, CastleManTalksToCSONTwice));
		StartConversationTwoNOTFriends.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.SecondConversationWithCSONNOTFriend, StartConversationTwoNOTFriends);
		//Schedule for setting up the third emotion state
		Reaction AfterSecondConversationNOTFriendsCarpenterSon = new Reaction ();
		AfterSecondConversationNOTFriendsCarpenterSon.AddAction(new NPCAddScheduleAction(this, CastleManFollowScheduleVTwo));
		AfterSecondConversationNOTFriendsCarpenterSon.AddAction(new NPCEmotionUpdateAction(this, new TalkWithCarpenterSonNotAsFriendRoundTwo(this, "")));
		AfterSecondConversationNOTFriendsCarpenterSon.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.FinishedSecondConversationWithCSONNOTFriend, AfterSecondConversationNOTFriendsCarpenterSon);
		//Schedule for Starting the third conversation
		Reaction StartConversationThreeNOTFriends = new Reaction ();
		StartConversationThreeNOTFriends.AddAction(new NPCAddScheduleAction(this, SetFinishedTalkingFlagForThirdConvoNOTFriends));
		StartConversationThreeNOTFriends.AddAction(new NPCAddScheduleAction(this, CastleManTalksToCSONThrice));
		StartConversationThreeNOTFriends.AddAction(new NPCCallbackAction(testStartGoingToBeachAfterCarpenterSonTalk));
		StartConversationThreeNOTFriends.AddAction(new NPCCallbackAction(setAngry));
		flagReactions.Add(FlagStrings.ThirdConvoWithCSONNOTFriend, StartConversationThreeNOTFriends);
		
		Reaction ReadyForBeachNOTAsFriends = new Reaction ();
		ReadyForBeachNOTAsFriends.AddAction(new NPCEmotionUpdateAction(this, new WaitingAtBeachNotAsFriend(this, "")));
		ReadyForBeachNOTAsFriends.AddAction(new NPCCallbackAction(setEmbarrased));
		ReadyForBeachNOTAsFriends.AddAction(new NPCAddScheduleAction(this, CastlemanWalkToBeachSchedule));
		ReadyForBeachNOTAsFriends.AddAction(new NPCCallbackAction(setGoingToBeach));
		flagReactions.Add(FlagStrings.BeachBeforeConvoNotFriendsString, ReadyForBeachNOTAsFriends);
		#endregion
		Reaction TriggerBeach = new Reaction();
		TriggerBeach.AddAction(new NPCCallbackAction(TimerAndBeach));
		TriggerBeach.AddAction(new NPCEmotionUpdateAction(this, new EmptyEmotion(this, "Let's keep going.  Everyone hates me.")));
		flagReactions.Add(FlagStrings.TimerForGoingToBeach, TriggerBeach);
		
		
		Reaction FinishedTalkingWithCSON = new Reaction ();
		FinishedTalkingWithCSON.AddAction(new NPCCallbackAction(testStartGoingToBeachAfterCarpenterSonTalk));
		flagReactions.Add(FlagStrings.FinishedCSONConversation, FinishedTalkingWithCSON);
		
		Reaction CheckLighthouseAtBeach = new Reaction();
		CheckLighthouseAtBeach.AddAction(new NPCCallbackAction(checkNumberAtBeach));
		flagReactions.Add(FlagStrings.LighthouseAtBeach, CheckLighthouseAtBeach);
		
		Reaction CheckCastleAtBeach = new Reaction();
		CheckCastleAtBeach.AddAction(new NPCCallbackAction(checkNumberAtBeach));
		flagReactions.Add(FlagStrings.CastleManAtBeach, CheckCastleAtBeach);
		
		Reaction SetPrepared = new Reaction();
		SetPrepared.AddAction(new NPCCallbackAction(setPrepare));
		flagReactions.Add(FlagStrings.PreparedForConversationWithLighthouse, SetPrepared);
		//This is to talk with the lighthouse girl for the first time.
		Reaction TalkWithLighthouseFirstTime = new Reaction();
		TalkWithLighthouseFirstTime.AddAction(new NPCAddScheduleAction(this, CastleManMeetsLighthouse));
		TalkWithLighthouseFirstTime.AddAction(new NPCEmotionUpdateAction(this, new AfterLighthouse(this, "")));
		TalkWithLighthouseFirstTime.AddAction(new NPCCallbackAction(testStartGoingtoBeachAfterLighthouseTalk));
		TalkWithLighthouseFirstTime.AddAction(new NPCCallbackAction(setEmbarrased));
		flagReactions.Add(FlagStrings.StartTalkingToLighthouse, TalkWithLighthouseFirstTime);
		
		#region Conversations
		Reaction ConversationPrepared = new Reaction();
		ConversationPrepared.AddAction(new NPCAddScheduleAction(this, CastleManTalksToLighthouseOnBeachFriends));
		flagReactions.Add(FlagStrings.TalkPreparedForConvo, ConversationPrepared);
		
		Reaction ConversationNOTPrepared = new Reaction();
		ConversationNOTPrepared.AddAction(new NPCAddScheduleAction(this, CastleManTalksToLighthouseOnBeachNOTFriends));
		flagReactions.Add(FlagStrings.TalkNotPreparedForConvo, ConversationNOTPrepared);
		
		
		#endregion
		
		
	}
	
	public override void StarTalkingWithPlayer() {
		currentEmotion.OnInteractionOpens();
		chatingWithPlayer = true;
		PassiveChatToPlayer.instance.RemoveNPCChat(this);
		scheduleStack.Pause();
		EnterState(new InteractingWithPlayerState(this, "Timid"));
	}
	
	public void TimerAndBeach(){
		Debug.Log("It made it here!");
		if(goingDownToBeach == false){	
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);	
		}
	}
	public void setGoingToBeach(){
		goingDownToBeach = true;	
	}
	public void setPrepare(){
		prepared = true;	
	}
	public void checkNumberAtBeach(){
		NumberAtBeach++;
		if (NumberAtBeach == 2){
			if(prepared == true){
				FlagManager.instance.SetFlag(FlagStrings.TalkPreparedForConvo);
			}
			else{
				FlagManager.instance.SetFlag(FlagStrings.TalkNotPreparedForConvo);
			}
		}
	}
	public void setFriends(){
		friends = true;	
	}
	public void testStartGoingToBeachAfterCarpenterSonTalk(){
		talkedToCSON = true;
		if(talkedToLighthouse == true){
			if(friends == true){
				FlagManager.instance.SetFlag(FlagStrings.BeachBeforeConvoFriendsString);
			}
			else{
				FlagManager.instance.SetFlag(FlagStrings.BeachBeforeConvoNotFriendsString);
			}
		}
	}
	public void testStartGoingtoBeachAfterLighthouseTalk(){
		talkedToLighthouse = true;
		if(talkedToCSON == true){
			if(friends == true){
				FlagManager.instance.SetFlag(FlagStrings.BeachBeforeConvoFriendsString);
				Debug.Log("What?");
			}
			else{
				FlagManager.instance.SetFlag(FlagStrings.BeachBeforeConvoNotFriendsString);
				Debug.Log("What?");
			}
		}
	}
	public void testFunction(){
		Debug.Log("Test");
	}
	protected override EmotionState GetInitEmotionState(){
		BecomingFriends = new MeetFamily(this, "");
		return (new InitialEmotionState(this, "..."));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		//schedule.Add(new Task(new MoveToObjectState(this, this.gameObject)));
		return (schedule);
	}

	protected override void SetUpSchedules(){
		CastleManFollowSchedule = new Schedule(this, Schedule.priorityEnum.Medium);
		CastleManFollowSchedule.Add(new TimeTask(0f, new IdleState(this)));
		CastleManFollowSchedule.Add(new Task(new FollowObjectState(this, player.gameObject)));
		
		CastleManFollowScheduleVTwo = new Schedule(this, Schedule.priorityEnum.High);
		CastleManFollowScheduleVTwo.Add(new TimeTask(0f, new IdleState(this)));
		CastleManFollowScheduleVTwo.Add(new TimeTask(0f, new FollowObjectState(this, player.gameObject)));
		Task TimerRanOut = new TimeTask(0f, new IdleState(this));
		TimerRanOut.AddFlagToSet(FlagStrings.TimerForGoingToBeach);
		CastleManFollowScheduleVTwo.Add(TimerRanOut);
		
		CastlemanWalkToBeachSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		CastlemanWalkToBeachSchedule.Add(new TimeTask(1f, new IdleState(this)));
		CastlemanWalkToBeachSchedule.Add(new Task(new MoveThenMarkDoneState(this, new Vector3(52f, -6f, 0.95f))));
		Task setAtBeachFlag = new TimeTask(0f, new IdleState(this));
		setAtBeachFlag.AddFlagToSet(FlagStrings.CastleManAtBeach);
		CastlemanWalkToBeachSchedule.Add(setAtBeachFlag);
		CastlemanWalkToBeachSchedule.Add(new TimeTask(10000f, new IdleState(this)));
		
		
		#region Friends
		CastleManTalksFirstFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setFlagFriendsFirstConvo = (new TimeTask(2f, new IdleState(this)));
		setFlagFriendsFirstConvo.AddFlagToSet(FlagStrings.FinishedInitialConversationWithCSONFriend);
		CastleManTalksFirstFriends.Add(setFlagFriendsFirstConvo);
		
		SetFinishedTalkingFlagForSecondConvoFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setFlagFriendsSecondConvo = (new TimeTask(2f, new IdleState(this)));
		setFlagFriendsSecondConvo.AddFlagToSet(FlagStrings.FinishedSecondConversationWithCSONFriend);
		SetFinishedTalkingFlagForSecondConvoFriends.Add(setFlagFriendsSecondConvo);
		
		SetFinishedTalkingFlagForThirdConvoFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setFlagFriendsThirdConvo = (new TimeTask(2f, new IdleState(this)));
		setFlagFriendsThirdConvo.AddFlagToSet(FlagStrings.FinishedThirdConvoWithCSONFriend);
		SetFinishedTalkingFlagForThirdConvoFriends.Add(setFlagFriendsThirdConvo);
		# endregion
		#region NotFriends
		CastleManTalksFirstNOTFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setFlagNOTFriendsFirstConvo = (new TimeTask(2f, new IdleState(this)));
		setFlagNOTFriendsFirstConvo.AddFlagToSet(FlagStrings.FinishedInitialConversationWithCSONNOTFriend);
		CastleManTalksFirstNOTFriends.Add(setFlagNOTFriendsFirstConvo);
		
		SetFinishedTalkingFlagForSecondConvoNOTFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setFlagNOTFriendsSecondConvo = (new TimeTask(2f, new IdleState(this)));
		setFlagNOTFriendsSecondConvo.AddFlagToSet(FlagStrings.FinishedSecondConversationWithCSONNOTFriend);
		SetFinishedTalkingFlagForSecondConvoNOTFriends.Add(setFlagNOTFriendsSecondConvo);
		
		SetFinishedTalkingFlagForThirdConvoNOTFriends = new Schedule(this, Schedule.priorityEnum.DoNow);
		Task setFlagNOTFriendsThirdConvo = (new TimeTask(2f, new IdleState(this)));
		setFlagNOTFriendsThirdConvo.AddFlagToSet(FlagStrings.FinishedThirdConvoWithCSONNOTFriend);
		SetFinishedTalkingFlagForThirdConvoNOTFriends.Add(setFlagNOTFriendsThirdConvo);
		#endregion
		#region Conversations
		CastleManMeetsLighthouse = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
			new CastleManToLighthouseFirstMeeting(),Schedule.priorityEnum.DoConvo);
		CastleManMeetsLighthouse.SetCanNotInteractWithPlayer();
		
		CastleManTalksToCSON = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), 
			new CastleManToCarpenterFirstConvo(),Schedule.priorityEnum.DoConvo);
		CastleManTalksToCSON.SetCanNotInteractWithPlayer();
		CastleManTalksToCSONTwice = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), 
			new CastleManToCarpenterSecondConvo(),Schedule.priorityEnum.DoConvo);
		CastleManTalksToCSONTwice.SetCanNotInteractWithPlayer();
		CastleManTalksToCSONThrice = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.CarpenterSonYoung), 
			new CastleManToCarpenterThirdConvo(),Schedule.priorityEnum.DoConvo);
		CastleManTalksToCSONThrice.SetCanNotInteractWithPlayer();
		
		
		CastleManTalksToLighthouseOnBeachFriends = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
			new CastleManToLightHouseFriends(),Schedule.priorityEnum.DoConvo);
		CastleManTalksToLighthouseOnBeachFriends.SetCanNotInteractWithPlayer();
		CastleManTalksToLighthouseOnBeachNOTFriends = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
			new CastlemanToLighthouseNotFriends(),Schedule.priorityEnum.DoConvo);
		CastleManTalksToLighthouseOnBeachNOTFriends.SetCanNotInteractWithPlayer();
		#endregion
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
			toControl.SetCharacterPortrait(StringsNPC.Sad);
			AppleReaction = new Reaction();
			AppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(AppleReaction));
				
			FishingRodReaction = new Reaction();
			FishingRodReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.FishingRod, new DispositionDependentReaction(FishingRodReaction));
				
			PlushieReaction = new Reaction();
			PlushieReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.TimeWhale, new DispositionDependentReaction(PlushieReaction));
				
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
			_allItemReactions.Add(StringsItem.CaptainLog, new DispositionDependentReaction(CaptainsLogReaction));
				
			ToySwordReaction = new Reaction();
			ToySwordReaction.AddAction(new UpdateCurrentTextAction(toControl, "......"));
			_allItemReactions.Add(StringsItem.ToySword, new DispositionDependentReaction(ToySwordReaction));

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
			//_npcInState.PlayAnimation("Timid");
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateWellTellMe(){
			_allChoiceReactions.Remove(WellTellMeChoice);
			_allChoiceReactions.Remove(FineHaveItYourWayChoice);
			//_npcInState.PlayAnimation("Timid");
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
			//_npcInState.PlayAnimation("Timid");
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateAreYouNew(){
			_allChoiceReactions.Remove(WhatsYourNameChoice);
			_allChoiceReactions.Remove(AreYouNewChoice);
			_allChoiceReactions.Add(FineHaveItYourWayChoice, new DispositionDependentReaction(FineHaveItYourWayReaction));
			_allChoiceReactions.Add(DontYouSpeakChoice, new DispositionDependentReaction(DontYouSpeakReaction));
			GUIManager.Instance.RefreshInteraction();
			//_npcInState.PlayAnimation("Timid");
			(_npcInState as CastlemanYoung).animationOnApproach = Strings.animation_stand;
			FlagManager.instance.SetFlag(FlagStrings.MusicianCommentOnSon);
		}
		public void UpdateWhatsYourName(){
			
			_allChoiceReactions.Remove(WhatsYourNameChoice);
			_allChoiceReactions.Remove(AreYouNewChoice);
			_allChoiceReactions.Add(FineHaveItYourWayChoice, new DispositionDependentReaction(FineHaveItYourWayReaction));
			_allChoiceReactions.Add(DontYouSpeakChoice, new DispositionDependentReaction(DontYouSpeakReaction));
			GUIManager.Instance.CloseInteractionMenu();
			//_npcInState.PlayAnimation("Timid");
			(_npcInState as CastlemanYoung).animationOnApproach = Strings.animation_stand;
			FlagManager.instance.SetFlag(FlagStrings.MusicianCommentOnSon);
			//FlagManager.instance.SetFlag(FlagStrings.PlayerAndCastleNOTFriends);
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
	private class CastleManTraveling : EmotionState{
	
		public CastleManTraveling(NPC toControl, string currentDialogue) : base(toControl, "This is stupid."){
			
		}
	}
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
			ItsBecauseYourStupidReaction.AddAction(new NPCCallbackAction(UpdateItsBecauseYourStupid));
			ItsBecauseYourStupidReaction.AddAction(new UpdateCurrentTextAction(toControl, "I trusted you...I'm not going to make that mistake again..."));
			
			ImReallyReallySorryChoice = new Choice ("I'm really really sorry.", "");
			ImReallyReallySorryReaction = new Reaction();
			ImReallyReallySorryReaction.AddAction(new NPCCallbackAction(UpdateImReallyReallySorry));
			ImReallyReallySorryReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
			
			YouGotMeIWasLyingChoice = new Choice ("You got me!\nI was lying.", "I trusted you...I'm not going to make that mistake again...");
			YouGotMeIWasLyingReaction = new Reaction();
			YouGotMeIWasLyingReaction.AddAction(new NPCCallbackAction(UpdateYouGotMeIWasLying));
			YouGotMeIWasLyingReaction.AddAction(new UpdateCurrentTextAction(toControl, "I trusted you...I'm not going to make that mistake again..."));
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
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I love poetry!");
			FlagManager.instance.SetFlag(FlagStrings.PlayerAndCastleFriends);
			FriendshipTally += 2;
		}
		public void UpdateThatSoundsCool(){
			_allChoiceReactions.Remove(ILikePoetryChoice);
			_allChoiceReactions.Remove(ThatSoundsCoolChoice);
			_allChoiceReactions.Remove(PoetryIsSillyChoice);
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.PlayerAndCastleFriends);
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
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.PlayerAndCastleFriends);
			SetDefaultText("You should apologize!");
		}
		//Ending one here
		public void UpdateItsBecauseYourStupid(){
			_allChoiceReactions.Remove(ImSorryChoice);
			_allChoiceReactions.Remove(ItsBecauseYourStupidChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I hate you!");
			FlagManager.instance.SetFlag(FlagStrings.PlayerAndCastleNOTFriends);
			Debug.Log ("This is where the bug is happening.");
		}
		public void UpdateImReallyReallySorry(){
			_allChoiceReactions.Remove(YouGotMeIWasLyingChoice);
			_allChoiceReactions.Remove(ImReallyReallySorryChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I guess I believe you...");
			FriendshipTally += 1;
		}
		//Ending one here.
		public void UpdateYouGotMeIWasLying(){
			_allChoiceReactions.Remove(YouGotMeIWasLyingChoice);
			_allChoiceReactions.Remove(ImReallyReallySorryChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I hate you!");
			FlagManager.instance.SetFlag(FlagStrings.PlayerAndCastleNOTFriends);
		}
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#region Visit Carpenter Son with you and CastleMan Friends		
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
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(ImSureHeDoesntHateYouChoice);
			_allChoiceReactions.Remove (HeTotallyHatesYouChoice);
			_allChoiceReactions.Add(DontBeSoScaredChoice, new DispositionDependentReaction(DontBeSoScaredReaction));
			_allChoiceReactions.Add(JustTalkAboutFishChoice, new DispositionDependentReaction(JustTalkAboutFishReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I dunno if your right about him not hating me...");
		}
		public void UpdateHeTotallyHatesYou(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(ImSureHeDoesntHateYouChoice);
			_allChoiceReactions.Remove (HeTotallyHatesYouChoice);
			GUIManager.Instance.CloseInteractionMenu();
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
			SetDefaultText("I don't like this stupid island.\nLet's keep going so I can get back soon.");
			
		}
		public void UpdateDontBeSoScared(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(DontBeSoScaredChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I wouldn't be scared if I knew what to say.");
		}
		public void UpdateJustTalkAboutFish(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			if(_allChoiceReactions.ContainsKey(DontBeSoScaredChoice)){
				_allChoiceReactions.Remove(DontBeSoScaredChoice);	
			}
			_allChoiceReactions.Remove(JustTalkAboutFishChoice);
			_allChoiceReactions.Add(NotToHimChoice, new DispositionDependentReaction(NotToHimReaction));
			_allChoiceReactions.Add(GoodPointLetsMoveOnChoice, new DispositionDependentReaction(GoodPointLetsMoveOnReaction));
			_allChoiceReactions.Add(PoetryIsntBoringToYouChoice, new DispositionDependentReaction(PoetryIsntBoringToYouReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I think fishing is stupid.");
		}
		public void UpdateNotToHim(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(NotToHimChoice);
			_allChoiceReactions.Remove(GoodPointLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(PoetryIsntBoringToYouChoice)){
					_allChoiceReactions.Remove(PoetryIsntBoringToYouChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.SecondConversationWithCSONFriend);
			SetDefaultText("I guess I'll just give it another try.");
		}
		public void UpdatePoetryIsntBoring(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(NotToHimChoice);
			_allChoiceReactions.Remove(GoodPointLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(PoetryIsntBoringToYouChoice)){
					_allChoiceReactions.Remove(PoetryIsntBoringToYouChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			//Set Flags here
			FlagManager.instance.SetFlag(FlagStrings.SecondConversationWithCSONFriend);
			SetDefaultText("I guess I'll just give it another try.");
		}
		public void UpdateGoodPointLetsMoveOn(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(NotToHimChoice);
			_allChoiceReactions.Remove(GoodPointLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(PoetryIsntBoringToYouChoice)){
					_allChoiceReactions.Remove(PoetryIsntBoringToYouChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			//Set flags here
			//This should be a different flag
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
			SetDefaultText("Let's go.");
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	private class TalkWithCarpenterSonAsFriendRoundTwo : EmotionState{
		Choice WhatIfYouWereYourselfChoice;
		Reaction WhatIfYouWereYourselfReaction;
		Choice YeahHeProbablyHatesYouChoice;
		Reaction YeahHeProbablyHatesYouReaction;
		Choice IDontHateYouChoice;
		Reaction IDontHateYouReaction;
		Choice MaybeYouJustNeedToTryChoice;
		Reaction MaybeYouJustNeedToTryReaction;
		Choice JustBeYourselfChoice;
		Reaction JustBeYourselfReaction;
		Choice OnSecondThoughtHeHatesYouChoice;
		Reaction OnSecondThoughtHeHatesYouReaction;
		Choice IfImYourFriendThenHeCanBeTooChoice;
		Reaction IfImYourFriendThenHeCanBeTooReaction;
		Choice FineLiveInFearChoice;
		Reaction FineLiveInFearReaction;
		public TalkWithCarpenterSonAsFriendRoundTwo(NPC toControl, string currentDialogue) : base(toControl, "See it didn't work!"){
			WhatIfYouWereYourselfChoice = new Choice ("What if you just were yourself?", "No one ever likes who I am...\nHow could he ever like the real me?");
			WhatIfYouWereYourselfReaction = new Reaction();
			WhatIfYouWereYourselfReaction.AddAction(new NPCCallbackAction(UpdateWhatIfYouWereYourself));
			WhatIfYouWereYourselfReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
			_allChoiceReactions.Add(WhatIfYouWereYourselfChoice, new DispositionDependentReaction(WhatIfYouWereYourselfReaction));
			
			YeahHeProbablyHatesYouChoice =  new Choice ("Yeah he probably hates you.", "Let's go, I'm tired of being here.");
			YeahHeProbablyHatesYouReaction =  new Reaction();
			YeahHeProbablyHatesYouReaction.AddAction(new NPCCallbackAction(UpdateYeahHeProbablyHatesYou));
			YeahHeProbablyHatesYouReaction.AddAction(new ShowOneOffChatAction(toControl, "Let's go, I'm tired of being here."));
			_allChoiceReactions.Add(YeahHeProbablyHatesYouChoice, new DispositionDependentReaction(YeahHeProbablyHatesYouReaction));
			
			IDontHateYouChoice =  new Choice("I don't hate you.", "Yeah?  But he might...");
			IDontHateYouReaction =  new Reaction();
			IDontHateYouReaction.AddAction(new NPCCallbackAction(UpdateIDontHateYou));
			IDontHateYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "Yeah?  But he might..."));
			
			MaybeYouJustNeedToTryChoice =  new Choice("Maybe you just need to try.", "I dunno about this...");
			MaybeYouJustNeedToTryReaction =  new Reaction();
			MaybeYouJustNeedToTryReaction.AddAction(new NPCCallbackAction(UpdateMaybeYouJustNeedToTry));
			MaybeYouJustNeedToTryReaction.AddAction(new UpdateCurrentTextAction(toControl, "I dunno about this..."));
				
			JustBeYourselfChoice = new Choice("Just be yourself.", "All right I'll try...");
			JustBeYourselfReaction = new Reaction();
			JustBeYourselfReaction.AddAction(new NPCCallbackAction(UpdateJustBeYourself));
			JustBeYourselfReaction.AddAction(new ShowOneOffChatAction(toControl, "All right I'll try..."));
			
			OnSecondThoughtHeHatesYouChoice = new Choice("On second thought he hates you.", "I knew it!  Let's get out of here...");
			OnSecondThoughtHeHatesYouReaction = new Reaction();
			OnSecondThoughtHeHatesYouReaction.AddAction(new NPCCallbackAction(UpdateOnSecondThoughtHeHatesYou));
			OnSecondThoughtHeHatesYouReaction.AddAction(new ShowOneOffChatAction(toControl, "I knew it!  Let's get out of here..."));
			
			IfImYourFriendThenHeCanBeTooChoice = new Choice("If I'm your friend, then he can be too.", "I guess so...\n I should at least make an effort.");
			IfImYourFriendThenHeCanBeTooReaction = new Reaction();
			IfImYourFriendThenHeCanBeTooReaction.AddAction(new NPCCallbackAction(UpdateIfImYourFriendThenHeCanBeToo));
			IfImYourFriendThenHeCanBeTooReaction.AddAction(new ShowOneOffChatAction(toControl, "I guess so...\nI should at least make an effort."));
			
			FineLiveInFearChoice =  new Choice ("Fine live in fear!", "I will!  Let's get outta here!");
			FineLiveInFearReaction = new Reaction();
			FineLiveInFearReaction.AddAction(new NPCCallbackAction(UpdateFineLiveInFear));
			FineLiveInFearReaction.AddAction(new ShowOneOffChatAction(toControl, "I will!  Let's get outta here!"));
		}
		public void UpdateWhatIfYouWereYourself(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(WhatIfYouWereYourselfChoice);
			_allChoiceReactions.Remove(YeahHeProbablyHatesYouChoice);
			_allChoiceReactions.Add(MaybeYouJustNeedToTryChoice, new DispositionDependentReaction(MaybeYouJustNeedToTryReaction));
			_allChoiceReactions.Add(IDontHateYouChoice, new DispositionDependentReaction(IDontHateYouReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("No one likes who I am.");
		}
		public void UpdateYeahHeProbablyHatesYou(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(YeahHeProbablyHatesYouChoice);
			_allChoiceReactions.Remove(WhatIfYouWereYourselfChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Everyone hates me!");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
		}
		public void UpdateIDontHateYou(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(IDontHateYouChoice);
			_allChoiceReactions.Remove(MaybeYouJustNeedToTryChoice);
			_allChoiceReactions.Add(FineLiveInFearChoice, new DispositionDependentReaction(FineLiveInFearReaction));
			_allChoiceReactions.Add(IfImYourFriendThenHeCanBeTooChoice, new DispositionDependentReaction(IfImYourFriendThenHeCanBeTooReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("He might hate me...");
		}
		public void UpdateMaybeYouJustNeedToTry(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(IDontHateYouChoice);
			_allChoiceReactions.Remove(MaybeYouJustNeedToTryChoice);
			_allChoiceReactions.Add(JustBeYourselfChoice, new DispositionDependentReaction(JustBeYourselfReaction));
			_allChoiceReactions.Add(OnSecondThoughtHeHatesYouChoice, new DispositionDependentReaction(OnSecondThoughtHeHatesYouReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I don't think I should talk to him again...");
		}
		public void UpdateJustBeYourself(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(JustBeYourselfChoice);
			_allChoiceReactions.Remove(OnSecondThoughtHeHatesYouChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Wow...I didn't think he would be willing to be friends with me...");
			//Set Flags here
			FlagManager.instance.SetFlag(FlagStrings.ThirdConvoWithCSONFriend);
		}
		public void UpdateOnSecondThoughtHeHatesYou(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(JustBeYourselfChoice);
			_allChoiceReactions.Remove(OnSecondThoughtHeHatesYouChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Everyone I meet doesn't like me...");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
		}
		public void UpdateIfImYourFriendThenHeCanBeToo(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(IfImYourFriendThenHeCanBeTooChoice);
			_allChoiceReactions.Remove(IfImYourFriendThenHeCanBeTooChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Wow...I didn't think he would be willing to be friends with me...");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.ThirdConvoWithCSONFriend);
		}
		public void UpdateFineLiveInFear(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(IfImYourFriendThenHeCanBeTooChoice);
			_allChoiceReactions.Remove(FineLiveInFearChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Everyone I meet doesn't like me...");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
		}
		
	}
	#endregion
	#region Visit Carpenter Son without being friends
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
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(MaybeYouShouldTryTalkingToHimAgainChoice);
			_allChoiceReactions.Remove (PrettyMuchChoice);
			_allChoiceReactions.Add(WhyWouldIWantToTrickYouChoice, new DispositionDependentReaction(WhyWouldIWantToTrickYouReaction));
			_allChoiceReactions.Add(JustTalkAboutFishChoice, new DispositionDependentReaction(JustTalkAboutFishReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Everyone is out to get me!");
		}
		public void UpdatePrettyMuch(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(MaybeYouShouldTryTalkingToHimAgainChoice);
			_allChoiceReactions.Remove (PrettyMuchChoice);
			GUIManager.Instance.CloseInteractionMenu();
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
			SetDefaultText("I hate all of you!");
			
		}
		public void UpdateWhyWouldIWantToTrickYou(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(WhyWouldIWantToTrickYouChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I know for sure you are trying to trick me!");
		}
		public void UpdateJustTalkAboutFish(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			if(_allChoiceReactions.ContainsKey(WhyWouldIWantToTrickYouChoice)){
				_allChoiceReactions.Remove(WhyWouldIWantToTrickYouChoice);	
			}
			_allChoiceReactions.Remove(JustTalkAboutFishChoice);
			_allChoiceReactions.Add(JustTalkToHimChoice, new DispositionDependentReaction(JustTalkToHimReaction));
			_allChoiceReactions.Add(UghFineLetsMoveOnChoice, new DispositionDependentReaction(UghFineLetsMoveOnReaction));
			_allChoiceReactions.Add(JustTrustMeChoice, new DispositionDependentReaction(JustTrustMeReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I think fishing is stupid.");
		}
		public void UpdateJustTalkToHim(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(JustTalkToHimChoice);
			_allChoiceReactions.Remove(UghFineLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(JustTrustMeChoice)){
					_allChoiceReactions.Remove(JustTrustMeChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.SecondConversationWithCSONNOTFriend);
			SetDefaultText("I guess so...");
		}
		public void UpdateJustTrustMe(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(JustTalkToHimChoice);
			_allChoiceReactions.Remove(UghFineLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(JustTrustMeChoice)){
					_allChoiceReactions.Remove(JustTrustMeChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			//Setflags
			FlagManager.instance.SetFlag(FlagStrings.SecondConversationWithCSONNOTFriend);
			SetDefaultText("Fine!  But if this is a trick...");
		}
		public void UpdateUghFineLetsMoveOn(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(JustTalkToHimChoice);
			_allChoiceReactions.Remove(UghFineLetsMoveOnChoice);
			if (_allChoiceReactions.ContainsKey(JustTrustMeChoice)){
					_allChoiceReactions.Remove(JustTrustMeChoice);
			}
			GUIManager.Instance.CloseInteractionMenu();
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
			SetDefaultText("Let's go.");
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	private class TalkWithCarpenterSonNotAsFriendRoundTwo : EmotionState{
		Choice StopBeingParanoidChoice;
		Reaction StopBeingParanoidReaction;
		Choice YupPrettyMuchChoice;
		Reaction YupPrettyMuchReaction;
		Choice LookJustActNaturalChoice;
		Reaction LookJustActNaturalReaction;
		Choice JustTryBeingYourselfChoice;
		Reaction JustTryBeingYourselfReaction;
		Choice HeWontTrustMeAlreadyChoice;
		Reaction HeWontTrustMeAlreadyReaction;
		Choice FairEnoughChoice;
		Reaction FairEnoughReaction;
		Choice BeYourselfChoice;
		Reaction BeYourselfReaction;
		Choice IveHadEnoughChoice;
		Reaction IveHadEnoughReaction;
		public TalkWithCarpenterSonNotAsFriendRoundTwo(NPC toControl, string currentDialogue) : base(toControl, "See it didn't work!"){
			StopBeingParanoidChoice = new Choice ("Stop being paranoid!", "What you said didn't work though!");
			StopBeingParanoidReaction = new Reaction();
			StopBeingParanoidReaction.AddAction(new NPCCallbackAction(UpdateStopBeingParanoid));
			StopBeingParanoidReaction.AddAction(new UpdateCurrentTextAction(toControl, "What you said didn't work though!"));
			_allChoiceReactions.Add(StopBeingParanoidChoice, new DispositionDependentReaction(StopBeingParanoidReaction));
			
			YupPrettyMuchChoice =  new Choice ("Yup! Pretty much.", "I hate all of you!");
			YupPrettyMuchReaction =  new Reaction();
			YupPrettyMuchReaction.AddAction(new NPCCallbackAction(UpdateYupPrettyMuch));
			YupPrettyMuchReaction.AddAction(new ShowOneOffChatAction(toControl, "I hate all of you!"));
			_allChoiceReactions.Add(YupPrettyMuchChoice, new DispositionDependentReaction(YupPrettyMuchReaction));
			
			LookJustActNaturalChoice =  new Choice("Look, just act natural", "What does that mean?");
			LookJustActNaturalReaction =  new Reaction();
			LookJustActNaturalReaction.AddAction(new NPCCallbackAction(UpdateLookJustActNatural));
			LookJustActNaturalReaction.AddAction(new UpdateCurrentTextAction(toControl, "What does that mean?"));
			
			JustTryBeingYourselfChoice =  new Choice("Just try being youself.", "But everybody hates who I am!");
			JustTryBeingYourselfReaction =  new Reaction();
			JustTryBeingYourselfReaction.AddAction(new NPCCallbackAction(UpdateJustTryBeingYourself));
			JustTryBeingYourselfReaction.AddAction(new UpdateCurrentTextAction(toControl, "But everybody hates who I am!"));
				
			HeWontTrustMeAlreadyChoice = new Choice("He won't! Trust me already!", "All right I'll try...");
			HeWontTrustMeAlreadyReaction = new Reaction();
			HeWontTrustMeAlreadyReaction.AddAction(new NPCCallbackAction(UpdateHeWontTrustMeAlready));
			HeWontTrustMeAlreadyReaction.AddAction(new ShowOneOffChatAction(toControl, "All right I'll try..."));
			
			FairEnoughChoice = new Choice("Fair enough.", "Hmmpphh...Let's go!");
			FairEnoughReaction = new Reaction();
			FairEnoughReaction.AddAction(new NPCCallbackAction(UpdateFairEnough));
			FairEnoughReaction.AddAction(new ShowOneOffChatAction(toControl, "Hmmpphh...Let's go!"));
			
			BeYourselfChoice = new Choice("Be yourself.", "I guess it won't hurt things anymore...");
			BeYourselfReaction = new Reaction();
			BeYourselfReaction.AddAction(new NPCCallbackAction(UpdateBeYourself));
			BeYourselfReaction.AddAction(new ShowOneOffChatAction(toControl, "I guess it won't hurt things anymore..."));
			
			IveHadEnoughChoice =  new Choice ("I've had enough!", "This is stupid, let's go!");
			IveHadEnoughReaction = new Reaction();
			IveHadEnoughReaction.AddAction(new NPCCallbackAction(IveHadEnough));
			IveHadEnoughReaction.AddAction(new ShowOneOffChatAction(toControl, "This is stupid, let's go!"));
		}
		public void UpdateStopBeingParanoid(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(StopBeingParanoidChoice);
			_allChoiceReactions.Remove(YupPrettyMuchChoice);
			_allChoiceReactions.Add(JustTryBeingYourselfChoice, new DispositionDependentReaction(JustTryBeingYourselfReaction));
			_allChoiceReactions.Add(LookJustActNaturalChoice, new DispositionDependentReaction(LookJustActNaturalReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("He won't talk with me!");
		}
		public void UpdateYupPrettyMuch(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(YupPrettyMuchChoice);
			_allChoiceReactions.Remove(StopBeingParanoidChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I hate you!");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
		}
		public void UpdateLookJustActNatural(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(LookJustActNaturalChoice);
			_allChoiceReactions.Remove(JustTryBeingYourselfChoice);
			_allChoiceReactions.Add(IveHadEnoughChoice, new DispositionDependentReaction(IveHadEnoughReaction));
			_allChoiceReactions.Add(BeYourselfChoice, new DispositionDependentReaction(BeYourselfReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("What does look natural mean?");
		}
		public void UpdateJustTryBeingYourself(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(LookJustActNaturalChoice);
			_allChoiceReactions.Remove(JustTryBeingYourselfChoice);
			_allChoiceReactions.Add(HeWontTrustMeAlreadyChoice, new DispositionDependentReaction(HeWontTrustMeAlreadyReaction));
			_allChoiceReactions.Add(FairEnoughChoice, new DispositionDependentReaction(FairEnoughReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Everyone hates who I really am");
		}
		public void UpdateHeWontTrustMeAlready(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(HeWontTrustMeAlreadyChoice);
			_allChoiceReactions.Remove(FairEnoughChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Wow...I didn't think he would be willing to be friends with me...");
			//Set Flags here
			FlagManager.instance.SetFlag(FlagStrings.ThirdConvoWithCSONNOTFriend);
		}
		public void UpdateFairEnough(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(HeWontTrustMeAlreadyChoice);
			_allChoiceReactions.Remove(FairEnoughChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Everyone I meet doesn't like me...");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
		}
		public void UpdateBeYourself(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(BeYourselfChoice);
			_allChoiceReactions.Remove(BeYourselfChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Wow...I didn't think he would be willing to be friends with me...");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.ThirdConvoWithCSONNOTFriend);
		}
		public void IveHadEnough(){
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
			_allChoiceReactions.Remove(BeYourselfChoice);
			_allChoiceReactions.Remove(IveHadEnoughChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Everyone I meet doesn't like me...");
			//Set flags here
			FlagManager.instance.SetFlag(FlagStrings.FinishedCSONConversation);
		}
		
	}
	#endregion
	#region LightHouse Girl
	//Emotion state after visiting with the lighthouse girl.  Shouldn't be filled with anything IMO, perhaps small dialogue but nothing significant.
	private class AfterLighthouse : EmotionState{
		
		public AfterLighthouse(NPC toControl, string currentDialogue) : base(toControl, "The farmer's daughter has to be the most amazing person I ever met!"){
			
		}
	}
	#endregion
	#region AtBeach
	//This is the conversation where you help the Castleman before talking with the lighthouse girl and you are a friend.
	private class WaitingAtBeachFriend : EmotionState{
		Choice YouDontNeedToKnowChoice;
		Reaction YouDontNeedToKnowReaction;
		Choice DoYouWantToBeFriendsChoice;
		Reaction DoYouWantToBeFriendsReaction;
		Choice ThenStopWorryingChoice;
		Reaction ThenStopWorryingReaction;
		Choice ToughLuckChoice;
		Reaction ToughLuckReaction;
		
		Choice OfCourseItIsChoice;
		Reaction OfCourseItIsReaction;
		Choice NoChanceChoice;
		Reaction NoChanceReaction;
		Choice NewThingsAreAlwaysScaryChoice;
		Reaction NewThingsAreAlwaysScareReaction;
		Choice JustActNaturallyChoice;
		Reaction JustActNaturallyReaction;
		Choice GiveUpChoice;
		Reaction GiveUpReaction;
		public WaitingAtBeachFriend(NPC toControl, string currentDialogue) : base(toControl, "I don't know what to do around the farmer's daughter.  She makes me nervous!"){
			//toControl.SetCharacterPortrait(StringsNPC.Embarrassed);
			YouDontNeedToKnowChoice = new Choice("You don't need to know.", "But its scary to talk with her.  What if I mess up?");
			YouDontNeedToKnowReaction = new Reaction();
			YouDontNeedToKnowReaction.AddAction(new NPCCallbackAction(UpdateYouDontNeedToKnow));
			YouDontNeedToKnowReaction.AddAction(new UpdateCurrentTextAction(toControl, "But its scary to talk with her.  What if I mess up?"));
			_allChoiceReactions.Add(YouDontNeedToKnowChoice, new DispositionDependentReaction(YouDontNeedToKnowReaction));
			
			DoYouWantToBeFriendsChoice = new Choice("Do you want to be friends?", "Yeah...I want to be friends with her...");
			DoYouWantToBeFriendsReaction = new Reaction();
			DoYouWantToBeFriendsReaction.AddAction(new NPCCallbackAction(UpdateDoYouWantToBeFriends));
			DoYouWantToBeFriendsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Yeah...I want to be friends with her..."));
			
			ThenStopWorryingChoice = new Choice("Then stop worrying.", "But...but...All right...I'll try to remain calm.");
			ThenStopWorryingReaction = new Reaction();
			ThenStopWorryingReaction.AddAction(new NPCCallbackAction(UpdateThenStopWorrying));
			ThenStopWorryingReaction.AddAction(new ShowOneOffChatAction(toControl, "But...but...All right...I'll try to remain calm."));
			
			ToughLuckChoice = new Choice("Tough luck.", "*sigh* I've got no chance here do I...");
			ToughLuckReaction = new Reaction();
			ToughLuckReaction.AddAction(new NPCCallbackAction(UpdateToughLuck));
			ToughLuckReaction.AddAction(new ShowOneOffChatAction(toControl, "*sigh* I've got no chance here do I..."));
			
			OfCourseItIsChoice = new Choice("Of course it is!", "But how do I overcome it?");
			OfCourseItIsReaction = new Reaction();
			OfCourseItIsReaction.AddAction(new NPCCallbackAction(UpdateOfCourseItIs));
			OfCourseItIsReaction.AddAction(new UpdateCurrentTextAction(toControl, "But how do I overcome it?"));
			
			NoChanceChoice = new Choice("You have no chance", "You're right, I can't overcome my fears.");
			NoChanceReaction = new Reaction();
			NoChanceReaction.AddAction(new NPCCallbackAction(UpdateNoChance));
			NoChanceReaction.AddAction(new ShowOneOffChatAction(toControl, "You're right, I can't overcome my fears."));
			
			NewThingsAreAlwaysScaryChoice = new Choice("New things are always scary.", "So how do I deal with my fear?");
			NewThingsAreAlwaysScareReaction = new Reaction();
			NewThingsAreAlwaysScareReaction.AddAction(new NPCCallbackAction(UpdateNewThingsAreAlwaysScary));
			NewThingsAreAlwaysScareReaction.AddAction(new UpdateCurrentTextAction(toControl, "So how do I deal with my fear?"));
			
			JustActNaturallyChoice = new Choice("Just act naturally.", "I...I'll give it my best shot.");
			JustActNaturallyReaction = new Reaction();
			JustActNaturallyReaction.AddAction(new NPCCallbackAction(UpdateJustActNaturally));
			JustActNaturallyReaction.AddAction(new ShowOneOffChatAction(toControl, "I...I'll give it my best shot."));
			
			GiveUpChoice = new Choice("Give up.", "I've got no chance do I.");
			GiveUpReaction = new Reaction();
			GiveUpReaction.AddAction(new NPCCallbackAction(UpdateGiveUp));
			GiveUpReaction.AddAction(new ShowOneOffChatAction(toControl, "I've got no chance do I."));
			
		}
		
		public void UpdateYouDontNeedToKnow(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(YouDontNeedToKnowChoice);
			_allChoiceReactions.Add(DoYouWantToBeFriendsChoice, new DispositionDependentReaction(DoYouWantToBeFriendsReaction));
			_allChoiceReactions.Add(OfCourseItIsChoice, new DispositionDependentReaction(OfCourseItIsReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'm scared to talk to her.");
		}
		public void UpdateDoYouWantToBeFriends(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(DoYouWantToBeFriendsChoice);
			_allChoiceReactions.Remove(OfCourseItIsChoice);
			_allChoiceReactions.Add(ThenStopWorryingChoice, new DispositionDependentReaction(ThenStopWorryingReaction));
			_allChoiceReactions.Add(ToughLuckChoice, new DispositionDependentReaction(ToughLuckReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I wish I could be friends with the farmer's daughter.");
		}
		public void UpdateThenStopWorrying(){
			_npcInState.SetCharacterPortrait(StringsNPC.Blink);
			_allChoiceReactions.Remove(ThenStopWorryingChoice);
			_allChoiceReactions.Remove(ToughLuckChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Stop worrying.  Stop worrying. Stop worrying.\nSorry I'm trying to get in a groove.");
			//Set flag here!
			FlagManager.instance.SetFlag(FlagStrings.BeachPreparedForConvo);
		}
		public void UpdateToughLuck(){
			_allChoiceReactions.Remove(ThenStopWorryingChoice);
			_allChoiceReactions.Remove(ToughLuckChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I've got no chance...");
			//Set flag here
			FlagManager.instance.SetFlag(FlagStrings.BeachNotPreparedForConvo);
		}
		public void UpdateOfCourseItIs(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(DoYouWantToBeFriendsChoice);
			_allChoiceReactions.Remove(OfCourseItIsChoice);
			_allChoiceReactions.Add(NewThingsAreAlwaysScaryChoice, new DispositionDependentReaction(NewThingsAreAlwaysScareReaction));
			_allChoiceReactions.Add(NoChanceChoice, new DispositionDependentReaction(NoChanceReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("How do I get over fear?");
		}
		public void UpdateNoChance(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(NoChanceChoice);
			_allChoiceReactions.Remove(NewThingsAreAlwaysScaryChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I'm always gonna be scared...");
			//Set flag here
			FlagManager.instance.SetFlag(FlagStrings.BeachNotPreparedForConvo);
		}
		public void UpdateNewThingsAreAlwaysScary(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(NoChanceChoice);
			_allChoiceReactions.Remove(NewThingsAreAlwaysScaryChoice);
			_allChoiceReactions.Add(JustActNaturallyChoice, new DispositionDependentReaction(JustActNaturallyReaction));
			_allChoiceReactions.Add(GiveUpChoice, new DispositionDependentReaction(GiveUpReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("How do I deal with fear?");
		}
		public void UpdateJustActNaturally(){
			_npcInState.SetCharacterPortrait(StringsNPC.Blink);
			_allChoiceReactions.Remove(JustActNaturallyChoice);
			_allChoiceReactions.Remove(GiveUpChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Act naturally. Act naturally. Act naturally.\nSorry trying to get in my groove.");
			//Set flag here too!
			FlagManager.instance.SetFlag(FlagStrings.BeachPreparedForConvo);
		}
		public void UpdateGiveUp(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(JustActNaturallyChoice);
			_allChoiceReactions.Remove(GiveUpChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I give up...");
			//Set flag here
			FlagManager.instance.SetFlag(FlagStrings.BeachNotPreparedForConvo);
		}

	}
	//Not friends and waiting for the lighthouse girl.
	private class WaitingAtBeachNotAsFriend : EmotionState{
		Choice YouDontNeedToKnowChoice;
		Reaction YouDontNeedToKnowReaction;
		Choice DoYouWantToBeFriendsChoice;
		Reaction DoYouWantToBeFriendsReaction;
		Choice ThenStopWorryingChoice;
		Reaction ThenStopWorryingReaction;
		Choice ToughLuckChoice;
		Reaction ToughLuckReaction;
		
		Choice OfCourseItIsChoice;
		Reaction OfCourseItIsReaction;
		Choice NoChanceChoice;
		Reaction NoChanceReaction;
		Choice NewThingsAreAlwaysScaryChoice;
		Reaction NewThingsAreAlwaysScareReaction;
		Choice JustActNaturallyChoice;
		Reaction JustActNaturallyReaction;
		Choice GiveUpChoice;
		Reaction GiveUpReaction;
		
		Choice HaveAFunTimeChoice;
		Reaction HaveAFunTimeReaction;
		Choice IHearWeddingBellsChoice;
		Reaction IHearWeddingBellsReaction;
		Choice MaybeYouShouldBeFriendsChoice;
		Reaction MaybeYouShouldBeFriendsReaction;
		public WaitingAtBeachNotAsFriend(NPC toControl, string currentDialogue) : base(toControl, "What do you want?  Go away!"){
			HaveAFunTimeChoice = new Choice("Have a fun time?", "Sorta...Especially with the Farmer's Daughter.\nShe's the most amazing girl I've met!");
			HaveAFunTimeReaction = new Reaction();
			HaveAFunTimeReaction.AddAction(new NPCCallbackAction(UpdateHaveAFunTime));
			HaveAFunTimeReaction.AddAction(new UpdateCurrentTextAction(toControl, "Sorta...Especially with the Farmer's Daughter.\nShe's the most amazing girl I've met!"));
			_allChoiceReactions.Add(HaveAFunTimeChoice, new DispositionDependentReaction(HaveAFunTimeReaction));
			
			IHearWeddingBellsChoice = new Choice("I hear wedding bells!", "Oh shut up!  Stop BEING MEAN!");
			IHearWeddingBellsReaction = new Reaction();
			IHearWeddingBellsReaction.AddAction(new NPCCallbackAction(UpdateIHearWeddingBells));
			IHearWeddingBellsReaction.AddAction(new ShowOneOffChatAction(toControl, "Oh shut up!  Stop BEING MEAN!"));
			
			
			MaybeYouShouldBeFriendsChoice = new Choice("Maybe you should be friends?", "But I don't know what to say to her.  I get nervous around her...");
			MaybeYouShouldBeFriendsReaction = new Reaction();
			MaybeYouShouldBeFriendsReaction.AddAction(new NPCCallbackAction(UpdateMaybeYouShouldBeFriends));
			MaybeYouShouldBeFriendsReaction.AddAction(new UpdateCurrentTextAction(toControl, "But I don't know what to say to her.  I get nervous around her..."));
			
			
			
			YouDontNeedToKnowChoice = new Choice("You don't need to know.", "But its scary to talk with her.  What if I mess up?");
			YouDontNeedToKnowReaction = new Reaction();
			YouDontNeedToKnowReaction.AddAction(new NPCCallbackAction(UpdateYouDontNeedToKnow));
			YouDontNeedToKnowReaction.AddAction(new UpdateCurrentTextAction(toControl, "But its scary to talk with her.  What if I mess up?"));
			
			DoYouWantToBeFriendsChoice = new Choice("Do you want to be friends?", "Yeah...I want to be friends with her...");
			DoYouWantToBeFriendsReaction = new Reaction();
			DoYouWantToBeFriendsReaction.AddAction(new NPCCallbackAction(UpdateDoYouWantToBeFriends));
			DoYouWantToBeFriendsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Yeah...I want to be friends with her..."));
			
			ThenStopWorryingChoice = new Choice("Then stop worrying.", "But...but...All right...I'll try to remain calm.");
			ThenStopWorryingReaction = new Reaction();
			ThenStopWorryingReaction.AddAction(new NPCCallbackAction(UpdateThenStopWorrying));
			ThenStopWorryingReaction.AddAction(new ShowOneOffChatAction(toControl, "But...but...All right...I'll try to remain calm."));
			
			ToughLuckChoice = new Choice("Tough luck.", "*sigh* I've got no chance here do I...");
			ToughLuckReaction = new Reaction();
			ToughLuckReaction.AddAction(new NPCCallbackAction(UpdateToughLuck));
			ToughLuckReaction.AddAction(new ShowOneOffChatAction(toControl, "*sigh* I've got no chance here do I..."));
			
			OfCourseItIsChoice = new Choice("Of course it is!", "But how do I overcome it?");
			OfCourseItIsReaction = new Reaction();
			OfCourseItIsReaction.AddAction(new NPCCallbackAction(UpdateOfCourseItIs));
			OfCourseItIsReaction.AddAction(new UpdateCurrentTextAction(toControl, "But how do I overcome it?"));
			
			NoChanceChoice = new Choice("You have no chance", "You're right, I can't overcome my fears.");
			NoChanceReaction = new Reaction();
			NoChanceReaction.AddAction(new NPCCallbackAction(UpdateNoChance));
			NoChanceReaction.AddAction(new ShowOneOffChatAction(toControl, "You're right, I can't overcome my fears."));
			
			NewThingsAreAlwaysScaryChoice = new Choice("New things are always scary.", "So how do I deal with my fear?");
			NewThingsAreAlwaysScareReaction = new Reaction();
			NewThingsAreAlwaysScareReaction.AddAction(new NPCCallbackAction(UpdateNewThingsAreAlwaysScary));
			NewThingsAreAlwaysScareReaction.AddAction(new UpdateCurrentTextAction(toControl, "So how do I deal with my fear?"));
			
			JustActNaturallyChoice = new Choice("Just act naturally.", "I...I'll give it my best shot.");
			JustActNaturallyReaction = new Reaction();
			JustActNaturallyReaction.AddAction(new NPCCallbackAction(UpdateJustActNaturally));
			JustActNaturallyReaction.AddAction(new ShowOneOffChatAction(toControl, "I...I'll give it my best shot."));
			
			GiveUpChoice = new Choice("Give up.", "I've got no chance do I.");
			GiveUpReaction = new Reaction();
			GiveUpReaction.AddAction(new NPCCallbackAction(UpdateGiveUp));
			GiveUpReaction.AddAction(new ShowOneOffChatAction(toControl, "I've got no chance do I."));
		}
		public void UpdateHaveAFunTime(){
			_npcInState.SetCharacterPortrait(StringsNPC.Embarrassed);
			_allChoiceReactions.Remove(HaveAFunTimeChoice);
			_allChoiceReactions.Add(IHearWeddingBellsChoice, new DispositionDependentReaction(IHearWeddingBellsReaction));
			_allChoiceReactions.Add(MaybeYouShouldBeFriendsChoice, new DispositionDependentReaction(MaybeYouShouldBeFriendsReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("The farmer's daughter...");
		}
		public void UpdateIHearWeddingBells(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(IHearWeddingBellsChoice);
			_allChoiceReactions.Remove(MaybeYouShouldBeFriendsChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("You are so mean!");
		}
		public void UpdateMaybeYouShouldBeFriends(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(IHearWeddingBellsChoice);
			_allChoiceReactions.Remove(MaybeYouShouldBeFriendsChoice);
			_allChoiceReactions.Add(YouDontNeedToKnowChoice, new DispositionDependentReaction(YouDontNeedToKnowReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I don't know what to say to her...");
		}
		
		public void UpdateYouDontNeedToKnow(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(YouDontNeedToKnowChoice);
			_allChoiceReactions.Add(DoYouWantToBeFriendsChoice, new DispositionDependentReaction(DoYouWantToBeFriendsReaction));
			_allChoiceReactions.Add(OfCourseItIsChoice, new DispositionDependentReaction(OfCourseItIsReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I'm scared to talk to her.");
		}
		public void UpdateDoYouWantToBeFriends(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(DoYouWantToBeFriendsChoice);
			_allChoiceReactions.Remove(OfCourseItIsChoice);
			_allChoiceReactions.Add(ThenStopWorryingChoice, new DispositionDependentReaction(ThenStopWorryingReaction));
			_allChoiceReactions.Add(ToughLuckChoice, new DispositionDependentReaction(ToughLuckReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I wish I could be friends with the farmer's daughter.");
		}
		public void UpdateThenStopWorrying(){
			_npcInState.SetCharacterPortrait(StringsNPC.Blink);
			_allChoiceReactions.Remove(ThenStopWorryingChoice);
			_allChoiceReactions.Remove(ToughLuckChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Stop worrying.  Stop worrying. Stop worrying.\nSorry I'm trying to get in a groove.");
			//Set flag here too!
			FlagManager.instance.SetFlag(FlagStrings.BeachPreparedForConvo);
		}
		public void UpdateToughLuck(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(ThenStopWorryingChoice);
			_allChoiceReactions.Remove(ToughLuckChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I've got no chance...");
			//Set flag reactions
			FlagManager.instance.SetFlag(FlagStrings.BeachNotPreparedForConvo);
		}
		public void UpdateOfCourseItIs(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(DoYouWantToBeFriendsChoice);
			_allChoiceReactions.Remove(OfCourseItIsChoice);
			_allChoiceReactions.Add(NewThingsAreAlwaysScaryChoice, new DispositionDependentReaction(NewThingsAreAlwaysScareReaction));
			_allChoiceReactions.Add(NoChanceChoice, new DispositionDependentReaction(NoChanceReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("How do I get over fear?");
		}
		public void UpdateNoChance(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(NoChanceChoice);
			_allChoiceReactions.Remove(NewThingsAreAlwaysScaryChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I'm always gonna be scared...");
			//Set flag reactions
			FlagManager.instance.SetFlag(FlagStrings.BeachNotPreparedForConvo);
		}
		public void UpdateNewThingsAreAlwaysScary(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
			_allChoiceReactions.Remove(NoChanceChoice);
			_allChoiceReactions.Remove(NewThingsAreAlwaysScaryChoice);
			_allChoiceReactions.Add(JustActNaturallyChoice, new DispositionDependentReaction(JustActNaturallyReaction));
			_allChoiceReactions.Add(GiveUpChoice, new DispositionDependentReaction(GiveUpReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("How do I deal with fear?");
		}
		public void UpdateJustActNaturally(){
			_npcInState.SetCharacterPortrait(StringsNPC.Blink);
			_allChoiceReactions.Remove(JustActNaturallyChoice);
			_allChoiceReactions.Remove(GiveUpChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("Act naturally. Act naturally. Act naturally.\nSorry trying to get in my groove.");
			//Set flag here!
			FlagManager.instance.SetFlag(FlagStrings.BeachPreparedForConvo);
		}
		public void UpdateGiveUp(){
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
			_allChoiceReactions.Remove(JustActNaturallyChoice);
			_allChoiceReactions.Remove(GiveUpChoice);
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("I give up...");
			//Set flag reactions
			FlagManager.instance.SetFlag(FlagStrings.BeachNotPreparedForConvo);
		}
	}
	//After the talk if things went poorly this is the end state for the castleman
	private class AfterWaitingAtBeachBadResult : EmotionState{
				
		public AfterWaitingAtBeachBadResult(NPC toControl, string currentDialogue) : base(toControl, "I failed at talking to the farmer's daughter!\nI don't know what to do now."){
					
		}
	}
	//After the talk if things went well.  This is the end state for the castleman		
	private class AfterWaitingAtBeachGoodResult : EmotionState{
				
		public AfterWaitingAtBeachGoodResult(NPC toControl, string currentDialogue) : base(toControl, "I'm so glad you helped me figure out what to say to her!"){
					
		}
	}
	#endregion
	#endregion
}
