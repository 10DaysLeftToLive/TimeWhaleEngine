using UnityEngine;
using System.Collections;

/*
 *  This state will be active when the user clicks on something to interact with.
 *  We will then move to it and perform the specific action on it.
 */
public class MoveThenDoState : MoveState {
    private State _toDoWhenDone;
    
    public MoveThenDoState(Character toControl, Vector3 goal, State toDoWhenDone) : base(toControl, goal){
        _toDoWhenDone = toDoWhenDone;
    }
    
    public override void OnEnter(){
		DebugManager.instance.Log(character.name + ": MoveThenDoState Exit", character.name, "State");
		
        character.PlayAnimation(Strings.animation_walk);
        base.OnEnter();
    }
    
    public override void OnExit(){
        character.PlayAnimation(Strings.animation_stand);
		DebugManager.instance.Log(character.name + ": MoveThenDoState Exit", character.name, "State");

        if (character is NPC)
        {

        }
        else
        {
            if (SoundManager.instance.AudioOn && SoundManager.instance.SFXOn)
            {
                SoundManager.instance.StopWalkSFX();
            }
        }
        
    }
    
    protected override void OnGoalReached(){
        character.PlayAnimation(Strings.animation_stand);
        character.EnterState(_toDoWhenDone);
    }
}