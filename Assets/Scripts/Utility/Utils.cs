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
	
	public static Vector3 GetPointInfrontOf(Vector3 start, GameObject objectToMoveInfront){
		Vector3 whereToMove = objectToMoveInfront.transform.position;
		Debug.Log("where to move = " + whereToMove + " - " + objectToMoveInfront.transform.localScale.x/2);
		if (Utils.CalcDifference(start.x, whereToMove.x) < 0) { // if the target is to the right
			whereToMove.x = whereToMove.x - objectToMoveInfront.transform.localScale.x/2 - SPACEINFRONT;
		} else {
			whereToMove.x = whereToMove.x + objectToMoveInfront.transform.localScale.x/2 + SPACEINFRONT;
		}
		Debug.Log("where to move = " + whereToMove);
		
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
		return (text.Length * .1f);
	}
}