using UnityEngine;
using System.Collections;

/// <summary>
/// Farmer Mother young specific scripting values
/// </summary>
public class FarmerMotherYoung : NPC {	
	protected override void Init() {
		id = NPCIDs.FARMER_MOTHER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction NewDialogueReaction = new Reaction();
		NewDialogueReaction.AddAction (new NPCCallbackAction(UpdateEmotionState));
		flagReactions.Add(FlagStrings.ConversationInMiddleFarmerMother, NewDialogueReaction);
	}
	public void UpdateEmotionState(){
		this.currentEmotion.UpdateEmotionState();
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Talk to me later. I'm busy!"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	Schedule openningWaitingSchedule;
	Schedule postOpenningSchedule;

	protected override void SetUpSchedules(){
		//Wait for player to come before initiating Mother talking to Father
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		openningWaitingSchedule.Add(new TimeTask(30, new WaitTillPlayerCloseState(this, player)));
		//openningWaitingSchedule.Add(new TimeTask(10, new MoveThenDoState(this, new Vector3(transform.position.x + 1,transform.position.y, transform.position.z), new IdleState(this))));
		scheduleStack.Add(openningWaitingSchedule);
		
		//After talking to father about daughter
		postOpenningSchedule = new Schedule(this,Schedule.priorityEnum.High);
		postOpenningSchedule.Add(new TimeTask(10, new IdleState(this)));
		postOpenningSchedule.Add(new TimeTask(10, new MoveThenDoState(this, new Vector3(transform.position.x - 10,transform.position.y, transform.position.z), new IdleState(this))));
		postOpenningSchedule.Add(new TimeTask(30, new ChangeEmotionState(this, new PostOpenningConvoEmotionState(this, "What do you want? I got lots of work to do!"))));
		scheduleStack.Add(postOpenningSchedule);
		
		//Talks to father about daughter
		scheduleStack.Add(new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerFatherYoung), 
			new YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue(),Schedule.priorityEnum.High));
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			Debug.LogError("This is the wrong emotion state");
		}
	
	}
	#endregion
	
	#region Post-Openning Conversation Emotion State
	private class PostOpenningConvoEmotionState : EmotionState{
		NPC farmerMotherYoung;
		
		Reaction reactionToWhatAbout;
		Choice whatAboutChoice = new Choice("What was that about?", "You saw our little spat? Don't worry about it! " +
			"It's just some talk on how ta raise our daughter. Don't need ta fill her head with silly stories...");
		
		Choice growGardenChoice = new Choice("Yes", "Well then, ya should be well versed in how ta plant seeds." +
										"Here in exchange for tha pendant! I'll give ya these sunflower seeds. ");
		Choice dontGrowGardenChoice = new Choice("No", "Well, I can teach ya how if ya want ta! " +
			"Just find a mound of earth and put tha seeds you're carrying in it. Then just let time do its work."  +
				"Here in exchange for tha pendant! I'll give ya these sunflower seeds.");
		
		Choice tellOnDaughterChoice = new Choice ("Tell on daughter", "*Sigh* Thanks fer talking ta me about this.");
		bool updateConversationFarmerMotherYoungFlag = false;
		
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
	
		public PostOpenningConvoEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			farmerMotherYoung = toControl;
			
			reactionToWhatAbout = new Reaction(new NPCCallbackAction(askedWhatAbout));
			_allChoiceReactions.Add(whatAboutChoice, new DispositionDependentReaction(reactionToWhatAbout));
			
			Reaction reactionToGivePendant = new Reaction();
			reactionToGivePendant.AddAction(new NPCTakeItemAction(toControl));
			reactionToGivePendant.AddAction(new NPCCallbackAction(givePendant));
			
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(reactionToGivePendant));
			
			
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
			
		}
		public void UpdateMyMom(){
			
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
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
		}
		public void UpdateCanReadAndWork(){
			_allChoiceReactions.Remove(CanReadAndWorkChoice);
			_allChoiceReactions.Remove(SheShouldDecideChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
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
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
		}
		public void UpdateYouAreRight(){
			_allChoiceReactions.Remove(YouAreRightChoice);
			if(_allChoiceReactions.ContainsKey(NotSillyChoice)){
				_allChoiceReactions.Remove(NotSillyChoice);	
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Stop botherin me I got work ta do!");
		}
		
		
		
		
		
		
		public override void UpdateEmotionState(){
			if(updateConversationFarmerMotherYoungFlag == false){
				if(_allChoiceReactions.ContainsKey(StoriesFunChoice)){
					_allChoiceReactions.Add(StoriesHelpRelateChoice, new DispositionDependentReaction(StoriesHelpRelateReaction));
					Debug.LogError("Added the choice to Young Mother");
				}
				updateConversationFarmerMotherYoungFlag = true;
				Debug.LogError("Set ConversationFlag to true");
			}
			Debug.LogError("Called Update emotion state");
		}
			
		private void askedWhatAbout(){
			_allChoiceReactions.Remove(whatAboutChoice);
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
					
		
	
	}
	#endregion
	#endregion
}
