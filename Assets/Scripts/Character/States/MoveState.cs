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
	protected static float NPC_SPEED_RATIO = 0.75f;
    private static string lastWay = null;
    private static int NameStartCounter = 0;
    private static int NameEndCounter = 0;
    private static bool traverseStair = false;
    private static bool towardStair = true;
        
    public MoveState(Character toControl, Vector3 goal) : base(toControl){
        _goal = goal;
    }
    
	Vector3 pos;
	Vector3 movement;
    public override void Update(){
        pos = character.transform.position;
        movement = _pathFollowing.GetVectorDirection();
        movement *= (speed * Time.deltaTime);

        if (NearPoint(_pathFollowing.GetPoint(), _pathFollowing.GetVectorDirection())){
            if (!_pathFollowing.NextNode())
            {
                OnGoalReached();
            }
            else
            {
                currentGoal = _pathFollowing.GetPoint();
                if (character is NPC)
                {
                }
                else
                {
                    if (SoundManager.instance.AudioOn)
                    {
                        lastWay = _pathFollowing.GetLastWayPointName();

                        Debug.Log("Before any checks, lastWay is " + lastWay);
                        /*DebugManager.instance.Log("lastWay is " + lastWay, "WalkSFX", "SFX");*/
                        NameStartCounter = lastWay.IndexOf(".");
                        NameEndCounter = lastWay.IndexOf("Stair");
                        // since we're heading towards the stairs, we need to grab the name of the area we are leaving and the area we're heading towards
                        if (lastWay.IndexOf("StairBase") > 0 || lastWay.IndexOf("StairTop") > 0 || lastWay == "Forest.010")
                        {

                            /*Strings.LASTAREA = lastWay.Substring(0, NameStartCounter);
                            Strings.NEXTAREA = lastWay.Substring(NameStartCounter + 5, NameEndCounter - (NameStartCounter + 5));*/
                            Debug.Log("Heading towards some stairs");
                            //DebugManager.instance.Log("LASTAREA is " + Strings.LASTAREA, "WalkSFX", "SFX");

                            if (lastWay.IndexOf("StairBase") > 0)
                            {
                                Strings.BOTTOMOFSTAIRS = lastWay.Substring(0, NameStartCounter);
                                Strings.TOPOFSTAIRS = lastWay.Substring(NameStartCounter + 5, NameEndCounter - (NameStartCounter + 5));
                            }
                            else if (lastWay.IndexOf("StairTop") > 0)
                            {
                                Strings.TOPOFSTAIRS = lastWay.Substring(0, NameStartCounter);
                                Strings.BOTTOMOFSTAIRS = lastWay.Substring(NameStartCounter + 5, NameEndCounter - (NameStartCounter + 5));
                            }

                            towardStair = true;
                        }
                        // possibly walking towards stairs
                        else if (towardStair)
                        {
                            // just started walking up the stairs
                            if (lastWay.EndsWith("High") || lastWay.EndsWith("Low"))
                            {
                                Debug.Log("Walking up or down the stairs.");
                                if (lastWay.IndexOf("Low") > 0)
                                {
                                    SoundManager.instance.StartCoroutineFadeDown(Strings.BOTTOMOFSTAIRS);
                                }
                                else
                                {
                                    SoundManager.instance.StartCoroutineFadeDown(Strings.TOPOFSTAIRS);
                                }
                                towardStair = false;
                                traverseStair = true;
                                Strings.CURRENTAREA = "Stairs";
                                if (SoundManager.instance.SFXOn)
                                {
                                    SoundManager.instance.PlayWalkSFX();
                                }
                                //Debug.Log("Just started walking up some stairs");
                            }
                            // walking on the pier or the bridge
                            else if (lastWay.IndexOf("Pier") > 0 || lastWay.IndexOf("Bridge") > 0)
                            {
                                Debug.Log("Walking on the pier or the bridge.");
                                towardStair = false;
                                traverseStair = true;
                                Strings.CURRENTAREA = "Stairs";
                                if (SoundManager.instance.SFXOn)
                                {
                                    SoundManager.instance.PlayWalkSFX();
                                }
                            }
                            // passed by the stairs, rather than going up them
                            else if (lastWay.IndexOf("Stair") == -1 && lastWay.IndexOf("Pier") == -1 && lastWay.IndexOf("Bridge") == -1)
                            {
                                Debug.Log("Passed by stairs, rather than going up them.");
                                towardStair = false;
                            }
                        }
                        // finished traversing the stairs
                        else if (traverseStair)
                        {
                            if (lastWay.EndsWith("High") || lastWay.EndsWith("Low"))
                            {
                                Debug.Log("Finished traversing the stairs");
                                traverseStair = false;
                                towardStair = true;

                                if (lastWay.EndsWith("High"))
                                {
                                    Strings.CURRENTAREA = Strings.TOPOFSTAIRS;
                                }
                                else
                                {
                                    Strings.CURRENTAREA = Strings.BOTTOMOFSTAIRS;
                                }
                                /*Strings.CURRENTAREA = Strings.NEXTAREA;
                                Strings.NEXTAREA = Strings.LASTAREA;
                                Strings.LASTAREA = Strings.CURRENTAREA;*/
                                if (SoundManager.instance.SFXOn)
                                {
                                    SoundManager.instance.PlayWalkSFX();
                                }
                                SoundManager.instance.StartCoroutineFadeUp(Strings.CURRENTAREA);

                                //Debug.Log("Just finished walking up some stairs and CURRENTAREA is "+Strings.CURRENTAREA);
                                DebugManager.instance.Log("Just finished walking up some stairs and CURRENTAREA is " + Strings.CURRENTAREA, "Audio");
                            }
                            else if (lastWay.IndexOf("Bridge") > 0)
                            {
                                if (lastWay.EndsWith("Left") || lastWay.EndsWith("Right"))
                                {
                                    Debug.Log("Finished traversing bridge.");
                                    traverseStair = false;
                                    towardStair = true;
                                    Strings.CURRENTAREA = "Forest";
                                    if (SoundManager.instance.SFXOn)
                                    {
                                        SoundManager.instance.PlayWalkSFX();
                                    }
                                }
                            }
                            else if (lastWay.IndexOf("Pier") > 0)
                            {
                                if (lastWay.EndsWith("Left"))
                                {
                                    Debug.Log("Finished traversing the pier.");
                                    traverseStair = false;
                                    towardStair = true;
                                    Strings.CURRENTAREA = "Beach";
                                    if (SoundManager.instance.SFXOn)
                                    {
                                        SoundManager.instance.PlayWalkSFX();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } else {
            Move(movement);
       		MakeCharacterLookCorrectly(movement.x);
        	CheckStuck(pos);
        }
    }
    
    public override void OnEnter(){
        CalcMovementPath();

        // Needs a check for which area the player is in to switch which
        // walking SFX is loaded.
        if (character is NPC){
			speed = speed*NPC_SPEED_RATIO;
        }else{
            if (SoundManager.instance.AudioOn && SoundManager.instance.SFXOn)
            {
                SoundManager.instance.PlayWalkSFX();
            }
        }
    }
    
    public override void OnExit(){
        //Debug.Log(character.name + ": MoveState Exit");

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
	
	public void UpdateGoal(Vector3 newGoal){
		_goal = newGoal;
		CalcMovementPath();
	}
	
	private void CalcMovementPath(){
		speed = 5f;
		if (currentMovementState == null){
       		currentMovementState = GetGoToStateToPoint(currentGoal);
		}
		if (CalculatePath()){
            currentGoal = _pathFollowing.GetPoint();
        } else {
            OnNoPath();
        }
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
        RaycastHit hitDown, hitUp;
		float distance = 999;
		Vector3 hitPos = Vector3.zero;
        
        if (Physics.Raycast(_goal, Vector3.down , out hitDown, 10, mask)) {
			if (hitDown.distance < distance){
				distance = hitDown.distance;
				hitPos = hitDown.point;
            	hitPos.y += character.transform.localScale.y/2;
			}
        } 
		if (Physics.Raycast(_goal, Vector3.up , out hitUp, 10, mask)) {
			if (hitUp.distance < distance/2){
				distance = hitUp.distance;
				hitPos = hitUp.point;
           		hitPos.y += character.transform.localScale.y/2;
			}
        }
		
		if (distance != 999){
			if (PathFinding.GetPathForPoints(character.transform.position, hitPos, character.transform.localScale.y/2)){
	                _pathFollowing = PathFinding.GetPath();
	                return (true);
	        }
		}
		
		return (false);
    }
    
    protected virtual bool PathSearch(Vector3 pos, Vector3 hitPos, float height){
        return (PathFinding.GetPathForPoints(pos, hitPos, height));
    }
    
    // Draw a line from the current position to the point and determine if we should walk or climb there
    private GoToState GetGoToStateToPoint(Vector3 point){
        return (new WalkToState(character)); //TODO
    }
    
    public virtual void OnStuck(){
        if (SoundManager.instance.AudioOn && SoundManager.instance.SFXOn)
        {
            SoundManager.instance.StopWalkSFX();
        }
		
        currentMovementState.OnStuck();
    }
    
    private void OnNoPath(){
        if (SoundManager.instance.AudioOn && SoundManager.instance.SFXOn)
        {
            SoundManager.instance.StopWalkSFX();
        }
		
        currentMovementState.OnGoalReached();
    }
    
	private void CheckStuck(Vector3 posAtStart){
		if (posAtStart.Equals(character.transform.position)){
            stuckTimer += Time.deltaTime;
        }else{
            stuckTimer = 0;	
        }
        
        if (stuckTimer > .5f){
            OnStuck();
        }
	}
	
    protected virtual void OnGoalReached(){
        currentMovementState.OnGoalReached();
    }
	
	private void MakeCharacterLookCorrectly(float direction){
		if (direction < 0){
            character.LookLeft();
        } else{
            character.LookRight();
        }
	}
}
