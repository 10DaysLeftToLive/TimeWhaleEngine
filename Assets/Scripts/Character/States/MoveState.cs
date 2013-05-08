using UnityEngine;
using System.Collections;

/*
 * MoveState.cs
 * 	The base for moving the layer around.
 *  Will calculate the path to a point and will move over time to it changing between walking and climbing as necessary
 */
public class MoveState : AbstractState {
    private Vector3 _goal;
    private Path _pathFollowing;
    protected float speed = 5f;
    private GoToState currentMovementState = null;
    private Vector3 currentGoal;
    private float stuckTimer;
    private AudioSource walkSFX;
        
    public MoveState(Character toControl, Vector3 goal) : base(toControl){
        _goal = goal;
    }
    
    public override void Update(){
        Vector3 pos = character.transform.position;
        Vector3 movement = Vector3.zero;
        movement = _pathFollowing.GetVectorDirection();

        movement *= (speed * Time.deltaTime);

        if (movement.x < 0){
            character.LookLeft();
        }else if (movement.x > 0){
            character.LookRight();
        }else {
            // going up/down
            // character.climb???	
        }
        
        if (NearPoint(_pathFollowing.GetPoint(), _pathFollowing.GetVectorDirection())){
            if (!_pathFollowing.NextNode()){
                OnGoalReached();
            } else {
                currentGoal = _pathFollowing.GetPoint();
                currentMovementState = new WalkToState(character);
            }
        } else {
            Move(movement);
        }
        
        if (pos.Equals(character.transform.position)){
            stuckTimer += Time.deltaTime;
        }else{
            stuckTimer = 0;	
        }
        
        if (stuckTimer > .5f){
            OnStuck();
        }
    }
    
    public override void OnEnter(){
        //Debug.Log(character.name + ": MoveState Enter");
        speed = 5f;
        if (CalculatePath()){
            currentGoal = _pathFollowing.GetPoint();
            currentMovementState = GetGoToStateToPoint(currentGoal);
        } else {
            OnNoPath();
        }
        
        // Needs a check for which area the player is in to switch which
        // walking SFX is loaded.
        SoundManager.instance.PlaySFX("WalkForest");
    }
    
    public override void OnExit(){
        //Debug.Log(character.name + ": MoveState Exit");

        SoundManager.instance.StopSFX();
    }
    
    private void Move(Vector3 moveDelta){
        currentMovementState.Move(moveDelta);
    }
    
    private bool NearPoint(Vector3 point, Vector3 vectorDir){
        Vector3 pos = character.transform.position;
        
        pos = Vector3.Scale(pos,vectorDir);
        float flatPos = pos.x + pos.y;
        
        point = Vector3.Scale(point,vectorDir);
        float flatDestionation = point.x + point.y;
        
        float difference = speed*Time.deltaTime;
        
        if (flatPos < flatDestionation + difference && flatPos > flatDestionation - difference)
            return true;
        
        return false;
    }
    
    private bool CalculatePath(){
        int mask = (1 << 9);
        RaycastHit hit;
        
        if (Physics.Raycast(_goal, Vector3.down , out hit, 10, mask)) {
            Vector3 hitPos = hit.point;
            hitPos.y += character.transform.localScale.y/2;
            //Debug.Log("ground at " + hitPos);
            
            if (PathFinding.GetPathForPoints(character.transform.position, hitPos, character.transform.localScale.y/2)){
			//if (PathFinding.GetPathToNode(character.transform.position, "WayPoint.011", character.transform.localScale.y/2)){
                _pathFollowing = new Path();
                _pathFollowing = PathFinding.GetPath();
                
                return (true);
            }
        } return (false);
    }
    
    protected virtual bool PathSearch(Vector3 pos, Vector3 hitPos, float height){
        return (PathFinding.GetPathForPoints(pos, hitPos, height));
    }
    
    // Draw a line from the current position to the point and determine if we should walk or climb there
    private GoToState GetGoToStateToPoint(Vector3 point){
        return (new WalkToState(character)); //TODO
    }
    
    public virtual void OnStuck(){
        Debug.Log(character.name + " got stuck.");
        currentMovementState.OnStuck();
    }
    
    private void OnNoPath(){
        // TODO make a state for "can't do that"
        Debug.Log(character.name + " could not find a path. Returning to idle");
        character.EnterState(new MarkTaskDone(character));
    }
    
    protected virtual void OnGoalReached(){
        // We have reached our goal, let the current movement state determine what to do now
        currentMovementState.OnGoalReached();
    }
}
