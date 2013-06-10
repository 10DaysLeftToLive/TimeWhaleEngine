using UnityEngine;
using System.Collections;

/// <summary>
/// NPC give item action. Will create a given object at the feet of the given npc. The object to make must be in the
/// resources folder and will be loaded by it's string name
/// </summary>
public class NPCGiveItemAction : Action {
	public delegate string ItemToGetFunction();
	private string _itemToGiveName;
	private NPC _npcToGiveItem;
	private ItemToGetFunction functToGetItem = null;
	
	public NPCGiveItemAction(){}
	
	public NPCGiveItemAction(NPC npcToGiveItem, string itemToGiveName){
		_itemToGiveName = itemToGiveName;
		_npcToGiveItem = npcToGiveItem;
	}
	
	public NPCGiveItemAction(NPC npcToGiveItem, ItemToGetFunction itemToGiveFNCallback){
		_npcToGiveItem = npcToGiveItem;
		functToGetItem = itemToGiveFNCallback;
	}
	
	public override void Perform(){
		if (functToGetItem != null){
			_itemToGiveName = functToGetItem();
		}
		Object itemToPlace = Resources.Load("Prefabs/Items/" + _itemToGiveName);
		if (itemToPlace == null){
			Debug.Log("Did not find " + _itemToGiveName);
			return;
		}
		Object newItem = GameObject.Instantiate(itemToPlace, _npcToGiveItem.GetFeet(), Quaternion.identity);
		newItem.name = _itemToGiveName;
	}
}
