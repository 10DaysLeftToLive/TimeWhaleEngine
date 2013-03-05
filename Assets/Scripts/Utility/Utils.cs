using UnityEngine;
using System.Collections;

public static class Utils{
	public static float CalcDistance(float point1, float point2){
		return (Mathf.Abs(point1 - point2));
	}
	
	public static float CalcDifference(float point1, float point2){
		return (point1 - point2);
	}
	
	public static bool Within(float point, float minimumDistance){
		return (point > -minimumDistance && point < minimumDistance);
	}
}