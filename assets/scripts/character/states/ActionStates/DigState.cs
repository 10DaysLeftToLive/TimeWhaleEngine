using UnityEngine;
using System.Collections;

public class DigState : PlayAnimationThenDoState {
	public delegate string ItemToGetFunction();
	private ItemToGetFunction functToGetItem = null;
	private string _itemToDrop;
	private float _timeToDig;
	private float DIG_TIME = 2f;
	
	public DigState(Character toControl) : base(toControl, Strings.animation_dig) {
		_itemToDrop = "";
		_timeToDig = DIG_TIME;
	}
	
	public DigState(Character toControl, string itemToDrop) : base(toControl, Strings.animation_dig) {
		_itemToDrop = itemToDrop;
		_timeToDig = DIG_TIME;
	}
	
	public override void Update() {
		_timeToDig -= Time.deltaTime;
		
		if (character is NPC && _timeToDig <= 0){
			//drop item
			if (_itemToDrop != "") {
				Object itemToPlace = Resources.Load(_itemToDrop);
				if (itemToPlace == null){
					Debug.Log("Did not find " + _itemToDrop);
					return;
				}
				Object newItem = GameObject.Instantiate(itemToPlace, character.GetFeet(), Quaternion.identity);
				newItem.name = _itemToDrop;
			}
			
			character.EnterState(new MarkTaskDone(character));
		}
		
		base.Update();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		DebugManager.instance.Log(character.name + 
			": DigState Enter to drop " + _itemToDrop, "State", character.name);
	}
	
	public override void OnExit() {
		DebugManager.instance.Log(character.name + ": DigState Exit", "State", character.name);
	}
}
