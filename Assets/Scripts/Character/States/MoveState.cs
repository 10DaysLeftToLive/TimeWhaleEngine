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
	
	public MoveState(Character toControl, Vector3 goal) : base(toControl){
		_goal = goal;
	}
	
	public override void Update(){
		Vector3 pos = character.transform.position;
		Vector3 movement = new Vector3(0,0,0);
		if (_pathFollowing.GetDirection() == 0){
			if (pos.x < _pathFollowing.GetPoint().x){
				movement.x += speed;
				character.LookRight();
			}else if (pos.x > _pathFollowing.GetPoint().x){
				movement.x -= speed;	
				character.LookLeft();
			}
		} else {
			if (pos.y < _pathFollowing.GetPoint().y){
				movement.y += speed;
			}else if (pos.y > _pathFollowing.GetPoint().y){
				movement.y -= speed;	
			}
		}
		movement *= Time.deltaTime;
		
		if (NearPoint(_pathFollowing.GetPoint(), _pathFollowing.GetDirection())){
			if (!_pathFollowing.NextNode()){
				OnGoalReached();
			} else {
				currentGoal = _pathFollowing.GetPoint();
				
				if (currentGoal.y > pos.y || currentGoal.y < pos.y){
					Debug.Log("The point was different vertically switching to climbing");
					currentMovementState = new ClimbToState(character);
				} else {
					Debug.Log("The point was the same vertically switching to walking");
					currentMovementState = new WalkToState(character);
				}
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
		Debug.Log(character.name + ": MoveState Enter");
		speed = 5f;
		
		if (CalculatePath()){
			currentGoal = _pathFollowing.GetPoint();
			currentMovementState = GetGoToStateToPoint(currentGoal);
		} else {
			OnNoPath();
		}
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": MoveState Exit");
	}
	
	private void Move(Vector3 moveDelta){
		Debug.Log (currentMovementState);
		currentMovementState.Move(moveDelta);
	}
	
	private bool NearPoint(Vector3 point, int dir){
		Vector3 pos = character.transform.position;
		float difference = speed*Time.deltaTime;
		if ((dir == 0 || dir == 1) && (pos.x  < point.x + difference && pos.x > point.x - difference))
			return true;
		if ((dir == 2 || dir == 3) && (pos.y  < point.y + difference && pos.y > point.y - difference))
			return true;
		return false;
	}
	
	private bool CalculatePath(){
		int mask = (1 << 9);
		RaycastHit hit;
		
		if (Physics.Raycast(_goal, Vector3.down , out hit, Mathf.Infinity, mask)) {
			Vector3 hitPos = hit.point;
			hitPos.y += character.transform.localScale.y/2;
			
			_pathFollowing = new Path();
			if (PathSearch(character.transform.position, hitPos, character.transform.localScale.y/2)){
				_pathFollowing = PathFinding.GetPath();
				
				return (true);
			}
		} return (false);
	}
	
	protected virtual bool PathSearch(Vector3 pos, Vector3 hitPos, float height){
		return (PathFinding.StartPath(pos, hitPos, height));
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
		character.EnterState(new IdleState(character));
	}
	
	protected virtual void OnGoalReached(){
		// We have reached our goal, let the current movement state determine what to do now
		currentMovementState.OnGoalReached();
	}
}
