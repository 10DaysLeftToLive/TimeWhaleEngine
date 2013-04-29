using UnityEngine;
using System.Collections;

public class NPCGiveItemAction : Action {
	private GameObject _itemToGive;
	private NPC _npcToGiveItem;
	
	public NPCGiveItemAction(){}
	
	public NPCGiveItemAction(NPC npcToGiveItem, GameObject itemToGive){
		_itemToGive = itemToGive;
		_npcToGiveItem = npcToGiveItem;
	}
	
	public override void Perform(){
		GameObject.Instantiate(_itemToGive, _npcToGiveItem.GetFeet(), Quaternion.identity);
	}
}
