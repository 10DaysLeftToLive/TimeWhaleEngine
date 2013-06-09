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

    protected override void Init()
    {
        id = NPCIDs.CARPENTER;
        base.Init();
    }

    protected override void SetFlagReactions()
    {
        conversationWithSonDone = new Reaction();
        conversationWithSonDone.AddAction(new NPCAddScheduleAction(this, WalkSitWhittle));
        flagReactions.Add(FlagStrings.carpenterSonYoungConvoWithDadFinished, conversationWithSonDone);
    }

    protected override EmotionState GetInitEmotionState()
    {
        return (new InitialEmotionState(this, "Sorry, my son can't play with you today. He'll be coming with me... as soon as he finds his tools."));
    }

    protected override Schedule GetSchedule()
    {
        Schedule schedule = new DefaultSchedule(this);
        return (schedule);
    }

    protected override void SetUpSchedules()
    {
        TimeTask WaitTask = new TimeTask(5f, new IdleState(this));
        Task MoveTask = new Task(new MoveState(this, new Vector3(34.1062f, -1.041937f, 0.3f)));
        //todo: add whittle animation

        WalkSitWhittle = new Schedule(this);

        WalkSitWhittle.Add(WaitTask);
        WalkSitWhittle.Add(MoveTask);
        //WalkSitWhittle.Add(IdleTask);
    }


    #region EmotionStates
    #region Initial Emotion State
    private class InitialEmotionState : EmotionState
    {

        string[] stringList = new string[30];
        Reaction randomMessage;
        int stringCounter = 0;

        public InitialEmotionState(NPC toControl, string currentDialogue)
            : base(toControl, currentDialogue)
        {
            randomMessage = new Reaction();

            randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
            SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));

            stringList[0] = "Why does my son always lose his tools?";
            stringList[1] = "Maybe he left his tools by the windmill?  Again?";
            stringList[2] = "I wish he'd take better care of his tools, they're a carpenter's most valuable asset after all.";
            stringCounter = 3;
        }

        public void RandomMessage()
        {
            SetDefaultText(stringList[(int)Random.Range(0, stringCounter)]);
        }

    }
    #endregion
    #endregion
}
