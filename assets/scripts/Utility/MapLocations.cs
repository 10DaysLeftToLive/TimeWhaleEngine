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
	
	#region LightHouse Location
	
	static Vector3 _lighthouse = new Vector3(58.92967f, 14.97302f, 0f);
	public static Vector3 LightHouseYoung {
		get { return new Vector3(_lighthouse.x, _lighthouse.y, _lighthouse.z); }
	}
	
	public static Vector3 LightHouseMiddle {
		get { return new Vector3(_lighthouse.x, _lighthouse.y + LevelManager.levelYOffSetFromCenter, _lighthouse.z); }
	}
	
	public static Vector3 LightHouseOld {
		get { return new Vector3(_lighthouse.x, _lighthouse.y + (LevelManager.levelYOffSetFromCenter * 2), _lighthouse.z); }
	}
	
	#endregion
	
	#region Reflection Tree
	
	static Vector3 _reflectionTree = new Vector3(-41.09068f, 17.48419f, 0f);
	
	public static Vector3 ReflectionTreeYoung {
		get{return new Vector3(_reflectionTree.x, _reflectionTree.y, _reflectionTree.z);}	
	}
	public static Vector3 ReflectionTreeMiddle {
		get{return new Vector3(_reflectionTree.x, _reflectionTree.y + LevelManager.levelYOffSetFromCenter, _reflectionTree.z);}	
	}
	public static Vector3 ReflectionTreeOld {
		get{return new Vector3(_reflectionTree.x, _reflectionTree.y + (LevelManager.levelYOffSetFromCenter * 2), _reflectionTree.z);}	
	}
	#endregion
	
	#region Bridge
		static Vector3 _bridge = new Vector3(58.92967f, 14.97302f, 0f);
		public static Vector3 BridgeYoung {
			get { return new Vector3(_bridge.x, _bridge.y, _bridge.z); }
		}
	
		public static Vector3 BridgeMiddle {
			get { return new Vector3(_bridge.x, _bridge.y + LevelManager.levelYOffSetFromCenter, _bridge.z); }
		}
	
		public static Vector3 BridgeeOld {
			get { return new Vector3(_bridge.x, _bridge.y + (LevelManager.levelYOffSetFromCenter * 2), _bridge.z); }
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
	
	public static Vector3 MiddleOfHauntedForestMiddle {
		get {return new Vector3 (_MiddleOfHauntedForest.x, _MiddleOfHauntedForest.y + LevelManager.levelYOffSetFromCenter, _MiddleOfHauntedForest.z);}
	}
	
	#endregion
}
