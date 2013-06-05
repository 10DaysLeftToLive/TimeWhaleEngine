using UnityEngine;
using System.Collections;

public class MapLocations {
	static Vector3 _carpenterHouse = new Vector3(30, -1.5f, -0.5f);
	public static Vector3 CarpenterHouseYoung{
		get{return _carpenterHouse;}
	}
	public static Vector3 CarpenterHouseMiddle{
		get{return new Vector3(_carpenterHouse.x, _carpenterHouse.y + LevelManager.levelYOffSetFromCenter, _carpenterHouse.z);}
	}
	public static Vector3 CarpenterHouseOld{
		get{return new Vector3(_carpenterHouse.x, _carpenterHouse.y + (LevelManager.levelYOffSetFromCenter * 2), _carpenterHouse.z);}
	}
	
	static Vector3 _TopOfFirstFloorStairsRight = new Vector3(38.27f, 7.73f, -0.5f);
	public static Vector3 TopOfFirstFloorStairsRightYoung{
		get{return _TopOfFirstFloorStairsRight;}
	}
	
	public static Vector3 TopOfFirstFloorStairsRightMiddle{
		get{return new Vector3(_TopOfFirstFloorStairsRight.x, _TopOfFirstFloorStairsRight.y + LevelManager.levelYOffSetFromCenter, _TopOfFirstFloorStairsRight.z);}
	}
	
	public static Vector3 TopOfFirstFloorStairsRightOld{
		get{return new Vector3(_TopOfFirstFloorStairsRight.x, _TopOfFirstFloorStairsRight.y + (LevelManager.levelYOffSetFromCenter * 2), _TopOfFirstFloorStairsRight.z);}
	}
	
	static Vector3 _baseOfPier = new Vector3(69.0f, -3.5f, -0.5f);
	public static Vector3 BaseOfPierYoung{
		get{return _baseOfPier;}
	}
	
	public static Vector3 BaseOfPierMiddle{
		get{return new Vector3(_baseOfPier.x, _baseOfPier.y + LevelManager.levelYOffSetFromCenter, _baseOfPier.z);}
	}
	
	public static Vector3 BaseOfPierOld{
		get{return new Vector3(_baseOfPier.x, _baseOfPier.y + (LevelManager.levelYOffSetFromCenter * 2), _baseOfPier.z);}
	}
	static Vector3 _middleOfBeach = new Vector3(49.7f, -7.4f, -0.5f);
	public static Vector3 MiddleOfBeachYoung{
		get{return _middleOfBeach;}
	}
	public static Vector3 MiddleOfBeachMiddle{
		get{return new Vector3(_middleOfBeach.x, _middleOfBeach.y + LevelManager.levelYOffSetFromCenter, _middleOfBeach.z);}
	}
	
	public static Vector3 MiddleOfBeachOld{
		get{return new Vector3(_middleOfBeach.x, _middleOfBeach.y + (LevelManager.levelYOffSetFromCenter * 2), _middleOfBeach.z);}
	}
	
	static Vector3 _upperMiddleBeach = new Vector3(62f, -6f, -.5f);
	public static Vector3 UpperMiddleBeachYoung{
		get{return _upperMiddleBeach;}	
	}
	public static Vector3 UpperMiddleBeachMiddle{
		get{return new Vector3(_upperMiddleBeach.x, _upperMiddleBeach.y + LevelManager.levelYOffSetFromCenter, _upperMiddleBeach.z);}	
	}
	public static Vector3 UpperMiddleBeachOld{
		get{return new Vector3(_upperMiddleBeach.x, _upperMiddleBeach.y + (LevelManager.levelYOffSetFromCenter * 2), _upperMiddleBeach.z);}	
	}
	
	#region WindMill Location
	static Vector3 _windMillLedge = new Vector3(-4.283016f, 11.34081f, 0f);
	public static Vector3 WindmillYoung {
		get{return new Vector3(_windMillLedge.x, _windMillLedge.y, _upperMiddleBeach.z);}	
	}
	public static Vector3 WindmillMiddle {
		get{return new Vector3(_windMillLedge.x, _windMillLedge.y + LevelManager.levelYOffSetFromCenter, _windMillLedge.z);}	
	}
	public static Vector3 WindmillOld {
		get{return new Vector3(_windMillLedge.x, _windMillLedge.y + (LevelManager.levelYOffSetFromCenter * 2), _windMillLedge.z);}	
	}
	
	#endregion
	
	#region SiblingOld
	static Vector3 _playerHouseWaterWell = new Vector3 (-2.4f, -2.4f, 0);
	public static Vector3 PlayerHouseWaterWellOld {
		get	{return new Vector3 (_playerHouseWaterWell.x, _playerHouseWaterWell.y + (LevelManager.levelYOffSetFromCenter * 2), _playerHouseWaterWell.z);}
	}
	
	static Vector3 _MiddleOfHauntedForest = new Vector3 (-28.25f, -2.4f, 0);
	public static Vector3 MiddleOfHauntedForestOld {
		get	{return new Vector3 (_MiddleOfHauntedForest.x, _MiddleOfHauntedForest.y + (LevelManager.levelYOffSetFromCenter * 2), _MiddleOfHauntedForest.z);}
	}
	
	#endregion
}
