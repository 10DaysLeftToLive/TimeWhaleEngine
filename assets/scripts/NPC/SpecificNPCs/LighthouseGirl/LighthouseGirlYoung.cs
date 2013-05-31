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
	public void setAngry(){
		this.SetCharacterPortrait(StringsNPC.Angry);
	}
	public void setSad(){
		this.SetCharacterPortrait(StringsNPC.Sad);
	}
	public void setBlink(){
		this.SetCharacterPortrait(StringsNPC.Blink);
	}
	public void setDefault(){
		this.SetCharacterPortrait(StringsNPC.Default);
	}
	public void setEmbarrased(){
		this.SetCharacterPortrait(StringsNPC.Embarassed);
	}
	public void setHappy(){
		this.SetCharacterPortrait(StringsNPC.Smile);	
	}
	private NPCConvoSchedule tellOnLighthouseConversationSchedule;
	protected override void SetFlagReactions(){
		Reaction TellOnLighthouseReaction = new Reaction();
		TellOnLighthouseReaction.AddAction(new ShowOneOffChatAction(this, "Git over here girl.", 2f));
		TellOnLighthouseReaction.AddAction(new NPCAddScheduleAction(this, tellOnLighthouseConversationSchedule));
		flagReactions.Add(FlagStrings.TellOnLighthouseConversation, TellOnLighthouseReaction);
		
		#region Castle man
		Reaction ReactToCastleMan = new Reaction();
		ReactToCastleMan.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleFriends , ReactToCastleMan);
		
		Reaction ReactToCastleManNotFriends = new Reaction();
		ReactToCastleManNotFriends.AddAction(new NPCAddScheduleAction(this, TalkWithCastleman));
		flagReactions.Add(FlagStrings.PlayerAndCastleNOTFriends , ReactToCastleManNotFriends);
		#endregion
		
	}
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "So my mom wants me to learn how to cook...but I'm gonna grow up to be a great warrior, not a cook! Get some kind of cooked food and I'll reward you!");
		jesterState = new JesterEmotionState(this, "Your sibling says you are a court jester! i demand that you tell me stories!");
		return (initialState);
	}
	
	protected override Schedule GetSchedule(){
		//Schedule schedule = new DefaultSchedule(this);
		return (InitialSchedule);
	}
	Schedule InitialSchedule;
	protected override void SetUpSchedules(){
		InitialSchedule = new Schedule(this, Schedule.priorityEnum.Medium);
		InitialSchedule.Add(new TimeTask(1500, new WaitTillPlayerCloseState(this, player)));
		InitialSchedule.Add(new Task(new IdleState(this), this, 0.1f, "Psst!  Come over here!"));
		
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
		NPC control;
		string appleString = "An apple...I can use that to cook...but a full pie would be better.";
		Choice MyMomIsTooChoice;
		
		
		Reaction MyMomIsTooReaction;
			
		Choice EvilPlanChoice;
			Choice AboutThatRewardChoice;
			Choice HowDoWeStopHerChoice;
				Choice NoDoYourChoresChoice;
					Choice OkayFineChoice;
					Choice NoChoice;
						Choice NoMeansNoChoice;
						Choice OkayOneAppleChoice;
						Choice ImTellingOnYouChoice;
				Choice OnItChoice;
				Choice AreYouCrazyChoice;
			Choice StopBeingLazyChoice;
				Choice ImTellingChoice;
					Choice OkayIGuessChoice;
					Choice NoImTellingChoice;
				Choice ThisIsSilllyChoice;
				Choice WarriorsCookChoice;
				
		Choice ImGonnaTellNowChoice;
		Reaction ImGonnaTellNowReaction;
		
		Reaction EvilPlanReaction;
		Reaction AboutThatRewardReaction;
		Reaction HowDoWeStopHerReaction;
		Reaction StopBeingLazyReaction;
		Reaction ImTellingReaction;
		Reaction OkayIGuessReaction;
		Reaction NoImTellingReaction;
		Reaction ThisIsSillyReaction;
		Reaction WarriorsCookReaction;
		Reaction NoDoYourChoresReaction;
		Reaction OnItReaction;
		Reaction AreYouCrazyReaction;
		Reaction OkayFineReaction;
		Reaction NoReaction;
		Reaction NoMeansNoReaction;
		Reaction OkayOneAppleReaction;
		Reaction ImTellingOnYouReaction;
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "My mom is a slave driver!  Help me defeat her evil plan and I will reward you noble knight!"){
			control = toControl;
			MyMomIsTooChoice = new Choice ("My mom is too!", "Yeah its horrible!");
			
			EvilPlanChoice = new Choice ("What evil plan?", "She wants to drain my strength through constant chores!\n But I'm a great warrior and can see through her cunning schemes to make me cook!");
			EvilPlanReaction = new Reaction ();
			EvilPlanReaction.AddAction(new NPCCallbackAction(UpdateEvilPlan));
			EvilPlanReaction.AddAction(new UpdateCurrentTextAction(toControl, "She wants to drain my strength through constant chores!\n But I'm a great warrior and can see through her cunning schemes to make me cook!"));
			_allChoiceReactions.Add(EvilPlanChoice, new DispositionDependentReaction(EvilPlanReaction));
			
				StopBeingLazyChoice = new Choice ("Stop being lazy!", "");
				StopBeingLazyReaction = new Reaction ();
				StopBeingLazyReaction.AddAction(new NPCCallbackAction(UpdateStopBeingLazy));
				StopBeingLazyReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
				
					ImTellingChoice = new Choice ("I'm telling!", "");
					ImTellingReaction = new Reaction ();
					ImTellingReaction.AddAction(new NPCCallbackAction(UpdateImTelling));
					ImTellingReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
			
						OkayIGuessChoice = new Choice ("That's okay, I guess...", "");
						OkayIGuessReaction = new Reaction();
						OkayIGuessReaction.AddAction(new NPCCallbackAction(UpdateOkayIGuess));
						OkayIGuessReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
						
						NoImTellingChoice = new Choice ("No, I'm telling!", "Fine...DAD!  HELP!  I'M BEING BULLIED!");
						NoImTellingReaction = new Reaction();
						NoImTellingReaction.AddAction(new NPCCallbackAction(UpdateNoImTelling));
						NoImTellingReaction.AddAction(new ShowOneOffChatAction(toControl, "Fine...DAD!  HELP!  I'M BEING BULLIED!"));
					
					ThisIsSilllyChoice = new Choice("This is silly.", "Hmmph!  I don't need  you!  On your way peasant!");
					ThisIsSillyReaction = new Reaction();
					ThisIsSillyReaction.AddAction(new NPCCallbackAction(UpdateThisIsSilly));
					ThisIsSillyReaction.AddAction(new ShowOneOffChatAction(toControl, "Hmmph!  I don't need  you!  On your way peasant!"));
					
					WarriorsCookChoice = new Choice ("Warrior's cook!", "What?!?  That's not true!  Warriors get food by saving the village and having a banquet in their honor!");
					WarriorsCookReaction = new Reaction();
					WarriorsCookReaction.AddAction(new NPCCallbackAction(UpdateWarriorsCook));
					WarriorsCookReaction.AddAction(new UpdateCurrentTextAction(toControl, "What?!?  That's not true!  Warriors get food by saving the village and having a banquet in their honor!"));
			
				AboutThatRewardChoice = new Choice("About that reward...", "");
				AboutThatRewardReaction = new Reaction();
				AboutThatRewardReaction.AddAction(new NPCCallbackAction(UpdateAboutThatReward));
				AboutThatRewardReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
				
				HowDoWeStopHerChoice = new Choice ("How do we stop her?", "Get me some apple pie...preferrably poisoned...so that I don't have to cook it myself!");
				HowDoWeStopHerReaction = new Reaction();
				HowDoWeStopHerReaction.AddAction(new NPCCallbackAction(UpdateStopHer));
				HowDoWeStopHerReaction.AddAction(new UpdateCurrentTextAction(toControl, "Get me some apple pie...preferrably poisoned...so that I don't have to cook it myself!"));
			
					NoDoYourChoresChoice = new Choice("No do your chores.", "Please, just help me out this one time!  I want to play on the beach, and I can't do that if I have to bake!");
					NoDoYourChoresReaction = new Reaction();
					NoDoYourChoresReaction.AddAction(new NPCCallbackAction(UpdateDoYourOwnChores));
					NoDoYourChoresReaction.AddAction(new UpdateCurrentTextAction(toControl, "Please, just help me out this one time!  I want to play on the beach, and I can't do that if I have to bake!"));
			
						NoChoice = new Choice("No!", "Okay...fine no apple pie.  Just get me an apple. Pllleeeaaassee!");
						NoReaction = new Reaction();
						NoReaction.AddAction(new NPCCallbackAction(UpdateNo));
						NoReaction.AddAction(new UpdateCurrentTextAction(toControl, "Okay...fine no apple pie.  Just get me an apple. Pllleeeaaassee!"));
					
							NoMeansNoChoice = new Choice("No means no!", "Hmmph!  I don't need  you!  On your way peasant!");
							NoMeansNoReaction = new Reaction();
							NoMeansNoReaction.AddAction(new NPCCallbackAction(UpdateNoMeansNo));
							NoMeansNoReaction.AddAction(new ShowOneOffChatAction(toControl, "Hmmph!  I don't need  you!  On your way peasant!"));
			
							OkayOneAppleChoice = new Choice("Okay. One Apple.", "Then go on your noble quest!");
							OkayOneAppleReaction = new Reaction();
							OkayOneAppleReaction.AddAction(new NPCCallbackAction(UpdateOkayOneMoreApple));
							OkayOneAppleReaction.AddAction(new ShowOneOffChatAction(toControl, "Then go on your noble quest!"));
			
							ImTellingOnYouChoice = new Choice ("I'm telling on you!", "No!  I'd be grounded for weeks!  Please, Please, Please don't do it!");
							ImTellingOnYouReaction = new Reaction();
							ImTellingOnYouReaction.AddAction(new NPCCallbackAction(UpdateTellingOnYou));
							ImTellingOnYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "No!  I'd be grounded for weeks!  Please, Please, Please don't do it!"));
				
						OkayFineChoice = new Choice("Okay..Fine", "Then onwards!  For apple pie!");
						OkayFineReaction = new Reaction();
						OkayFineReaction.AddAction(new NPCCallbackAction(UpdateOkayFine));
						OkayFineReaction.AddAction(new ShowOneOffChatAction(toControl, "Then onwards!  For apple pie!"));		
			
					OnItChoice = new Choice("On it!", "Hurry, with the pie I can stop here evil scheme!");
					OnItReaction = new Reaction();
					OnItReaction.AddAction(new NPCCallbackAction(UpdateOnIt));
					OnItReaction.AddAction(new ShowOneOffChatAction(toControl, "Hurry, with the pie I can stop here evil scheme!"));
					
					AreYouCrazyChoice = new Choice("", "");
					AreYouCrazyReaction = new Reaction();
					AreYouCrazyReaction.AddAction(new NPCCallbackAction(UpdateAreYouCrazy));
					AreYouCrazyReaction.AddAction(new UpdateCurrentTextAction(toControl, ""));
				
			ImGonnaTellNowChoice = new Choice("I'm telling now!", "");
			ImGonnaTellNowReaction = new Reaction();
			ImGonnaTellNowReaction.AddAction(new NPCCallbackAction(UpdateImTellingNow));
			ImGonnaTellNowReaction.AddAction(new ShowOneOffChatAction(toControl, ""));
		}
		public void UpdateEvilPlan (){
			_allChoiceReactions.Remove(EvilPlanChoice);
			_allChoiceReactions.Remove(MyMomIsTooChoice);
			_allChoiceReactions.Add(StopBeingLazyChoice, new DispositionDependentReaction(StopBeingLazyReaction));
			_allChoiceReactions.Add(AboutThatRewardChoice, new DispositionDependentReaction(AboutThatRewardReaction));
			_allChoiceReactions.Add(HowDoWeStopHerChoice, new DispositionDependentReaction(HowDoWeStopHerReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("My mom...is...draining...my strength...please...help!");
		}
				public void UpdateStopHer(){
					_allChoiceReactions.Remove(HowDoWeStopHerChoice);
					_allChoiceReactions.Remove(StopBeingLazyChoice);
					if(_allChoiceReactions.ContainsKey(AboutThatRewardChoice)){
						_allChoiceReactions.Remove(AboutThatRewardChoice);	
					}
					_allChoiceReactions.Add(NoDoYourChoresChoice, new DispositionDependentReaction(NoDoYourChoresReaction));
					_allChoiceReactions.Add(OnItChoice, new DispositionDependentReaction(OnItReaction));
					_allChoiceReactions.Add(AreYouCrazyChoice, new DispositionDependentReaction(AreYouCrazyReaction));
					GUIManager.Instance.RefreshInteraction();
					SetDefaultText("Help me cook to stop her evil plan!");
				}
						public void UpdateOnIt(){
							_allChoiceReactions.Remove(OnItChoice);
							_allChoiceReactions.Remove(NoDoYourChoresChoice);
							_allChoiceReactions.Remove(AreYouCrazyChoice);	
							GUIManager.Instance.CloseInteractionMenu();
							SetDefaultText("Hurry!  I need you to get apple pie!");
						}
						public void UpdateDoYourOwnChores(){
							_allChoiceReactions.Remove(OnItChoice);
							_allChoiceReactions.Remove(NoDoYourChoresChoice);
							_allChoiceReactions.Remove(AreYouCrazyChoice);	
							_allChoiceReactions.Add(NoChoice, new DispositionDependentReaction(NoReaction));
							_allChoiceReactions.Add(OkayFineChoice, new DispositionDependentReaction(OkayFineReaction));
							GUIManager.Instance.RefreshInteraction();
							SetDefaultText("");
						}
								public void UpdateOkayFine(){
									
								}
								public void UpdateNo(){
									_allChoiceReactions.Remove(NoChoice);
									_allChoiceReactions.Remove(OkayFineChoice);
									_allChoiceReactions.Add(NoMeansNoChoice, new DispositionDependentReaction(NoMeansNoReaction));
									_allChoiceReactions.Add(OkayOneAppleChoice, new DispositionDependentReaction(OkayOneAppleReaction));
									//Add anotherone here.
									GUIManager.Instance.RefreshInteraction();
									SetDefaultText("");
								}
										public void UpdateOkayOneMoreApple(){
											
										}
										public void UpdateNoMeansNo(){
											
										}
										public void UpdateTellingOnYou(){
			
										}
				public void UpdateAreYouCrazy(){
			
				}
			public void UpdateAboutThatReward (){
				_allChoiceReactions.Remove(AboutThatRewardChoice);
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("");
			}
			public void UpdateStopBeingLazy (){
				_allChoiceReactions.Remove(StopBeingLazyChoice);
				_allChoiceReactions.Remove(AboutThatRewardChoice);
				_allChoiceReactions.Remove(HowDoWeStopHerChoice);
				_allChoiceReactions.Add(ImTellingChoice, new DispositionDependentReaction(ImTellingReaction));
				_allChoiceReactions.Add(ThisIsSilllyChoice, new DispositionDependentReaction(ThisIsSillyReaction));
				_allChoiceReactions.Add(WarriorsCookChoice, new DispositionDependentReaction(WarriorsCookReaction));
				//control
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Hmmph.  I'm never lazy!");
			}
				public void UpdateImTelling (){
					_allChoiceReactions.Remove(ImTellingChoice);
					_allChoiceReactions.Remove(ThisIsSilllyChoice);
					_allChoiceReactions.Remove(WarriorsCookChoice);
					GUIManager.Instance.CloseInteractionMenu();
					SetDefaultText("");
				}
						public void UpdateOkayIGuess(){
				
						}
						public void UpdateNoImTelling(){
				
						}
				public void UpdateThisIsSilly (){
					_allChoiceReactions.Remove(ImTellingChoice);
					_allChoiceReactions.Remove(ThisIsSilllyChoice);
					_allChoiceReactions.Remove(WarriorsCookChoice);
					GUIManager.Instance.CloseInteractionMenu();
					SetDefaultText("On your way peasant!");
				}
				public void UpdateWarriorsCook(){
					
				}
		public void UpdateImTellingNow(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.CloseInteractionMenu();
			SetDefaultText("My dad told you to leave me alone!");
			//flag here
		}
		public override void UpdateEmotionState(){
			
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
