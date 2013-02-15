using UnityEngine;
using System.Collections;

public class ManagerLoader {	
	private static Transform _youngRoot, _middleRoot, _oldRoot; 
	
	public static void LoadManagers(Transform youngRoot, Transform middleRoot, Transform oldRoot){
		_youngRoot = youngRoot;
		_middleRoot = middleRoot;
		_oldRoot = oldRoot;
		
		LoadBuildingManager();
		LoadLockedDoorManager();
		LoadNPCDispositionManager();
	}
	
	private static void LoadBuildingManager(){
		BuildingManager.instance.LoadInObjectsToManage(_youngRoot, CharacterAgeState.YOUNG);
		BuildingManager.instance.LoadInObjectsToManage(_middleRoot, CharacterAgeState.MIDDLE);
		BuildingManager.instance.LoadInObjectsToManage(_oldRoot, CharacterAgeState.OLD);
	}
	
	private static void LoadLockedDoorManager(){
		LockedDoorManager.instance.LoadInObjectsToManage(_youngRoot, CharacterAgeState.YOUNG);
		LockedDoorManager.instance.LoadInObjectsToManage(_middleRoot, CharacterAgeState.MIDDLE);
		LockedDoorManager.instance.LoadInObjectsToManage(_oldRoot, CharacterAgeState.OLD);
	}
	
	private static void LoadNPCDispositionManager(){
		NPCDispositionManager.instance.LoadInNPCs(_youngRoot, CharacterAgeState.YOUNG);
		NPCDispositionManager.instance.LoadInNPCs(_middleRoot, CharacterAgeState.MIDDLE);
		NPCDispositionManager.instance.LoadInNPCs(_oldRoot, CharacterAgeState.OLD);
	}
}
