using UnityEngine;
using System.Collections;
using System;

public class OnClick : MonoBehaviour {	
	void Start () {
		EventManager.instance.mOnClickEvent += new EventManager.mOnClickDelegate (OnClickEvent);
	}
	
	protected virtual void DoClick(){}
	
	private void OnClickEvent (EventManager EM, EventArgs e){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    	RaycastHit hit;
		if(this.collider.Raycast(ray, out hit, 100)) {
			DoClick();
		}
    }
}
