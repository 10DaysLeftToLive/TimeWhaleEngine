using UnityEngine;
using System.Collections;

public static class Utils{
	public static float CalcDistance(float point1, float point2){
		return (Mathf.Abs(point1 - point2));
	}
}