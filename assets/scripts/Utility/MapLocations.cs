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
}
