using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl young specific scripting values
/// </summary>
public class LighthouseGirlYoung : NPC {
	InitialEmotionState initialState;
	JesterEmotionState jesterState;
	Schedule TalkWithCastleman;
	protected override void Init() {
		id = NPCIDs.LIGHTHOUSE_GIRL;
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
	protected void setEmbarrased(){
		this.SetCharacterPortrait(StringsNPC.Embarassed);
	}
	protected void setHappy(){
		this.SetCharacterPortrait(StringsNPC.Smile);	
	}
	private NPCConvoSchedule tellOnLighthouseConversationSchedule;
	protected override void SetFlagReactions(){
		Reaction TellOnLighthouseReaction = new Reaction();
		TellOnLighthouseReaction.AddAction(new ShowOneOffChatAction(this, "Git over here girl.", 2f));
		TellOnLighthouseReaction.AddAction(new NPCAddScheduleAction(this, tellOnLighthouseConversationSchedule));
		flagReactions.Add(FlagStrings.TellOnLighthouseConversation, TellOnLighthouseReaction);
		
		Reaction ReactToCastleMan = new Reaction();
		ReactToCastleMan.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends , ReactToCastleMan);
		
		Reaction ReactToCastleManNotFriends = new Reaction();
		ReactToCastleManNotFriends.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleNOTFriends , ReactToCastleManNotFriends);
		
	}
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "So my mom wants me to learn how to cook...but I'm gonna grow up to be a great warrior, not a cook! Get some kind of cooked food and I'll reward you!");
		jesterState = new JesterEmotionState(this, "Your sibling says you are a court jester! i demand that you tell me stories!");
		return (initialState);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		tellOnLighthouseConversationSchedule = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
			new YoungFarmerMotherToLighthouseGirlToldOn(),Schedule.priorityEnum.High);
		TalkWithCastleman = new Schedule (this, Schedule.priorityEnum.High);
		TalkWithCastleman.Add(new TimeTask(3000, new WaitTillPlayerCloseState(this, player)));
		Task setFlag = (new Task(new MoveThenDoState(this, this.gameObject.transform.position, new MarkTaskDone(this))));
		setFlag.AddFlagToSet(FlagStrings.StartTalkingToLighthouse);
		TalkWithCastleman.Add(setFlag);
		//TalkWithCastleman.Add(new TimeTask(300, new WaitTillPlayerCloseState(this, player)));
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		Choice OwnChoresChoice = new Choice("Do your own chores", "Hmmmpphhh...Great warriors don't need help from other people!");
		Choice WhatDoYouNeedChoice = new Choice("What do you need?", "Apple pie! My mom wants me to learn how to cook or something stupid like that...");
		Choice SureChoice = new Choice("Sure", "My mom wants to bake apple pie! So give me some and we can pretend I baked it!");
		Reaction OwnChoresReaction, WhatDoYouNeedReaction, SureReaction;
		Choice GetAppleChoice = new Choice("I'll get you an apple", "Well...okay...that will help...");
		Reaction GaveApplePieReaction = new Reaction();
		Reaction GaveAppleReaction = new Reaction();
		NPC control;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			control = toControl;
			OwnChoresReaction = new Reaction(); 
			WhatDoYouNeedReaction = new Reaction(); 
			SureReaction = new Reaction();
			
			
			OwnChoresReaction.AddAction(new NPCCallbackAction(UpdateOwnChores));
			_allChoiceReactions.Add(OwnChoresChoice,new DispositionDependentReaction(OwnChoresReaction));
			
			WhatDoYouNeedReaction.AddAction(new NPCCallbackAction(UpdateWhatDoNeed));
			_allChoiceReactions.Add(WhatDoYouNeedChoice,new DispositionDependentReaction(WhatDoYouNeedReaction));
			
			SureReaction.AddAction(new NPCCallbackAction(UpdateSure));
			_allChoiceReactions.Add(SureChoice,new DispositionDependentReaction(SureReaction));
			
			
		}
		
		public override void UpdateEmotionState(){
			
		}
		
		public void UpdateOwnChores(){
			_allChoiceReactions.Remove(OwnChoresChoice);
			_allChoiceReactions.Remove(WhatDoYouNeedChoice);
			_allChoiceReactions.Remove(SureChoice);
			FlagManager.instance.SetFlag(FlagStrings.TellOnLighthouse);	
		}
		
		public void UpdateWhatDoNeed(){
			Reaction GetAppleReaction = new Reaction();
			GetAppleReaction.AddAction(new NPCCallbackAction(GetAnApple));
			_allChoiceReactions.Add(GetAppleChoice,new DispositionDependentReaction(GetAppleReaction));
			_allChoiceReactions.Remove(WhatDoYouNeedChoice);
			_allChoiceReactions.Remove(OwnChoresChoice);
			_allChoiceReactions.Remove(SureChoice);
			GUIManager.Instance.RefreshInteraction();
			
			
			FlagManager.instance.SetFlag(FlagStrings.TellOnLighthouse);
		}
		
		public void UpdateSure(){
			_allChoiceReactions.Remove(WhatDoYouNeedChoice);
			_allChoiceReactions.Remove(OwnChoresChoice);
			_allChoiceReactions.Remove(SureChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Have you gotten me that apple pie yet?");
			GaveApplePieReaction.AddAction(new NPCTakeItemAction(control));
			GaveApplePieReaction.AddAction(new NPCCallbackAction(GaveApplePieResponse));
			GaveApplePieReaction.AddAction(new UpdateCurrentTextAction(control, "Thanks! your the best!"));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(GaveApplePieReaction));
			
			
			FlagManager.instance.SetFlag(FlagStrings.TellOnLighthouse);
		}
		
		public void GetAnApple(){
			_allChoiceReactions.Remove(GetAppleChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("So you going to help me out with baking an apple pie?");
			GaveAppleReaction.AddAction(new NPCTakeItemAction(control));
			GaveAppleReaction.AddAction(new NPCCallbackAction(GaveAppleResponse));
			GaveAppleReaction.AddAction(new UpdateCurrentTextAction(control, "Thanks! Now to do this silly cooking thing...real warriors don't cook..."));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(GaveAppleReaction));
		}
		
		public void GaveApplePieResponse(){
			SetDefaultText("Thanks for the apple pie!");
		}
		
		public void GaveAppleResponse(){
			SetDefaultText("Thanks for the apple!");
		}
	}
	#endregion
	private class JesterEmotionState : EmotionState {
		Choice SureChoice = new Choice("Sure!", "Great! So where does it start?");
		Choice NoGoodChoice = new Choice("I'm no good", "My dad says anybody can tell stories if they want to!");
		Choice NoJesterChoice = new Choice("I'm no Jester!", "Fine! I don't need your silly stories!");
		Choice LongTimeChoice = new Choice("A long time ago...", "Ooooohhh!!! Where was it? A castle?");
		Choice OnceUponChoice = new Choice("Once upon a time...", "Oooohhh!!! Where was it? A castle?");
		Choice MyMomChoice = new Choice("My mom once said...", "That's a boring start my dad always says to start with Once Upong a Time...");
		
		Choice GalaxyChoice = new Choice("in a galaxy far away...", "What's a galaxy? You're making things up. You should practice your story telling you're pretty bad at it!");
		Choice CastleChoice = new Choice("There was a caste", "Ooh! What's in the castle?");
		Choice PrincessChoice = new Choice("There was a princess", "Princesses are boring..Make it a warrior!");
		Choice UhOnceUponChoice = new Choice("Uh..Once upon a time", "Oooohhh!!! Where was it? A castle?");
		Choice LittleGirlChoice = new Choice("There was this little girl", "This is so terrible I have nothing to say.");
		Reaction SureReaction = new Reaction();
		Reaction NoGoodReaction = new Reaction();
		Reaction NoJesterReaction = new Reaction();
		Reaction LongTimeReaction = new Reaction();
		Reaction OnceUponReaction = new Reaction();
		Reaction MyMomReaction = new Reaction();
		Reaction GalaxyReaction = new Reaction();
		Reaction CastleReaction = new Reaction();
		Reaction PrincessReaction = new Reaction();
		Reaction UhOnceUponReaction = new Reaction();
		Reaction LittleGirlReaction = new Reaction();
		
		
		Choice ThereWasAWarriorChoice;
		Reaction ThereWasAWarriorReaction;
		Choice ImSupposedToTellItChoice;
		Reaction ImSupposedToTellItReaction;
		Choice ButIChoice;
		Reaction ButIReaction;
		Choice GrumbleChoice;
		Reaction GrumbleReaction;
		Choice LaLaLaLaChoice;
		Reaction LaLaLaLaReaction;
		Choice YouArentDoneYetChoice;
		Reaction YouArentDoneYetReaction;
		Choice ThisIsBoringChoice;
		Reaction ThisIsBoringReaction;
		Choice SayNothingOneChoice;
		Reaction SayNothingOneReaction;
		Choice SayNothingTwoChoice;
		Reaction SayNothingTwoReaction;
		Choice HappilyEverAfterChoice;
		Reaction HappilyEverAfterReaction;
		
		public JesterEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
			SureReaction.AddAction(new NPCCallbackAction(SureResponse));
			_allChoiceReactions.Add(SureChoice,new DispositionDependentReaction(SureReaction));
			
			_allChoiceReactions.Add(NoGoodChoice,new DispositionDependentReaction(NoGoodReaction));
			
			_allChoiceReactions.Add(NoJesterChoice,new DispositionDependentReaction(NoJesterReaction));
		
		}
		
		public void SureResponse(){
			_allChoiceReactions.Remove(SureChoice);
			_allChoiceReactions.Remove(NoGoodChoice);
			_allChoiceReactions.Remove(NoJesterChoice);
			
			MyMomReaction.AddAction(new NPCCallbackAction(MyMomResponse));
			_allChoiceReactions.Add(MyMomChoice,new DispositionDependentReaction(MyMomReaction));
			
			OnceUponReaction.AddAction(new NPCCallbackAction(OnceUponResponse));
			_allChoiceReactions.Add(OnceUponChoice,new DispositionDependentReaction(OnceUponReaction));
			
			LongTimeReaction.AddAction(new NPCCallbackAction(LongTimeResponse));
			_allChoiceReactions.Add(LongTimeChoice,new DispositionDependentReaction(LongTimeReaction));
			
			SetDefaultText("I'm still waiting for that story!");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void LongTimeResponse(){
			_allChoiceReactions.Remove(LongTimeChoice);
			_allChoiceReactions.Remove(OnceUponChoice);
			_allChoiceReactions.Remove(MyMomChoice);
			
			PrincessReaction.AddAction(new NPCCallbackAction(PrincessResponse));
			_allChoiceReactions.Add(PrincessChoice,new DispositionDependentReaction(PrincessReaction));
			
			CastleReaction.AddAction(new NPCCallbackAction(CastleResponse));
			_allChoiceReactions.Add(CastleChoice,new DispositionDependentReaction(CastleReaction));
			
			GalaxyReaction.AddAction(new NPCCallbackAction(GalaxyResponse));
			_allChoiceReactions.Add(GalaxyChoice,new DispositionDependentReaction(GalaxyReaction));
			
			SetDefaultText("What happened long ago?");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void OnceUponResponse(){
			_allChoiceReactions.Remove(LongTimeChoice);
			_allChoiceReactions.Remove(OnceUponChoice);
			_allChoiceReactions.Remove(MyMomChoice);
			
			_allChoiceReactions.Remove(UhOnceUponChoice);
			_allChoiceReactions.Remove(LittleGirlChoice);
			
			
			PrincessReaction.AddAction(new NPCCallbackAction(PrincessResponse));
			_allChoiceReactions.Add(PrincessChoice,new DispositionDependentReaction(PrincessReaction));
			
			SetDefaultText("Once upon a time...then what?");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void MyMomResponse(){
			_allChoiceReactions.Remove(LongTimeChoice);
			_allChoiceReactions.Remove(OnceUponChoice);
			_allChoiceReactions.Remove(MyMomChoice);
			
			UhOnceUponReaction.AddAction(new NPCCallbackAction(OnceUponResponse));
			_allChoiceReactions.Add(UhOnceUponChoice,new DispositionDependentReaction(UhOnceUponReaction));
			
			LittleGirlReaction.AddAction(new NPCCallbackAction(LittleGirlResponse));
			_allChoiceReactions.Add(LittleGirlChoice,new DispositionDependentReaction(LittleGirlReaction));

			SetDefaultText("Bad..just bad!");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void GalaxyResponse(){
			_allChoiceReactions.Remove(GalaxyChoice);
			_allChoiceReactions.Remove(CastleChoice);
			_allChoiceReactions.Remove(PrincessChoice);
			
			SetDefaultText("You should practice your story telling you're pretty bad at it!");
			GUIManager.Instance.RefreshInteraction();
		}
		public void CastleResponse(){
			_allChoiceReactions.Remove(GalaxyChoice);
			_allChoiceReactions.Remove(CastleChoice);
			_allChoiceReactions.Remove(PrincessChoice);
			
			SetDefaultText("So, whats in the castle?");
			GUIManager.Instance.RefreshInteraction();
		}
		public void PrincessResponse(){
			_allChoiceReactions.Remove(GalaxyChoice);
			_allChoiceReactions.Remove(CastleChoice);
			_allChoiceReactions.Remove(PrincessChoice);
			SetDefaultText("Switch the Princess to a warrior!");
			GUIManager.Instance.RefreshInteraction();
		}
		public void LittleGirlResponse(){
			_allChoiceReactions.Remove(UhOnceUponChoice);
			_allChoiceReactions.Remove(LittleGirlChoice);
			SetDefaultText("Uhmm, I don't know what to say...");
			GUIManager.Instance.RefreshInteraction();
		}
	}
	#endregion
}
