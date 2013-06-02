using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl young specific scripting values
/// </summary>
public class LighthouseGirlYoung : NPC {
	InitialEmotionState initialState;
	JesterEmotionState jesterState;
	Schedule TalkWithCastleman;
	Schedule WalkToBeach;
	Schedule GaveApple;
	Schedule GaveNothingSchedule;
	NPCConvoSchedule LighthouseGoingToBeach;
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
	private NPCConvoSchedule AttemptToTellOnLighthouse;
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
		
		Reaction CounterTellOn = new Reaction();
		CounterTellOn.AddAction(new NPCAddScheduleAction(this, AttemptToTellOnLighthouse));
		flagReactions.Add(FlagStrings.CounterTellOn, CounterTellOn);
		
		Reaction ReadyToGoToBeach = new Reaction();
		ReadyToGoToBeach.AddAction(new NPCAddScheduleAction(this, WalkToBeach));
		ReadyToGoToBeach.AddAction(new NPCAddScheduleAction(this, LighthouseGoingToBeach));
		ReadyToGoToBeach.AddAction(new NPCEmotionUpdateAction(this, new AtBeachState(this, "")));
		flagReactions.Add(FlagStrings.GoDownToBeach, ReadyToGoToBeach);
		
		Reaction MakingApplePieFromApple = new Reaction();
		MakingApplePieFromApple.AddAction(new NPCAddScheduleAction(this, GaveApple));
		flagReactions.Add(FlagStrings.MakePieFromApple, MakingApplePieFromApple);
		
		
		
		Reaction MakingApplePieFromScratch = new Reaction();
		MakingApplePieFromApple.AddAction(new NPCAddScheduleAction(this, GaveApple));
		flagReactions.Add(FlagStrings.MakePieFromScratch, MakingApplePieFromScratch);
	}
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "So my mom wants me to learn how to cook...but I'm gonna grow up to be a great warrior, not a cook! Get some kind of cooked food and I'll reward you!");
		jesterState = new JesterEmotionState(this, "Your sibling says you are a court jester! i demand that you tell me stories!");
		return (initialState);
	}
	
	protected override Schedule GetSchedule(){
		//Schedule schedule = new DefaultSchedule(this);
		return (GaveApple);
	}
	Schedule InitialSchedule;
	protected override void SetUpSchedules(){
		InitialSchedule = new Schedule(this, Schedule.priorityEnum.Medium);
		InitialSchedule.Add(new TimeTask(1500, new WaitTillPlayerCloseState(this, player)));
		InitialSchedule.Add(new Task(new IdleState(this), this, 0.1f, "Psst!  Come over here!"));
		
		AttemptToTellOnLighthouse = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerFatherYoung), 
			new LightHouseToFarmerFather(),Schedule.priorityEnum.DoConvo);
		AttemptToTellOnLighthouse.SetCanNotInteractWithPlayer();
		tellOnLighthouseConversationSchedule = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
			new YoungFarmerMotherToLighthouseGirlToldOn(),Schedule.priorityEnum.DoConvo);
		
		Task SetFlagToBeach = (new Task(new MoveThenDoState(this, this.gameObject.transform.position, new MarkTaskDone(this))));
		SetFlagToBeach.AddFlagToSet(FlagStrings.GoDownToBeach);
		
		TalkWithCastleman = new Schedule (this, Schedule.priorityEnum.High);
		TalkWithCastleman.Add(new TimeTask(3000, new WaitTillPlayerCloseState(this, player)));
		Task setFlag = (new Task(new MoveThenDoState(this, this.gameObject.transform.position, new MarkTaskDone(this))));
		setFlag.AddFlagToSet(FlagStrings.StartTalkingToLighthouse);
		TalkWithCastleman.Add(setFlag);
		
		GaveApple = new Schedule(this, Schedule.priorityEnum.High);
		GaveApple.Add(new TimeTask(500f, new IdleState(this)));
		GaveApple.Add(SetFlagToBeach);
		
		GaveNothingSchedule = new Schedule(this, Schedule.priorityEnum.High);
		GaveNothingSchedule.Add(new TimeTask(750f, new IdleState(this)));
		GaveNothingSchedule.Add(SetFlagToBeach);
		
		WalkToBeach = new Schedule(this, Schedule.priorityEnum.DoNow);
		WalkToBeach.Add(new Task(new MoveThenMarkDoneState(this, MapLocations.MiddleOfBeachYoung)));
		
		LighthouseGoingToBeach = new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerMotherYoung), 
			new LighthouseToFarmerMother(),Schedule.priorityEnum.DoConvo);
	}
	
	
	#region EmotionStates
	private class AtBeachState: EmotionState{
		public AtBeachState(NPC toControl, string currentDialogue) : base(toControl, "Don't bother me I'm off to build sand castles to protect the island!"){
		}
	}
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		NPC control;
		string appleString = "An apple...I can use that to cook...but a full pie would be better.";
		string pieString = "A pie!  Perfect!  How did you know I needed that to dodge my chores?";
		Reaction TakeAppleReaction;
		Reaction DenyAppleReaction;
		Reaction TakePieReaction;
		
		
		Choice EvilPlanChoice;
			Choice HowDoWeStopHerChoice;
				Choice NoDoYourChoresChoice;
					Choice OkayFineChoice;
					Choice NoChoice;
						Choice NoMeansNoChoice;
						Choice OkayOneAppleChoice;
						Choice ImTellingOnYouChoice;
							Choice ThisOneTimeChoice;
								Choice SighYouWinChoice;
								Choice NOOChoice;
							Choice ImGonnaTellNowChoice;
				Choice OnItChoice;
			Choice StopBeingLazyChoice;
				Choice ImTellingChoice;
					Choice OkayIGuessChoice;
					Choice NoImTellingChoice;
				Choice ThisIsSilllyChoice;
				Choice WarriorsCookChoice;
					Choice FairEnoughChoice;
					Choice WhatIfYouTravelChoice;
				
		
		Reaction ImGonnaTellNowReaction;
		Reaction ThisOneTimeReaction;
		Reaction NOOReaction;
		Reaction SighYouWinReaction;
		Reaction EvilPlanReaction;
		Reaction HowDoWeStopHerReaction;
		Reaction StopBeingLazyReaction;
		Reaction ImTellingReaction;
		Reaction OkayIGuessReaction;
		Reaction NoImTellingReaction;
		Reaction ThisIsSillyReaction;
		Reaction WarriorsCookReaction;
		Reaction NoDoYourChoresReaction;
		Reaction OnItReaction;
		Reaction OkayFineReaction;
		Reaction NoReaction;
		Reaction NoMeansNoReaction;
		Reaction OkayOneAppleReaction;
		Reaction ImTellingOnYouReaction;
		Reaction WhatIfYouTravelReaction;
		Reaction FairEnoughReaction;
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "My mom is a slave driver!  Help me defeat her evil plan and I will reward you noble knight!"){
			control = toControl;
			TakePieReaction = new Reaction();
			TakePieReaction.AddAction(new NPCTakeItemAction(toControl));
			TakePieReaction.AddAction(new NPCCallbackAction(UpdateTakeApplePie));
			_allItemReactions.Add(StringsItem.ApplePie, new DispositionDependentReaction(TakePieReaction));
			
			TakeAppleReaction = new Reaction();
			TakeAppleReaction.AddAction(new NPCTakeItemAction(toControl));
			TakeAppleReaction.AddAction(new NPCCallbackAction(UpdateTakeApple));
			
			DenyAppleReaction = new Reaction();
			DenyAppleReaction.AddAction(new NPCCallbackAction(UpdateDenyApple));
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(TakePieReaction));
			
			
			EvilPlanChoice = new Choice ("What evil plan?", "She wants to drain my strength through constant chores!\n But I'm a great warrior and can see through her cunning schemes to make me cook!");
			EvilPlanReaction = new Reaction ();
			EvilPlanReaction.AddAction(new NPCCallbackAction(UpdateEvilPlan));
			EvilPlanReaction.AddAction(new UpdateCurrentTextAction(toControl, "She wants to drain my strength through constant chores!\n But I'm a great warrior and can see through her cunning schemes to make me cook!"));
			_allChoiceReactions.Add(EvilPlanChoice, new DispositionDependentReaction(EvilPlanReaction));
			
				StopBeingLazyChoice = new Choice ("Stop being lazy!", "I'm not lazy!  I just have better things to do, like build sand castles on the beach.  Someone needs to prepare for an invasion of the island!");
				StopBeingLazyReaction = new Reaction ();
				StopBeingLazyReaction.AddAction(new NPCCallbackAction(UpdateStopBeingLazy));
				StopBeingLazyReaction.AddAction(new UpdateCurrentTextAction(toControl, "I'm not lazy!  I just have better things to do, like build sand castles on the beach.  Someone needs to prepare for an invasion of the island!"));
				
					ImTellingChoice = new Choice ("I'm telling!", "NO!  Please don't!  I promise I'll be good!  Just don't tell on me!");
					ImTellingReaction = new Reaction ();
					ImTellingReaction.AddAction(new NPCCallbackAction(UpdateImTelling));
					ImTellingReaction.AddAction(new UpdateCurrentTextAction(toControl, "NO!  Please don't!  I promise I'll be good!  Just don't tell on me!"));
			
						OkayIGuessChoice = new Choice ("That's okay, I guess...", "Yay!  So about that apple pie?  No?  Okay fine...");
						OkayIGuessReaction = new Reaction();
						OkayIGuessReaction.AddAction(new NPCCallbackAction(UpdateOkayIGuess));
						OkayIGuessReaction.AddAction(new ShowOneOffChatAction(toControl, "Yay!  So about that apple pie?  No?  Okay fine..."));
						
						NoImTellingChoice = new Choice ("No, I'm telling!", "Fine...DAD!  HELP!  I'M BEING BULLIED!");
						NoImTellingReaction = new Reaction();
						NoImTellingReaction.AddAction(new NPCCallbackAction(UpdateNoImTelling));
					
					ThisIsSilllyChoice = new Choice("This is silly.", "Hmmph!  I don't need  you!  On your way peasant!");
					ThisIsSillyReaction = new Reaction();
					ThisIsSillyReaction.AddAction(new NPCCallbackAction(UpdateThisIsSilly));
					ThisIsSillyReaction.AddAction(new ShowOneOffChatAction(toControl, "Hmmph!  I don't need  you!  On your way peasant!"));
					
					WarriorsCookChoice = new Choice ("Warrior's cook!", "What?!?  That's not true!  Warriors get food by saving the village and having a banquet in their honor!");
					WarriorsCookReaction = new Reaction();
					WarriorsCookReaction.AddAction(new NPCCallbackAction(UpdateWarriorsCook));
					WarriorsCookReaction.AddAction(new UpdateCurrentTextAction(toControl, "What?!?  That's not true!  Warriors get food by saving the village and having a banquet in their honor!"));
				
						WhatIfYouTravelChoice = new Choice ("What if you travel?", "Well...I guess I would need to know how to cook if I got lost in a mountain chasing a villain...");
						WhatIfYouTravelReaction = new Reaction();
						WhatIfYouTravelReaction.AddAction(new NPCCallbackAction(UpdateWhatIfYouTravel));
						WhatIfYouTravelReaction.AddAction(new ShowOneOffChatAction(toControl, "Well...I guess I would need to know how to cook if I got lost in a mountain chasing a villain...I guess I'll do it."));
					
						FairEnoughChoice = new Choice ("Fair Enough", "Good to see you agree!  Now aquire apple pie for me!");
						FairEnoughReaction = new Reaction();
						FairEnoughReaction.AddAction(new NPCCallbackAction(UpdateFairEnough));
						FairEnoughReaction.AddAction(new ShowOneOffChatAction(toControl, "Good to see you agree!  Now aquire apple pie for me!"));
					
				HowDoWeStopHerChoice = new Choice ("How do we stop her?", "Get me some apple pie...so that I don't have to cook it myself!");
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
			
							OkayOneAppleChoice = new Choice("Okay. One Apple.", "Then go forth on your noble quest!");
							OkayOneAppleReaction = new Reaction();
							OkayOneAppleReaction.AddAction(new NPCCallbackAction(UpdateOkayOneMoreApple));
							OkayOneAppleReaction.AddAction(new ShowOneOffChatAction(toControl, "Then go on your noble quest!"));
			
							ImTellingOnYouChoice = new Choice ("I'm telling on you!", "No!  I'd be grounded for weeks!  Please, Please, Please don't do it!");
							ImTellingOnYouReaction = new Reaction();
							ImTellingOnYouReaction.AddAction(new NPCCallbackAction(UpdateTellingOnYou));
							ImTellingOnYouReaction.AddAction(new UpdateCurrentTextAction(toControl, "No!  I'd be grounded for weeks!  Please, Please, Please don't do it!"));
				
								ThisOneTimeChoice = new Choice ("This one time...", "Thank you thank you!  So about that apple?");
								ThisOneTimeReaction = new Reaction();
								ThisOneTimeReaction.AddAction(new NPCCallbackAction(UpdateThisOneTime));
								ThisOneTimeReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thank you thank you!  So about that apple?"));
								
									SighYouWinChoice = new Choice ("*Sigh* You win.", "Yes!  Now Sir Knight, go out and findeth the apple!");
									SighYouWinReaction = new Reaction();
									SighYouWinReaction.AddAction(new NPCCallbackAction(UpdateSighYouWin));
									SighYouWinReaction.AddAction(new ShowOneOffChatAction(toControl, "Yes!  Now Sir Knight, go out and findeth the apple!"));
												
									NOOChoice = new Choice ("NO!", "Okay fine.  Be that way, see if I care!");
									NOOReaction = new Reaction();
									NOOReaction.AddAction(new NPCCallbackAction(UpdateNOO));
									NOOReaction.AddAction(new ShowOneOffChatAction(toControl, "Okay fine.  Be that way, see if I care!"));
								
								ImGonnaTellNowChoice = new Choice("I'm telling now!", "Fine...DAD!  HELP!  I'M BEING BULLIED!");
								ImGonnaTellNowReaction = new Reaction();
								ImGonnaTellNowReaction.AddAction(new NPCCallbackAction(UpdateImTellingNow));
			
						OkayFineChoice = new Choice("Okay..Fine", "Then onwards!  For apple pie!");
						OkayFineReaction = new Reaction();
						OkayFineReaction.AddAction(new NPCCallbackAction(UpdateOkayFine));
						OkayFineReaction.AddAction(new ShowOneOffChatAction(toControl, "Then onwards!  For apple pie!"));		
			
					OnItChoice = new Choice("On it!", "Hurry, with the pie I can stop her evil scheme!");
					OnItReaction = new Reaction();
					OnItReaction.AddAction(new NPCCallbackAction(UpdateOnIt));
					OnItReaction.AddAction(new ShowOneOffChatAction(toControl, "Hurry, with the pie I can stop here evil scheme!"));
								
		}
		public void UpdateDenyApple(){
			SetDefaultText(appleString);
			GUIManager.Instance.RefreshInteraction();
		}
		public void UpdateTakeApple(){
			FlagManager.instance.SetFlag(FlagStrings.MakePieFromApple);
			SetDefaultText("Thanks for the apple...I guess...");
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Thanks for the apple...too bad you couldn't give me pie...");
		}
		public void UpdateTakeApplePie(){
			FlagManager.instance.SetFlag(FlagStrings.GoDownToBeach);
			SetDefaultText(pieString);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Next stop: the beach!");
		}
		
		public void UpdateEvilPlan (){
			_allChoiceReactions.Remove(EvilPlanChoice);
			_allChoiceReactions.Add(StopBeingLazyChoice, new DispositionDependentReaction(StopBeingLazyReaction));
			_allChoiceReactions.Add(HowDoWeStopHerChoice, new DispositionDependentReaction(HowDoWeStopHerReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("My mom...is...draining...my strength...please...help!");
		}
				public void UpdateStopHer(){
					_allChoiceReactions.Remove(HowDoWeStopHerChoice);
					_allChoiceReactions.Remove(StopBeingLazyChoice);
					_allChoiceReactions.Add(NoDoYourChoresChoice, new DispositionDependentReaction(NoDoYourChoresReaction));
					_allChoiceReactions.Add(OnItChoice, new DispositionDependentReaction(OnItReaction));
					GUIManager.Instance.RefreshInteraction();
					SetDefaultText("Help me cook to stop her evil plan!");
					pieString = "Pie!  Perfect, exactly what I needed!";
				}
						public void UpdateOnIt(){
							_allChoiceReactions.Remove(OnItChoice);
							_allChoiceReactions.Remove(NoDoYourChoresChoice);
							GUIManager.Instance.CloseInteractionMenu();
							SetDefaultText("Hurry!  I need you to get apple pie!");
							pieString = "Pie!  Perfect, exactly what I needed!";
							//Placeholder for an extra flag
						}
						public void UpdateDoYourOwnChores(){
							_allChoiceReactions.Remove(OnItChoice);
							_allChoiceReactions.Remove(NoDoYourChoresChoice);
							_allChoiceReactions.Add(NoChoice, new DispositionDependentReaction(NoReaction));
							_allChoiceReactions.Add(OkayFineChoice, new DispositionDependentReaction(OkayFineReaction));
							GUIManager.Instance.RefreshInteraction();
							SetDefaultText("Please help a girl play on the beach!");
							pieString = "See!  It wasn't that hard to give me some pie!";
						}
								public void UpdateOkayFine(){
									_allChoiceReactions.Remove(NoChoice);
									_allChoiceReactions.Remove(OkayFineChoice);
									GUIManager.Instance.CloseInteractionMenu();
									SetDefaultText("Did you find apple pie.");
									pieString = "See!  It wasn't that hard to give me some pie!";
									//FlagManager.instance.SetFlag(FlagStrings.WaitForItem);
								}
								public void UpdateNo(){
									_allChoiceReactions.Remove(NoChoice);
									_allChoiceReactions.Remove(OkayFineChoice);
									_allChoiceReactions.Add(ImTellingOnYouChoice, new DispositionDependentReaction(ImTellingOnYouReaction));
									_allChoiceReactions.Add(NoMeansNoChoice, new DispositionDependentReaction(NoMeansNoReaction));
									_allChoiceReactions.Add(OkayOneAppleChoice, new DispositionDependentReaction(OkayOneAppleReaction));
									//Add anotherone here.
									GUIManager.Instance.RefreshInteraction();
									SetDefaultText("Okay fine!  Just get me an apple!");
									pieString = "See!  It wasn't that hard to give me some pie!";
								}
										public void UpdateOkayOneMoreApple(){
											_allChoiceReactions.Remove(NoMeansNoChoice);
											_allChoiceReactions.Remove(OkayOneAppleChoice);
											_allChoiceReactions.Remove(ImTellingOnYouChoice);
											GUIManager.Instance.CloseInteractionMenu();
											SetDefaultText("Have you got an apple yet?");
											//FlagManager.instance.SetFlag(FlagStrings.WaitForItem);
											pieString = "See!  It wasn't that hard to give me some pie!";
										}
										public void UpdateNoMeansNo(){
											_allChoiceReactions.Remove(NoMeansNoChoice);
											_allChoiceReactions.Remove(OkayOneAppleChoice);
											_allChoiceReactions.Remove(ImTellingOnYouChoice);
											GUIManager.Instance.CloseInteractionMenu();
											SetDefaultText("Go away peasant!");
											FlagManager.instance.SetFlag(FlagStrings.MakePieFromScratch);
										}
										public void UpdateTellingOnYou(){
											_allChoiceReactions.Remove(NoMeansNoChoice);
											_allChoiceReactions.Remove(OkayOneAppleChoice);
											_allChoiceReactions.Remove(ImTellingOnYouChoice);
											_allChoiceReactions.Add(ThisOneTimeChoice,new DispositionDependentReaction(ThisOneTimeReaction));
											_allChoiceReactions.Add(ImGonnaTellNowChoice, new DispositionDependentReaction(ImGonnaTellNowReaction));
											GUIManager.Instance.RefreshInteraction();
											SetDefaultText("Please, please, please don't tell on them!");
										}
												public void UpdateThisOneTime(){
													_allChoiceReactions.Remove(ThisOneTimeChoice);
													_allChoiceReactions.Add(SighYouWinChoice,new DispositionDependentReaction(SighYouWinReaction));
													_allChoiceReactions.Add(NOOChoice, new DispositionDependentReaction(NOOReaction));
													GUIManager.Instance.RefreshInteraction();
													SetDefaultText("So an apple?");
												
												}
														public void UpdateSighYouWin(){
															_allChoiceReactions.Remove(ImGonnaTellNowChoice);
															_allChoiceReactions.Remove(SighYouWinChoice);
															_allChoiceReactions.Remove(NOOChoice);
															GUIManager.Instance.CloseInteractionMenu();
															SetDefaultText("Now go and aquire that apple!");
															//FlagManager.instance.SetFlag(FlagStrings.WaitForItem);
														}
														public void UpdateNOO(){
															_allChoiceReactions.Remove(ImGonnaTellNowChoice);
															_allChoiceReactions.Remove(SighYouWinChoice);
															_allChoiceReactions.Remove(NOOChoice);
															GUIManager.Instance.CloseInteractionMenu();
															SetDefaultText("Go away!  I don't like you!");
															FlagManager.instance.SetFlag(FlagStrings.MakePieFromScratch);
														}
			public void UpdateStopBeingLazy (){
				_allChoiceReactions.Remove(StopBeingLazyChoice);
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
					_allChoiceReactions.Add(OkayIGuessChoice, new DispositionDependentReaction(OkayIGuessReaction));
					_allChoiceReactions.Add(NoImTellingChoice, new DispositionDependentReaction(NoImTellingReaction));
					GUIManager.Instance.RefreshInteraction();
					SetDefaultText("Please don't tell!");
				}
						public void UpdateOkayIGuess(){
							_allChoiceReactions.Remove(OkayIGuessChoice);
							_allChoiceReactions.Remove(NoImTellingChoice);
							GUIManager.Instance.CloseInteractionMenu();
							FlagManager.instance.SetFlag(FlagStrings.MakePieFromScratch);
							SetDefaultText("Apple Pie?  No?  okay...");
						}
						public void UpdateNoImTelling(){
							_allChoiceReactions.Remove(OkayIGuessChoice);
							_allChoiceReactions.Remove(NoImTellingChoice);
							GUIManager.Instance.CloseInteractionMenu();
							FlagManager.instance.SetFlag(FlagStrings.CounterTellOn);
							SetDefaultText("Dad said to stop talking to me!");
						}
				public void UpdateThisIsSilly (){
					_allChoiceReactions.Remove(ImTellingChoice);
					_allChoiceReactions.Remove(ThisIsSilllyChoice);
					_allChoiceReactions.Remove(WarriorsCookChoice);
					GUIManager.Instance.CloseInteractionMenu();
					SetDefaultText("On your way peasant!");
					FlagManager.instance.SetFlag(FlagStrings.MakePieFromScratch);
				}
				public void UpdateWarriorsCook(){
					_allChoiceReactions.Remove(WarriorsCookChoice);
					_allChoiceReactions.Remove(ThisIsSilllyChoice);
					_allChoiceReactions.Remove(ImTellingChoice);
					_allChoiceReactions.Add(WhatIfYouTravelChoice, new DispositionDependentReaction(WhatIfYouTravelReaction));
					_allChoiceReactions.Add(FairEnoughChoice, new DispositionDependentReaction(FairEnoughReaction));
					GUIManager.Instance.RefreshInteraction();
					SetDefaultText("Warrior's don't cook!");
				}
						public void UpdateWhatIfYouTravel(){
							_allChoiceReactions.Remove(WhatIfYouTravelChoice);
							_allChoiceReactions.Remove(FairEnoughChoice);
							GUIManager.Instance.CloseInteractionMenu();
							SetDefaultText("I guess warrior's should know how to cook...");
						}
						public void UpdateFairEnough(){
							_allChoiceReactions.Remove(FairEnoughChoice);
							_allChoiceReactions.Remove(WhatIfYouTravelChoice);
							GUIManager.Instance.CloseInteractionMenu();
							SetDefaultText("Go findeth me apple pie!");
						}
		public void UpdateImTellingNow(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.CounterTellOn);
			SetDefaultText("My dad told you to leave me alone!");
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
