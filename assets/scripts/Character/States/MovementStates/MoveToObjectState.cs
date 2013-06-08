using UnityEngine;
using System.Collections;

public class MoveToObjectState : MoveState {
	protected int ticksTillUpdate = 0;
	protected GameObject _toMoveTo;
	protected static int TICKS_TILL_UPDATE = 30;
	protected static float DISTANCE_TO_STOP = 1.5f;
	
	public MoveToObjectState(Character toControl, GameObject toMoveTo) : base(toControl, toMoveTo.transform.position){
		_toMoveTo = toMoveTo;
    }
	
	// Don't want to calculate a new path every tick
	public override void Update() {
		//UpdateGoal(_toMoveTo.transform.position);
		if (ticksTillUpdate < 1) {
			ticksTillUpdate = TICKS_TILL_UPDATE;
			UpdateGoal(_toMoveTo.transform.position);
		} else {
			ticksTillUpdate -= 1;
		}
		
		if (Utils.InDistance(character.gameObject, _toMoveTo, DISTANCE_TO_STOP)) {
			OnGoalReached();
		}
		
		base.Update();
	}
    
    public override void OnEnter(){
        DebugManager.instance.Log(character.name + ": MoveToTalkState Enter", character.name);
        //TODO: Handle case for climb
		UpdateGoal(_toMoveTo.transform.position);
        character.PlayAnimation(Strings.animation_walk);
        base.OnEnter();
    }
    
    public override void OnExit(){
        character.PlayAnimation(Strings.animation_stand);
        DebugManager.instance.Log(character.name + ": MoveThenDoState Exit", character.name);

        if (character is NPC)
        {

        }
        else
        {
            if(SoundManager.instance.AudioOn && SoundManager.instance.SFXOn)
            SoundManager.instance.StopWalkSFX();
        }
        
    }
    
    protected override void OnGoalReached(){
         DebugManager.instance.Log(character.name + " has reached the goal. Swithcing to what to do when done.", character.name);
        character.PlayAnimation(Strings.animation_stand);
        character.EnterState(new MarkTaskDone(character));
    }
}
