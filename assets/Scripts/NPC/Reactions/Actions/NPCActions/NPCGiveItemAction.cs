using UnityEngine;
using System.Collections;

/// <summary>
/// NPC give item action. Will create a given object at the feet of the given npc. The object to make must be in the
/// resources folder and will be loaded by it's string name
/// </summary>
public class NPCGiveItemAction : Action {
	private string _itemToGiveName;
	private NPC _npcToGiveItem;
	
	public NPCGiveItemAction(){}
	
	public NPCGiveItemAction(NPC npcToGiveItem, string itemToGiveName){
		_itemToGiveName = itemToGiveName;
		_npcToGiveItem = npcToGiveItem;
	}
	
	public override void Perform(){
		Object itemToPlace = Resources.Load(_itemToGiveName);
		if (itemToPlace == null){
			Debug.Log("Did not find " + _itemToGiveName);
			return;
		}
		GameObject.Instantiate(itemToPlace, _npcToGiveItem.GetFeet(), Quaternion.identity);
	}
}
