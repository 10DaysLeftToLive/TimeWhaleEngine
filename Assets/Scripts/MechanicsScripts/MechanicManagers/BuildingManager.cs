using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {
	Dictionary<int, BuildingContainer> buildings;
	
	void Awake(){
		buildings = new Dictionary<int, BuildingContainer >();
	}
	
	public void LoadInBuildings(Transform buildingsRoot, CharacterAgeState age){
		for (int i = 0; i < buildingsRoot.transform.childCount; i++){
			Building building = (Building) buildingsRoot.transform.GetChild(i).GetComponent<Building>();
			if (!buildings.ContainsKey(building.id)){
				buildings.Add(building.id, new BuildingContainer());
			}
			buildings[building.id].Add(building, age);
		}
	}
	
	public void ToggleWithId(bool interiorIsShowing, int id){
		if (buildings.ContainsKey(id)){
			buildings[id].ToggleAll();
		} else {
			Debug.LogWarning("BuildingManager does not contain a building with id " + id);
		}
	}
	
	public void Add(Building toAdd, CharacterAgeState age){		
		buildings[toAdd.id].Add(toAdd, age);
	}
	
	#region Singleton
	private static BuildingManager bm_instance = null;
	
	public static BuildingManager instance{
		get {
            if (bm_instance == null) {
                bm_instance = FindObjectOfType(typeof (BuildingManager)) as BuildingManager;
            }
 
            // If it is still null, create a new instance
            if (bm_instance == null) {
                GameObject obj = new GameObject("BuildingManager");
                bm_instance = obj.AddComponent(typeof (BuildingManager)) as BuildingManager;
            }
 
            return bm_instance;
        }
	}
	#endregion
}