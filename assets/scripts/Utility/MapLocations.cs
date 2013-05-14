using UnityEngine;
using System.Collections;

public class MapLocations {
	static Vector3 _carpenterHouse = new Vector3(30, -1.5f, -0.5f);
	public static Vector3 CarpenterHouseYoung{
		get{return _carpenterHouse;}
	}
	public static Vector3 CarpenterHouseMiddle{
		get{return _carpenterHouse * LevelManager.levelYOffSetFromCenter;}
	}
	public static Vector3 CarpenterHouseOld{
		get{return _carpenterHouse * LevelManager.levelYOffSetFromCenter * 2;}
	}
	
	static Vector3 _TopOfFirstFloorStairsRight = new Vector3(38.27f, 7.73f, -0.5f);
	public static Vector3 _TopOfFirstFloorStairsRightYoung{
		get{return _TopOfFirstFloorStairsRight;}
	}
	public static Vector3 _TopOfFirstFloorStairsRightMiddle{
		get{return _TopOfFirstFloorStairsRight * LevelManager.levelYOffSetFromCenter;}
	}
	public static Vector3 _TopOfFirstFloorStairsRightOld{
		get{return _TopOfFirstFloorStairsRight * LevelManager.levelYOffSetFromCenter * 2;}
	}
}
