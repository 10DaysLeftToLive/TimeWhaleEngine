using UnityEngine;
using System.Collections;
using System;

public class EventManager : MonoBehaviour {
	public EventArgs e = null;
 
    //EVENT: OnClick
    public delegate void mOnClickDelegate(EventManager EM, EventArgs e);
    //Event
    public event mOnClickDelegate mOnClickEvent;
	
    //Riser
    public void RiseOnClickEvent(){
        if(mOnClickEvent != null)  mOnClickEvent(this,e);
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