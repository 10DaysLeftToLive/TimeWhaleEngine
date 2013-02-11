using UnityEngine;
using System.Collections;
using System;

/*
 * OnClick.cs
 * 	Base class for all other OnClick event types
 * 	Whenever an objet is clicked on, this script will activate and will determine if the attached object was the
 * 	one clicked on. If it is then DoClick() will be called in the child script.
 */

public abstract class OnClick : MonoBehaviour {	
	protected EventManager.mOnClickDelegate delagate;
	
	void Start () {
		InitEvent();
	}
	
	protected abstract void DoClick(ClickPositionArgs e);
	
	private void OnClickEvent (EventManager EM, ClickPositionArgs e){	
		Ray ray = Camera.main.ScreenPointToRay (e.position);
    	RaycastHit hit;
		if(this.collider.Raycast(ray, out hit, 10)) {
			DoClick(e);
		}
    }
	
	private void OnApplicationQuit (){
		EventManager.instance.mOnClickEvent -= 	delagate;
	}
	
	protected void InitEvent(){
		delagate = new EventManager.mOnClickDelegate (OnClickEvent);
		EventManager.instance.mOnClickEvent += delagate;
	}
}
