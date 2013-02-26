using UnityEngine;
using System.Collections;

public class MoveState : AbstractState {
	private Vector3 _goal;
	private Path _pathFollowing;
	private float speed = 5f;
	private GoToState currentMovementState = null;
	
	private Vector3 currentGoal;
	private int currentDirection;
	
	public MoveState(Character toControl, Vector3 goal) : base(toControl){
		_goal = goal;
	}
	
	public override void Update(){
		Debug.Log(character.name + ": MoveState Update");
		
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
				currentDirection = _pathFollowing.GetDirection();
				
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
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": MoveState Enter");
		
		CalculatePath();
		currentGoal = _pathFollowing.GetPoint();
		currentMovementState = GetGoToStateToPoint(currentGoal);
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": MoveState Exit");
	}
	
	private void Move(Vector3 moveDelta){
		currentMovementState.Move(moveDelta);
	}
	
	private bool NearPoint(Vector3 point, int dir){
		Vector3 pos = character.transform.position;
		float difference = speed*Time.deltaTime;
		Debug.Log("Nearpoint(" + point +", " + dir + ")");
		if ((dir == 0 || dir == 1) && (pos.x  < point.x + difference && pos.x > point.x - difference))
			return true;
		if ((dir == 2 || dir == 3) && (pos.y  < point.y + difference && pos.y > point.y - difference))
			return true;
		return false;
	}
	
	private void CalculatePath(){
		int mask = (1 << 9);
		RaycastHit hit;
		// Get the first node in the path by looking down at the floor
		if (Physics.Raycast(_goal, Vector3.down , out hit, Mathf.Infinity, mask)) {
			Vector3 hitPos = hit.point;
			hitPos.y += character.transform.localScale.y/2;
			_pathFollowing = new Path();
			if (PathFinding.StartPath(character.transform.position, hitPos, character.transform.localScale.y/2)){
				_pathFollowing = PathFinding.GetPath();
				Debug.Log("Got a path to the initial point " + _pathFollowing.GetPoint());
			} else {
				OnNoPath();
			}
		}
	}
	
	// Draw a line from the current position to the point and determine if we should walk or climb there
	private GoToState GetGoToStateToPoint(Vector3 point){
		return (new WalkToState(character)); //TODO
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
