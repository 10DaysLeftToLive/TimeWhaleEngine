using UnityEngine;
using System.Collections;
using System;

public class OnClick : MonoBehaviour {	
	void Start () {
		InitEvent();
	}
	
	protected virtual void DoClick(){}
	
	private void OnClickEvent (EventManager EM, ClickPositionArgs e){		
		Ray ray = Camera.main.ScreenPointToRay (e.position);
    	RaycastHit hit;
		if(this.collider.Raycast(ray, out hit, 10)) {
			DoClick();
			NotifyObjectClickedOn();
		} else if (!Physics.Raycast(ray,10)) {
			NotifyNoObjectClickedOn(e.position);
		}
    }
	
	private void NotifyObjectClickedOn(){
		EventManager.instance.RiseOnClickedObjectEvent(new ClickedObjectArgs(this.gameObject));
	}
	
	private void NotifyNoObjectClickedOn(Vector3 position){
		Debug.Log("No Object was clicked on");
		EventManager.instance.RiseOnClickedNoObjectEvent(new ClickPositionArgs(position));
	}
	
	protected void InitEvent(){
		EventManager.instance.mOnClickEvent += new EventManager.mOnClickDelegate (OnClickEvent);
	}
}
