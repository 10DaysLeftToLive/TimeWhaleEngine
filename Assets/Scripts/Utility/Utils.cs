using UnityEngine;
using System.Collections;

/// <summary>
/// Utils is a collection of various functions that are useful
/// </summary>
public static class Utils{
	private static float SPACEINFRONT = .3f;
	
	/// <summary>
	/// Calculates the distance between two points
	/// </summary>
	public static float CalcDistance(float point1, float point2){
		return (Mathf.Abs(point1 - point2));
	}
	
	/// <summary>
	/// Calculates the difference between 2 points
	/// </summary>
	public static float CalcDifference(float point1, float point2){
		return (point1 - point2);
	}
	
	/// <summary>
	/// Returns true if the specified point is within the minimumDistance.
	/// </summary>
	public static bool Within(float point, float minimumDistance){
		return (point > -minimumDistance && point < minimumDistance);
	}
	
	/// <summary>
	/// Gets the point infront of the given gameobject from the start point. It will give a point that is right next to the object.
	/// </summary>
	public static Vector3 GetPointInfrontOf(Vector3 start, GameObject objectToMoveInfront){
		Vector3 whereToMove = objectToMoveInfront.transform.position;
		if (Utils.CalcDifference(start.x, whereToMove.x) < 0) { // if the target is to the right
			whereToMove.x = whereToMove.x - objectToMoveInfront.transform.localScale.x/2 - SPACEINFRONT;
		} else {
			whereToMove.x = whereToMove.x + objectToMoveInfront.transform.localScale.x/2 + SPACEINFRONT;
		}
		
		return (whereToMove);
	}
	
	/// <summary>
	/// Sets the given gameobject to the given active state. Usefull for bone animations
	/// </summary>
	public static void SetActiveRecursively(GameObject gameObject, bool active) {
		gameObject.SetActive (active);
    	foreach (Transform limb in gameObject.transform) {
        	SetActiveRecursively (limb.gameObject, active);
		}
	}
	
	/// returns the amount of time in ms to display, is kinda arbitrary
	public static float CalcTimeToDisplayText(string text){
		return (text.Length * .1f);
	}
}