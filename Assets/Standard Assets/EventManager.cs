using UnityEngine;
using System.Collections;
using System;

public class EventManager : MonoBehaviour {
    //EVENT: OnClick
    public delegate void mOnClickDelegate(EventManager EM, ClickPositionArgs e);
    //Event
    public event mOnClickDelegate mOnClickEvent;
    //Riser
    public void RiseOnClickEvent(ClickPositionArgs clickPosition){
        if(mOnClickEvent != null)  mOnClickEvent(this,clickPosition);
    }
	
	//EVENT: OnClickOnObject
    public delegate void mOnClickedObjectDelegate(EventManager EM, ClickedObjectArgs e);
    //Event
    public event mOnClickedObjectDelegate mOnClickObjectEvent;
    //Riser
    public void RiseOnClickedObjectEvent(ClickedObjectArgs clickObject){
        if(mOnClickObjectEvent != null)  mOnClickObjectEvent(this,clickObject);
    }
	
	private static EventManager em_instance = null;
	
	public static EventManager instance{
		get {
            if (em_instance == null) {
                //  FindObjectOfType(...) returns the first ScreenSetup object in the scene.
                em_instance =  FindObjectOfType(typeof (EventManager)) as EventManager;
            }
 
            // If it is still null, create a new instance
            if (em_instance == null) {
                GameObject obj = new GameObject("EventManager");
                em_instance = obj.AddComponent(typeof (EventManager)) as EventManager;
            }
 
            return em_instance;
        }
	}
	
	void OnApplicationQuit() {
        em_instance = null;
    }
}

public class ClickPositionArgs : EventArgs {
    public Vector3 position;
	public ClickPositionArgs(Vector2 _position){
		position = new Vector3(_position.x, _position.y, 0);
	}
}

public class ClickedObjectArgs : EventArgs {
	public GameObject clickedObject;
	public ClickedObjectArgs(GameObject _clickedObject){
		clickedObject = _clickedObject;
	}
}