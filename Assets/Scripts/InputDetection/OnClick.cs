using UnityEngine;
using System.Collections;
using System;

public class OnClick : MonoBehaviour {	
	protected EventManager.mOnClickDelegate delagate;
	
	void Start () {
		InitEvent();
	}
	
	protected virtual void DoClick(){}
	
	private void OnClickEvent (EventManager EM, ClickPositionArgs e){	
		Ray ray = Camera.main.ScreenPointToRay (e.position);
    	RaycastHit hit;
		if(this.collider.Raycast(ray, out hit, 10)) {
			DoClick();
		}
    }
	
	private void OnApplicationQuit (){
		EventManager.instance.mOnClickEvent -= 	delagate;
	}
	
	private void NotifyObjectClickedOn(){
		EventManager.instance.RiseOnClickedObjectEvent(new ClickedObjectArgs(this.gameObject));
	}
	
	private void NotifyNoObjectClickedOn(Vector3 position){
		EventManager.instance.RiseOnClickedNoObjectEvent(new ClickPositionArgs(position));
	}
	
	protected void InitEvent(){
		delagate = new EventManager.mOnClickDelegate (OnClickEvent);
		EventManager.instance.mOnClickEvent += delagate;
	}
}
