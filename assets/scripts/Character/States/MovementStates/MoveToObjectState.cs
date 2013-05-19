using UnityEngine;
using System.Collections;

public class MoveToObjectState : MoveState {
	private int ticksTillUpdate = 0;
	private GameObject _toMoveTo;
	private static int TICKS_TILL_UPDATE = 5;
	private static float DISTANCE_TO_STOP = 1.5f;
	
	public MoveToObjectState(Character toControl, GameObject toMoveTo, Vector3 goal) : base(toControl, goal){
		_toMoveTo = toMoveTo;
    }
	
	// Don't want to calculate a new path every tick
	public override void Update() {
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
        Debug.Log(character.name + ": MoveToTalkState Enter");
        //TODO: Handle case for climb
		UpdateGoal(_toMoveTo.transform.position);
        character.PlayAnimation(Strings.animation_walk);
        base.OnEnter();
    }
    
    public override void OnExit(){
        character.PlayAnimation(Strings.animation_stand);
        Debug.Log(character.name + ": MoveThenDoState Exit");

        if (character is NPC)
        {

        }
        else
        {
            SoundManager.instance.StopSFX();
        }
        
    }
    
    protected override void OnGoalReached(){
        Debug.Log(character.name + " has reached the goal. Swithcing to what to do when done.");
        character.PlayAnimation(Strings.animation_stand);
        character.EnterState(new MarkTaskDone(character));
    }
}
