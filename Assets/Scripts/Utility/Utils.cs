using UnityEngine;
using System.Collections;

public static class Utils{
	private static float SPACEINFRONT = .3f;
	
	public static float CalcDistance(float point1, float point2){
		return (Mathf.Abs(point1 - point2));
	}
	
	public static float CalcDifference(float point1, float point2){
		return (point1 - point2);
	}
	
	public static bool Within(float point, float minimumDistance){
		return (point > -minimumDistance && point < minimumDistance);
	}
	
	static float xDistance;
	static float yDistance;
	public static float GetDistance(GameObject gameObjOne, GameObject gameObjTwo){
		xDistance = Mathf.Abs(gameObjOne.transform.position.x - gameObjTwo.transform.position.x);
		yDistance = Mathf.Abs(gameObjOne.transform.position.y - gameObjTwo.transform.position.y);
		return (Mathf.Sqrt(Mathf.Pow(xDistance, 2.0f) + Mathf.Pow(yDistance, 2.0f)));
	}
	
	public static bool InDistance(GameObject gameObjOne, GameObject gameObjTwo, float distance) {
		return (GetDistance(gameObjOne, gameObjTwo) < distance);
	}
	
	/// <summary>
	/// Gets the point infront of the given object. Based on their positions. 
	/// If the start is right inside the target object it will move to one of the sides
	/// </summary>
	public static Vector3 GetPointInfrontOf(Vector3 start, GameObject objectToMoveInfront){
		Vector3 whereToMove = objectToMoveInfront.transform.position;
		Bounds objectBounds = objectToMoveInfront.collider.bounds;
		
		if (Utils.CalcDifference(start.x, objectBounds.min.x) < 0) { // if the target is to the right
			whereToMove.x = whereToMove.x - objectToMoveInfront.transform.localScale.x/2 - SPACEINFRONT;
		} else if (Utils.CalcDifference(start.x, objectBounds.max.x) > 0) { // if the target is to the left
			whereToMove.x = whereToMove.x + objectToMoveInfront.transform.localScale.x/2 + SPACEINFRONT;
		} else { // if we are inside
			if (Utils.CalcDifference(start.x, whereToMove.x) < 0) { // if the target is to the right
				whereToMove.x = whereToMove.x - objectToMoveInfront.transform.localScale.x/2 - SPACEINFRONT;
			} else {
				whereToMove.x = whereToMove.x + objectToMoveInfront.transform.localScale.x/2 + SPACEINFRONT;
			}
		}
		
		return (whereToMove);
	}
	
	public static void SetActiveRecursively(GameObject gameObject, bool active) {
		gameObject.SetActive (active);
    	foreach (Transform limb in gameObject.transform) {
        	SetActiveRecursively (limb.gameObject, active);
		}
	}
	
	// returns the amount of time in ms
	public static float CalcTimeToDisplayText(string text){
		return (text.Length * .09f);
	}
}