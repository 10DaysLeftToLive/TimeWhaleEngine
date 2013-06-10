// Back and forth Dialogue with CarpenterSonYoung
// If interrupted, tells you he's busy right now, then continues
// when finished, Carpenter Son walks away
// Carpenter grunts about his son, first the tools are gone and now he starts talking about doing something other than Carpentry
// Talks about disciplining his son, how could he not want to be a carpenter?

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarpenterYoung : NPC
{
	Schedule WalkSitWhittle;
	Reaction conversationWithSonDone;
    Reaction toolboxGivenToSonDone;

	protected override void Init()
	{
		id = NPCIDs.CARPENTER;
		base.Init();
	}

	protected override void SetFlagReactions()
	{
		conversationWithSonDone = new Reaction();
		conversationWithSonDone.AddAction(new NPCAddScheduleAction(this, WalkSitWhittle));
		conversationWithSonDone.AddAction(new NPCEmotionUpdateAction(this, new RandomMessageEmotionState(this, "Sorry, my son is busy today.  Hopefully he won't spend the whole day searching for his tools and can practice some carpentry.")));
		flagReactions.Add(FlagStrings.carpenterSonYoungConvoWithDadFinished, conversationWithSonDone);

        toolboxGivenToSonDone = new Reaction();
        toolboxGivenToSonDone.AddAction(new NPCEmotionUpdateAction(this, new GivenSonToolboxEmotionState(this, "Thanks for finding my son his toolbox, again.")));
        flagReactions.Add(FlagStrings.carpenterSonYoungGottenTools, toolboxGivenToSonDone);
	}

	protected override EmotionState GetInitEmotionState()
	{
		return (new InitialEmotionState(this, "Sorry, my son can't play with you today. He'll be working with me today, practicing his carpentry."));
	}

	protected override Schedule GetSchedule()
	{
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules()
	{
		TimeTask WaitTask = new TimeTask(0f, new IdleState(this));
		Task MoveTask = new Task(new MoveState(this, new Vector3(34.1062f, -1.041937f, 0.3f)));
		TimeTask SitTask = new TimeTask(0f, new AbstractAnimationState(this, "Sit"));
		Task WhittleTask = new Task(new AbstractAnimationState(this, "Idle Sit"));

		WalkSitWhittle = new Schedule(this);

		WalkSitWhittle.Add(WaitTask);
		WalkSitWhittle.Add(MoveTask);
		WalkSitWhittle.Add(SitTask);
		WalkSitWhittle.Add(WhittleTask);
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


	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState
	{
		public InitialEmotionState(NPC toControl, string currentDialogue)
			: base(toControl, currentDialogue)
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
		}

	}
	#endregion
	#region Random Message Emotion State
	private class RandomMessageEmotionState : EmotionState
	{
		string[] stringList = new string[30];
		Reaction randomMessage;
		int stringCounter = 0;

		public RandomMessageEmotionState(NPC toControl, string currentDialogue)
			: base(toControl, currentDialogue)
		{
			randomMessage = new Reaction();

			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage)); 

			stringList[0] = "Hopefully my son can find his tools without your help this time.";
			stringList[1] = "Maybe you could help my son find his tools like last time?";
			stringList[2] = "I wish my son would take better care of his tools, they're a carpenter's most valuable asset after all.  Just like how important an oven is for a baker like your mother.";
			stringCounter = 3;
		}

		public void RandomMessage()
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);

			SetDefaultText(stringList[(int)Random.Range(0, stringCounter)]);
		}
	}
	#endregion
    #region Given Son Toolbox
    private class GivenSonToolboxEmotionState : EmotionState
    {
        string[] stringList = new string[30];
        Reaction randomMessage;
        int stringCounter = 0;

        public GivenSonToolboxEmotionState(NPC toControl, string currentDialogue)
			: base(toControl, currentDialogue)
		{

            randomMessage = new Reaction();

            randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
            SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));

            stringList[0] = "How many times has he relied on you already?";
            stringList[1] = "Sorry you had to go out of your way to help my son, again.";
            stringList[2] = "I hope my son isn't relying on you to find his tools all the time.";
            stringCounter = 3;
		}

        public void RandomMessage()
        {
            _npcInState.SetCharacterPortrait(StringsNPC.Default);

            SetDefaultText(stringList[(int)Random.Range(0, stringCounter)]);
        }
    }
    #endregion
    #endregion
}
