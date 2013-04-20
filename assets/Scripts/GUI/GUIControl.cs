using UnityEngine;
using System.Collections;

/*
 * GUIControl.cs
 * 	A part that the gui manager will hold to display all the various menus
 * 	all menus will derive from this class and be given to the gui manager
 */

public abstract class GUIControl : MonoBehaviour {
	private bool initialized = false; // Records if this control has already been initialized or not
	protected GUIEvent currentResponse;
	
	public bool Initialized {
		get {return (initialized);}	
	}
	
	public void Initialize(){
		currentResponse = new GUIEvent();
		ClearResponse();
		Init();
		initialized = true;
	}
	
	public virtual void Init(){}// for children to load settings as needed
	public virtual void UpdateControl(){} // for updating as needed
	public abstract void Render(); // to display the content of the control
	
	public void ClearResponse(){
		currentResponse.type = GUIEventType.NULLEVENT;
	}
}
