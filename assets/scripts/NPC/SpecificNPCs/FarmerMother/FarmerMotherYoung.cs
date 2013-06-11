using UnityEngine;
using System.Collections;

/// <summary>
/// Farmer Mother young specific scripting values
/// </summary>
public class FarmerMotherYoung : NPC {	
	//Emotion state switching doesn't take into account any set flags.
	//Needs to be fixed
	
	bool ConversationInMiddleSet = false;
	bool TellOnLighthouseSet = false;
	
	NPCConvoSchedule InitialConversation;
	
	protected override void Init() {
		id = NPCIDs.FARMER_MOTHER;
		base.Init();
	}
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
		NewDialogueReaction.AddAction (new NPCCallbackAction(UpdateConversationInMiddleFarmerMother));
		flagReactions.Add(FlagStrings.ConversationInMiddleFarmerMother, NewDialogueReaction);
		
		Reaction TellOnReaction = new Reaction();
		TellOnReaction.AddAction (new NPCCallbackAction(UpdateTellOnLighthouse));
		flagReactions.Add(FlagStrings.TellOnLighthouse, TellOnReaction);
		
		Reaction OpeningConversation = new Reaction();
		OpeningConversation.AddAction(new NPCAddScheduleAction(this, InitialConversation));
		flagReactions.Add(FlagStrings.OpeningConversationFarmerMotherToFarmerFather, OpeningConversation);
		
		Reaction StartHoeing = new Reaction();
		StartHoeing.AddAction(new NPCAddScheduleAction(this, postOpenningSchedule));
		flagReactions.Add(FlagStrings.HoeAfterDialogue, StartHoeing);
		
	}
	public void UpdateConversationInMiddleFarmerMother(){
		this.currentEmotion.PassStringToEmotionState(FlagStrings.ConversationInMiddleFarmerMother);
		ConversationInMiddleSet = true;
	}
	public void UpdateTellOnLighthouse(){
		this.currentEmotion.PassStringToEmotionState(FlagStrings.TellOnLighthouse);
		TellOnLighthouseSet = true;
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new StoryStoppingEmotionState(this, "Talk to me later. I'm busy!"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (openningWaitingSchedule);
	}
	
	Schedule openningWaitingSchedule;
	Schedule postOpenningSchedule;

	protected override void SetUpSchedules(){
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoConvo);
		openningWaitingSchedule.Add(new Task(new WaitTillPlayerCloseState(this, ref player)));
		Task setFlag =  (new TimeTask(2f, new IdleState(this)));
		setFlag.AddFlagToSet(FlagStrings.OpeningConversationFarmerMotherToFarmerFather);
		openningWaitingSchedule.Add(setFlag);
		
		
		InitialConversation = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerFatherYoung), 
			new YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue(),Schedule.priorityEnum.DoConvo);
		InitialConversation.SetCanNotInteractWithPlayer();
		InitialConversation.SetFlagOnComplete(FlagStrings.HoeAfterDialogue);
		
		postOpenningSchedule = new Schedule(this, Schedule.priorityEnum.Medium);
		Task goToField = new Task(new MoveState(this, new Vector3(transform.position.x - 4, transform.position.y, transform.position.z)));
		Task hoeField = new Task(new AbstractAnimationState(this, "Hoe"));
		postOpenningSchedule.Add(hoeField);
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){

		}
	
	}
	#endregion
	
	#region Post-Openning Conversation Emotion State
	private class StoryStoppingEmotionState : EmotionState{
		NPC farmerMotherYoung;
		Reaction GiveSeedsReaction;
		Reaction reactionToWhatAbout;
		Choice whatAboutChoice = new Choice("What was that about?", "You saw our little spat? Don't worry about it! " +
			"It's just some talk on how ta raise our daughter. Don't need ta fill her head with silly stories...");
		
		Choice growGardenChoice = new Choice("Yes", "Well then, ya should be well versed in how ta plant seeds." +
										"Here in exchange for tha pendant! I'll give ya these sunflower seeds. ");
		Choice dontGrowGardenChoice = new Choice("No", "Well, I can teach ya how if ya want ta! " +
			"Just find a mound of earth and put tha seeds you're carrying in it. Then just let time do its work."  +
				"Here in exchange for tha pendant! I'll give ya these sunflower seeds.");
		
		bool updateConversationFarmerMotherYoungFlag = false;
		bool setTellOn = false;
		bool FinishedStoriesConversation = false;
		bool FinishedGardeningConversation = false;
		bool FinishedTellOnConversation = false;
		
		Choice TellOnDaughterChoice;
		Reaction TellOnDaughterReaction;
				
		Choice StoriesHelpRelateChoice;
		Reaction StoriesHelpRelateReaction;
		Choice WhyNotChoice;
		Reaction WhyNotReaction;
		Choice MyMomChoice;
		Reaction MyMomReaction;
		
		Choice StoriesFunChoice;
		Reaction StoriesFunReaction;
		
		Choice StoriesSillyChoice;
		Reaction StoriesSillyReaction;
		Choice StoriesUsefulChoice;
		Reaction StoriesUsefulReaction;
		Choice GuessSoChoice;
		Reaction GuessSoReaction;
		Choice StoriesArentHorribleChoice;
		Reaction StoriesArentHorribleReaction;
		Choice IllConvinceChoice;
		Reaction IllConvinceReaction;
		Choice CanReadAndWorkChoice;
		Reaction CanReadAndWorkReaction;
		Choice SheShouldDecideChoice;
		Reaction SheShouldDecideReaction;
		Choice DaughterFarmerChoice;                                 
		Reaction DaughterFarmerReaction;
		Choice NotSillyChoice;
		Reaction NotSillyReaction;
		Choice YouAreRightChoice;
		Reaction YouAreRightReaction;
	
		public StoryStoppingEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			farmerMotherYoung = toControl;
			
			reactionToWhatAbout = new Reaction(new NPCCallbackAction(askedWhatAbout));
			_allChoiceReactions.Add(whatAboutChoice, new DispositionDependentReaction(reactionToWhatAbout));
			
			Reaction reactionToGivePendant = new Reaction();
			reactionToGivePendant.AddAction(new NPCTakeItemAction(toControl));
			reactionToGivePendant.AddAction(new NPCCallbackAction(givePendant));
			
			//_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(reactionToGivePendant));
			GiveSeedsReaction = new Reaction();
			GiveSeedsReaction.AddAction(new NPCCallbackAction(UpdateGiveSeeds));
			GiveSeedsReaction.AddAction(new NPCTakeItemAction(toControl));
			GiveSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks fer tha seeds!  Just what we needed after my idiot husband fergot ta buy them!"));
			_allItemReactions.Add(StringsItem.SunflowerSeeds, new DispositionDependentReaction(GiveSeedsReaction));
			
			StoriesFunChoice = new Choice("But stories are fun!", "Hmmpphh...I didn't need any fancy stories when I was growing up!  What ya need kid is discipline!");
			StoriesFunReaction = new Reaction();
			StoriesFunReaction.AddAction(new NPCCallbackAction(UpdateStoriesFun));
			StoriesFunReaction.AddAction(new UpdateCurrentTextAction(toControl, "Hmmpphh...I didn't need any fancy stories when I was growing up!  What ya need kid is discipline!"));
			
			StoriesSillyChoice = new Choice("Stories are silly.", "Haw!  A kid aftah my own heart!  If only my fool daughter and husband could see things the way I do!");
			StoriesSillyReaction = new Reaction();
			StoriesSillyReaction.AddAction(new NPCCallbackAction(UpdateStoriesSilly));
			StoriesSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "Haw!  A kid aftah my own heart!  If only my fool daughter and husband could see things the way I do!"));
			
			StoriesUsefulChoice = new Choice("But stories are useful!", "Kid, I know your too young to understand, but we all have a job, and mine and my daughters isn't telling stories but taking care of this farm!");
			StoriesUsefulReaction = new Reaction();
			StoriesUsefulReaction.AddAction(new NPCCallbackAction(UpdateStoriesUseful));
			StoriesUsefulReaction.AddAction(new UpdateCurrentTextAction(toControl, "Kid, I know your too young to understand, but we all have a job, and mine and my daughters isn't telling stories but taking care of this farm!"));
			
			GuessSoChoice = new Choice("I guess so...", "Look.  What you need ta understand is that, we all have jobs ta do, and we don't need ta waste our time filling our heads with nonsense bout castles and warriors.  We're a family of farmers, not fighters!");
			GuessSoReaction = new Reaction();
			GuessSoReaction.AddAction(new NPCCallbackAction(UpdateGuessSo));
			GuessSoReaction.AddAction(new UpdateCurrentTextAction(toControl, "Look.  What you need ta understand is that, we all have jobs ta do, and we don't need ta waste our time filling our heads with nonsense bout castles and warriors.  We're a family of farmers, not fighters!"));
			
			StoriesArentHorribleChoice = new Choice("Stories aren't horrible though.", "Look.  What you need ta understand is that, we all have jobs ta do, and we don't need ta waste our time filling our heads with nonsense bout castles and warriors.  We're a family of farmers, not fighters!");
			StoriesArentHorribleReaction = new Reaction();
			StoriesArentHorribleReaction.AddAction(new NPCCallbackAction(UpdateStoriesArentHorrible));
			StoriesArentHorribleReaction.AddAction(new UpdateCurrentTextAction(toControl, "Look.  What you need ta understand is that, we all have jobs ta do, and we don't need ta waste our time filling our heads with nonsense bout castles and warriors.  We're a family of farmers, not fighters!"));
			
			IllConvinceChoice = new Choice("I'll convince them!", "Naw...its okay, I know how to handle my own family!  I'll make sure that they see my way in time!");
			IllConvinceReaction = new Reaction();
			IllConvinceReaction.AddAction(new NPCCallbackAction(UpdateIllConvince));
			IllConvinceReaction.AddAction(new UpdateCurrentTextAction(toControl, "Naw...its okay, I know how to handle my own family!  I'll make sure that they see my way in time!"));
			
			CanReadAndWorkChoice = new Choice("You can work and read stories.", "*Sigh*  You're just too young ta understand.");
			CanReadAndWorkReaction = new Reaction();
			CanReadAndWorkReaction.AddAction(new NPCCallbackAction(UpdateCanReadAndWork));
			CanReadAndWorkReaction.AddAction(new UpdateCurrentTextAction(toControl, "*Sigh*  You're just too young ta understand."));
			
			SheShouldDecideChoice = new Choice("Maybe she should decide.", "That silly girl don't know what's best for her!  She's nearly got herself killed tryin to pretend to be some tragic hero by jumpin off the cliff.");
			SheShouldDecideReaction = new Reaction();
			SheShouldDecideReaction.AddAction(new NPCCallbackAction(UpdateSheShouldDecide));
			SheShouldDecideReaction.AddAction(new UpdateCurrentTextAction(toControl, "That silly girl don't know what's best for her!  She's nearly got herself killed tryin to pretend to be some tragic hero by jumpin off the cliff."));
			
			DaughterFarmerChoice = new Choice("So your daughter will be a farmer?", "Of course!  What else would she be?  A warrior like she says she will?  Don't make me laugh!");
			DaughterFarmerReaction = new Reaction();
			DaughterFarmerReaction.AddAction(new NPCCallbackAction(UpdateDaughterFarmer));
			DaughterFarmerReaction.AddAction(new UpdateCurrentTextAction(toControl, "Of course!  What else would she be?  A warrior like she says she will?  Don't make me laugh!"));
			
			NotSillyChoice = new Choice("She's not silly!", "*Sigh*  You're just too young ta understand.");
			NotSillyReaction = new Reaction();
			NotSillyReaction.AddAction(new NPCCallbackAction(UpdateNotSilly));
			NotSillyReaction.AddAction(new UpdateCurrentTextAction(toControl, "*Sigh*  You're just too young ta understand."));
			
			YouAreRightChoice = new Choice("You're right.", "Course I am!");
			YouAreRightReaction = new Reaction();
			YouAreRightReaction.AddAction(new NPCCallbackAction(UpdateYouAreRight));
			YouAreRightReaction.AddAction(new UpdateCurrentTextAction(toControl, "Course I am!"));
			
			StoriesHelpRelateChoice = new Choice("Stories help her get closer to her dad.", "Ya don't need ta be close to yah parents!  Ya just need them ta discipline ya.");
			StoriesHelpRelateReaction = new Reaction();
			StoriesHelpRelateReaction.AddAction(new NPCCallbackAction(UpdateStoriesHelpRelate));
			StoriesHelpRelateReaction.AddAction(new UpdateCurrentTextAction(toControl, "Ya don't need ta be close to yah parents!  Ya just need them ta discipline ya."));
			
			WhyNotChoice = new Choice("Why not?", "My dad didn't tell stories, and I came out okay!");
			WhyNotReaction = new Reaction();
			WhyNotReaction.AddAction(new NPCCallbackAction(UpdateWhyNot));
			WhyNotReaction.AddAction(new UpdateCurrentTextAction(toControl, "My dad didn't tell stories, and I came out okay!"));
			
			MyMomChoice = new Choice("My mom tells me stories.", "Yeah?  Well...I guess you don't jump off of cliffs...Perhaps the stories ain't the problem...I'll let her keep the stories so long as she don't jump off cliffs again.");
			MyMomReaction = new Reaction();
			MyMomReaction.AddAction(new NPCCallbackAction(UpdateMyMom));
			MyMomReaction.AddAction(new UpdateCurrentTextAction(toControl, "Yeah?  Well...I guess you don't jump off of cliffs...Perhaps the stories ain't the problem...I'll let her keep the stories so long as she don't jump off cliffs again."));
			
			TellOnDaughterChoice = new Choice ("Tell on daughter.", "*Sigh* Thanks fer talkin ta me bout this.");
			TellOnDaughterReaction = new Reaction();
			TellOnDaughterReaction.AddAction(new NPCCallbackAction(UpdateTellOnDaughter));
			TellOnDaughterReaction.AddAction(new UpdateCurrentTextAction(toControl, "*Sigh* Thanks fer talking ta me bout this."));
		}
		public void UpdateGiveSeeds(){
			_allItemReactions.Remove(StringsItem.SunflowerSeeds);
			FlagManager.instance.SetFlag(FlagStrings.FarmAlive);	
		}
		public void UpdateTellOnDaughter(){
			_allChoiceReactions.Remove(TellOnDaughterChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Thanks fer talkin to me bout that.  That girl needs ta learn responsibility.");
			_npcInState.PlayAnimation("Hoe");
			FinishedTellOnConversation = true;
			FlagManager.instance.SetFlag(FlagStrings.TellOnLighthouseConversation);
			FlagManager.instance.SetFlag(FlagStrings.TellOnDaughter);
		}
		
		public void UpdateStoriesHelpRelate(){
			_allChoiceReactions.Remove(StoriesSillyChoice);
			_allChoiceReactions.Remove(StoriesFunChoice);
			_allChoiceReactions.Remove(StoriesHelpRelateChoice);
			_allChoiceReactions.Add(WhyNotChoice, new DispositionDependentReaction(WhyNotReaction));
			_allChoiceReactions.Add(MyMomChoice, new DispositionDependentReaction(MyMomReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateWhyNot(){
			_allChoiceReactions.Remove(WhyNotChoice);
			_allChoiceReactions.Remove(MyMomChoice);
			if(setTellOn == true && FinishedTellOnConversation == false){
				_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I reckon you were right bout those stories.");
			_npcInState.PlayAnimation("Hoe");
			FinishedStoriesConversation = true;
		}
		public void UpdateMyMom(){
			_allChoiceReactions.Remove(WhyNotChoice);
			_allChoiceReactions.Remove(MyMomChoice);
			if(setTellOn == true && FinishedTellOnConversation == false){
				_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Don't come to me talkin bout those silly stories again.");
			_npcInState.PlayAnimation("Hoe");
			FinishedStoriesConversation = true;
			//Setflag here
		}
		
		
		
		public void UpdateStoriesFun(){
			_allChoiceReactions.Remove(StoriesFunChoice);
			_allChoiceReactions.Remove(StoriesSillyChoice);
			_allChoiceReactions.Add(StoriesUsefulChoice, new DispositionDependentReaction(StoriesUsefulReaction));
			_allChoiceReactions.Add(GuessSoChoice, new DispositionDependentReaction(GuessSoReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateStoriesSilly(){
			_allChoiceReactions.Remove(StoriesFunChoice);
			_allChoiceReactions.Remove(StoriesSillyChoice);
			_allChoiceReactions.Add(StoriesArentHorribleChoice, new DispositionDependentReaction(StoriesArentHorribleReaction));
			_allChoiceReactions.Add(IllConvinceChoice, new DispositionDependentReaction(IllConvinceReaction));
			GUIManager.Instance.RefreshInteraction();
			FlagManager.instance.SetFlag(FlagStrings.StoriesAreSilly);
		}
		public void UpdateStoriesUseful(){
			_allChoiceReactions.Remove(StoriesUsefulChoice);
			_allChoiceReactions.Remove(GuessSoChoice);
			_allChoiceReactions.Add(CanReadAndWorkChoice, new DispositionDependentReaction(CanReadAndWorkReaction));
			_allChoiceReactions.Add(SheShouldDecideChoice, new DispositionDependentReaction(SheShouldDecideReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateGuessSo(){
			_allChoiceReactions.Remove(StoriesUsefulChoice);
			_allChoiceReactions.Remove(GuessSoChoice);
			_allChoiceReactions.Add(DaughterFarmerChoice, new DispositionDependentReaction(DaughterFarmerReaction));
			_allChoiceReactions.Add(SheShouldDecideChoice, new DispositionDependentReaction(SheShouldDecideReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateStoriesArentHorrible(){
			_allChoiceReactions.Remove(StoriesArentHorribleChoice);
			_allChoiceReactions.Remove(IllConvinceChoice);
			_allChoiceReactions.Add(DaughterFarmerChoice, new DispositionDependentReaction(DaughterFarmerReaction));
			_allChoiceReactions.Add(SheShouldDecideChoice, new DispositionDependentReaction(SheShouldDecideReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateIllConvince(){
			_allChoiceReactions.Remove(StoriesArentHorribleChoice);
			_allChoiceReactions.Remove(IllConvinceChoice);
			if(setTellOn == true && FinishedTellOnConversation == false){
				_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
			_npcInState.PlayAnimation("Hoe");
			FinishedStoriesConversation = true;
		}
		public void UpdateCanReadAndWork(){
			_allChoiceReactions.Remove(CanReadAndWorkChoice);
			_allChoiceReactions.Remove(SheShouldDecideChoice);
			if(setTellOn == true && FinishedTellOnConversation == false){
				_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
			_npcInState.PlayAnimation("Hoe");
			FinishedStoriesConversation = true;
			FlagManager.instance.SetFlag(FlagStrings.WorkAndStories);
		}
		public void UpdateSheShouldDecide(){
			_allChoiceReactions.Remove(SheShouldDecideChoice);
			if(_allChoiceReactions.ContainsKey(CanReadAndWorkChoice)){
				_allChoiceReactions.Remove(CanReadAndWorkChoice);	
			}
			if(_allChoiceReactions.ContainsKey(DaughterFarmerChoice)){
				_allChoiceReactions.Remove(DaughterFarmerChoice);	
			}
			_allChoiceReactions.Add(NotSillyChoice, new DispositionDependentReaction(NotSillyReaction));
			_allChoiceReactions.Add(YouAreRightChoice, new DispositionDependentReaction(YouAreRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateDaughterFarmer(){
			_allChoiceReactions.Remove(SheShouldDecideChoice);
			_allChoiceReactions.Remove(DaughterFarmerChoice);
			_allChoiceReactions.Add(YouAreRightChoice, new DispositionDependentReaction(YouAreRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateNotSilly(){
			_allChoiceReactions.Remove(NotSillyChoice);
			_allChoiceReactions.Remove(YouAreRightChoice);
			if(setTellOn == true && FinishedTellOnConversation == false){
				_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
			_npcInState.PlayAnimation("Hoe");
			FinishedStoriesConversation = true;
			FlagManager.instance.SetFlag(FlagStrings.NotSilly);
		}
		public void UpdateYouAreRight(){
			_allChoiceReactions.Remove(YouAreRightChoice);
			if(_allChoiceReactions.ContainsKey(NotSillyChoice)){
				_allChoiceReactions.Remove(NotSillyChoice);	
			}
			if(setTellOn == true && FinishedTellOnConversation == false){
				_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
			_npcInState.PlayAnimation("Hoe");
			FinishedStoriesConversation = true;
			FlagManager.instance.SetFlag(FlagStrings.YourRight);
		}
		
		
		
		
		
		
		public override void UpdateEmotionState(){
			/*if(updateConversationFarmerMotherYoungFlag == false){
				if(_allChoiceReactions.ContainsKey(StoriesFunChoice)){
					_allChoiceReactions.Add(StoriesHelpRelateChoice, new DispositionDependentReaction(StoriesHelpRelateReaction));
				}
				updateConversationFarmerMotherYoungFlag = true;
			}*/
		}
			
		private void askedWhatAbout(){
			_allChoiceReactions.Remove(whatAboutChoice);
			if (_allChoiceReactions.ContainsKey(TellOnDaughterChoice)){
				_allChoiceReactions.Remove(TellOnDaughterChoice);
			}
			_allChoiceReactions.Add(StoriesFunChoice, new DispositionDependentReaction(StoriesFunReaction));
			_allChoiceReactions.Add(StoriesSillyChoice, new DispositionDependentReaction(StoriesSillyReaction));
			if(updateConversationFarmerMotherYoungFlag == true && !(_allChoiceReactions.ContainsKey(StoriesHelpRelateChoice))){
					_allChoiceReactions.Add(StoriesHelpRelateChoice, new DispositionDependentReaction(StoriesHelpRelateReaction));	
			}
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void tellOnDaughter(){
			farmerMotherYoung.AddSchedule(new NPCConvoSchedule(farmerMotherYoung, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
				new YoungFarmerMotherToLighthouseGirlToldOn(),Schedule.priorityEnum.DoNow));
				
		}

		private void givePendant(){
			_allChoiceReactions.Remove(whatAboutChoice);
			Reaction reactionToGiveSunflowerSeeds = new Reaction(new NPCCallbackAction(dropSunflowerSeeds));
			
			_allItemReactions.Remove(StringsItem.Apple);
			SetDefaultText("You want to grow a flower garden, eh? Ya ever done it before?");
			
			_allChoiceReactions.Add(growGardenChoice, new DispositionDependentReaction(reactionToGiveSunflowerSeeds));
			_allChoiceReactions.Add(dontGrowGardenChoice, new DispositionDependentReaction(reactionToGiveSunflowerSeeds));
			
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void dropSunflowerSeeds(){
			_allChoiceReactions.Remove(growGardenChoice);
			_allChoiceReactions.Remove(dontGrowGardenChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("What do you want? I got lots of work to do!");
			_allChoiceReactions.Add(whatAboutChoice, new DispositionDependentReaction(reactionToWhatAbout));
			
		}
					
		public override void PassStringToEmotionState (string text)
		{
			if (text == FlagStrings.ConversationInMiddleFarmerMother){
				if(updateConversationFarmerMotherYoungFlag == false && FinishedStoriesConversation == false){
					if(_allChoiceReactions.ContainsKey(StoriesFunChoice)){
						_allChoiceReactions.Add(StoriesHelpRelateChoice, new DispositionDependentReaction(StoriesHelpRelateReaction));
					}
					updateConversationFarmerMotherYoungFlag = true;
				}
			}
			else if (text == FlagStrings.TellOnLighthouse){
				setTellOn = true;
				if(_allChoiceReactions.ContainsKey(whatAboutChoice)){
					_allChoiceReactions.Add(TellOnDaughterChoice, new DispositionDependentReaction(TellOnDaughterReaction));
				}
			}
		}
	
	}
	#endregion
	#endregion
}
