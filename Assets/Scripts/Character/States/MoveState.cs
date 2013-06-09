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
	private static float MIN_DISTANCE_TO_POINT = 4;
	private static float zDepthChange = .2f;
        
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
            if (!_pathFollowing.NextNode()){
                OnGoalReached();
            } else {
                currentGoal = _pathFollowing.GetPoint();
                if (character is Player){
                    TransitionSounds();
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

        if (character is NPC){
			speed = speed*NPC_SPEED_RATIO;
        } else if (SoundManager.instance.AudioOn && SoundManager.instance.SFXOn){
            SoundManager.instance.PlayWalkSFX();
        }
    }
    
    public override void OnExit(){
        if (character is Player && SoundManager.instance.AudioOn && SoundManager.instance.SFXOn){
            SoundManager.instance.StopWalkSFX();
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
			if (character is NPC){ // the player should try to move near to the point instead of quiting
            	OnNoPath();
			} else {
				GoToNearestNode();
			}
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
		float distance = MIN_DISTANCE_TO_POINT;
		Vector3 hitPos = Vector3.zero;
        
        if (Physics.Raycast(_goal, Vector3.down , out hitDown, 10, mask)) {
			if (hitDown.distance < distance){
				distance = hitDown.distance;
				hitPos = hitDown.point;
            	hitPos.y += .2f;
			}
        } 
		if (Physics.Raycast(_goal, Vector3.up , out hitUp, 10, mask)) {
			if (hitUp.distance < distance/2){
				hitDown = hitUp;
				distance = hitUp.distance;
				hitPos = hitUp.point;
           		hitPos.y += .2f;
			}
        }
		
		if (distance != MIN_DISTANCE_TO_POINT){
			float height = character.transform.localScale.y/2;
			if (PathFinding.GetPathForPoints(character.transform.position, hitPos, height, hitDown)){
                _pathFollowing = PathFinding.GetPath();
                return (true);
	        }
		}
		
		return (false);
    }
    
   /* protected virtual bool PathSearch(Vector3 pos, Vector3 hitPos, float height){
        return (PathFinding.GetPathForPoints(pos, hitPos, height));
    }*/
    
    // Draw a line from the current position to the point and determine if we should walk or climb there
    private GoToState GetGoToStateToPoint(Vector3 point){
        return (new WalkToState(character)); //TODO
    }
    
    public virtual void OnStuck(){
        if (character is Player &&  SoundManager.instance.AudioOn && SoundManager.instance.SFXOn){
            SoundManager.instance.StopWalkSFX();
        }
		
        currentMovementState.OnStuck();
    }
    
    private void OnNoPath(){
        if (character is Player && SoundManager.instance.AudioOn && SoundManager.instance.SFXOn){
            SoundManager.instance.StopWalkSFX();
        }
		
        currentMovementState.OnGoalReached();
    }
    
	private void CheckStuck(Vector3 posAtStart){
		if (posAtStart.Equals(character.transform.position)){
            stuckTimer += Time.deltaTime;
        } else {
            stuckTimer = 0;	
        }
        
        if (stuckTimer > .5f){
            OnStuck();
        }
	}
	
	private void GoToNearestNode(){
		Vector3 newGoal = GetNearestNodeToGoal(_goal);
		// if we are already at the nearest node don't move
		if (Utils.CalcDistance(newGoal.x, character.transform.position.x) < speed*Time.deltaTime){
			OnNoPath();
		} else {
			UpdateGoal(newGoal);
		}
	}
	
	private Vector3 GetNearestNodeToGoal(Vector3 goalCantReach){
		WayPoints[] ageWaypoints = LevelManager.GetCurrentAgeWaypoints();
		
		WayPoints closest = ageWaypoints[0];
		float closestDistance = 100;
		float currentDistance;
		
		foreach (WayPoints waypoint in ageWaypoints){
			if (waypoint.pointAge != CharacterAgeManager.currentAge) continue;
			currentDistance = Vector3.Distance(waypoint.transform.position, goalCantReach);
			if (currentDistance < closestDistance){
				closestDistance = currentDistance;
				closest = waypoint;
			}
		}
		
		return (closest.transform.position);
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
	
    private void TransitionSounds()
    {
        if (SoundManager.instance.AudioOn)
        {
            lastWay = _pathFollowing.GetLastWayPointName();
            //Debug.Log("lastWay is " + lastWay);
            NameStartCounter = lastWay.IndexOf(".");
            NameEndCounter = lastWay.IndexOf("Stair");
            // since we're heading towards the stairs, we need to grab the name of the area we are leaving and the area we're heading towards
            if (lastWay.IndexOf("StairBase") > 0 || lastWay.IndexOf("StairTop") > 0 || lastWay == "Forest.010")
            {
				Vector3 charPos = character.transform.position;
				charPos.z -= zDepthChange;
				character.transform.position = character.transform.position;
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
            else if (towardStair)
            { // possibly walking towards stairs
                if (lastWay.EndsWith("High") || lastWay.EndsWith("Low"))
                {
                    if (lastWay.IndexOf("Low") > 0)
                    {
                        Strings.PREVIOUSAREA = Strings.BOTTOMOFSTAIRS;
                        Crossfade.instance.StartCoroutineFadeDown();
                    }
                    else
                    {
                        Strings.PREVIOUSAREA = Strings.TOPOFSTAIRS;
                        Crossfade.instance.StartCoroutineFadeDown();
                    }
                    towardStair = false;
                    traverseStair = true;
                    Strings.CURRENTAREA = "Stairs";
                    SoundManager.instance.PlayWalkSFX();
                }
                else if (lastWay.IndexOf("Pier") > 0 || lastWay.IndexOf("Bridge") > 0)
                { // walking on the pier or the bridge
                    towardStair = false;
                    traverseStair = true;
                    Strings.CURRENTAREA = "Stairs";
                    SoundManager.instance.PlayWalkSFX();
                }
                else if (lastWay.IndexOf("Stair") == -1 && lastWay.IndexOf("Pier") == -1 && lastWay.IndexOf("Bridge") == -1)
                { // passed by the stairs, rather than going up them
                    towardStair = false;
                }
            }
            else if (traverseStair)
            { // finished traversing the stairs
				Vector3 charPos = character.transform.position;
				charPos.z += zDepthChange;
				character.transform.position = character.transform.position;
                if (lastWay.EndsWith("High") || lastWay.EndsWith("Low"))
                {
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
                    SoundManager.instance.PlayWalkSFX();
                    Crossfade.instance.StartCoroutineFadeUp();

                    if (lastWay == "MarketWindmillStairLow" || lastWay == "MarketWindmillStairHigh")
                    {
                        if (!Crossfade.instance.CrossFade)
                        {
                            //Debug.Log("No crossfade occuring.");
                            if (Crossfade.instance.MarketAmbient.isPlaying)
                            {
                                Crossfade.instance.startCoroutineFadeOverTime("Market", "Windmill");
                            }
                            else
                            {
                                Crossfade.instance.startCoroutineFadeOverTime("Windmill", "Market");
                            }
                        }
                        else
                        {
                            ////Debug.Log("Crossfade occuring.");
                            Crossfade.instance.startCoroutineFadeOverTime("Market", "Windmill");
                        }
                    }
                }
                else if (lastWay.IndexOf("Bridge") > 0)
                {
                    if (lastWay.EndsWith("Left") || lastWay.EndsWith("Right"))
                    {
                        traverseStair = false;
                        towardStair = true;
                        Strings.CURRENTAREA = "Forest";
                        SoundManager.instance.PlayWalkSFX();
                    }
                }
                else if (lastWay.IndexOf("Pier") > 0 && lastWay.EndsWith("Left"))
                {
                    traverseStair = false;
                    towardStair = true;
                    Strings.CURRENTAREA = "Beach";
                    SoundManager.instance.PlayWalkSFX();
                }
                else if (lastWay == "BeachForestStairMiddle")
                {
                    if (!Crossfade.instance.CrossFade)
                    {
                        //Debug.Log("No crossfade occuring.");
                        if (Crossfade.instance.BeachAmbient.isPlaying)
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Beach", "Forest");
                        }
                        else
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Forest", "Beach");
                        }
                    }
                    else
                    {
                        //Debug.Log("Crossfade occuring.");
                        Crossfade.instance.startCoroutineFadeOverTime("Forest", "Beach");
                    }
                }
                else if (lastWay == "ForestToMarketLeftStair.000" || lastWay == "ForestToMarketRightStair.000")
                {
                    if (!Crossfade.instance.CrossFade)
                    {
                        //Debug.Log("No crossfade occuring.");
                        if (Crossfade.instance.ForestAmbient.isPlaying)
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Forest", "Market");
                        }
                        else
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Market", "Forest");
                        }
                    }
                    else
                    {
                        //Debug.Log("Crossfade occuring.");
                        Crossfade.instance.startCoroutineFadeOverTime("Market", "Forest");
                    }
                }
                else if (lastWay == "MarketToReflectionTreeStair.002")
                {
                    if (!Crossfade.instance.CrossFade)
                    {
                        //Debug.Log("No crossfade occuring.");
                        if (Crossfade.instance.MarketAmbient.isPlaying)
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Market", "ReflectionTree");
                        }
                        else
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("ReflectionTree", "Market");
                        }
                    }
                    else
                    {
                        //Debug.Log("Crossfade occuring.");
                        Crossfade.instance.startCoroutineFadeOverTime("Market", "ReflectionTree");
                    }
                }
                else if (lastWay == "MarketToLighthouseStair.000")
                {
                    if (!Crossfade.instance.CrossFade)
                    {
                        //Debug.Log("No crossfade occuring.");
                        if (Crossfade.instance.MarketAmbient.isPlaying)
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Market", "Lighthouse");
                        }
                        else
                        {
                            Crossfade.instance.startCoroutineFadeOverTime("Lighthouse", "Market");
                        }
                    }
                    else
                    {
                        //Debug.Log("Crossfade occuring.");
                        Crossfade.instance.startCoroutineFadeOverTime("Market", "Lighthouse");
                    }
                }
            }
        }
    }
}