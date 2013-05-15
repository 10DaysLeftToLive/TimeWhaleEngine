using UnityEngine;
using System.Collections;

/*
 * GUIControl.cs
 * 	A part that the gui manager will hold to display all the various menus
 * 	all menus will derive from this class and be given to the gui manager
 */

public abstract class GUIControl : MonoBehaviour {
	private bool initialized = false; // Records if this control has already been initialized or not
	private bool isClickable = true; // used for disabling buttons but still rendering them
	
	public bool Initialized {
		get {return (initialized);}	
	}
	
	public void Initialize(){
		Init();
		initialized = true;
	}
	
	public virtual void Init(){}// for children to load settings as needed
	public virtual void UpdateControl(){} // for updating as needed
	public abstract void Render(); // to display the content of the control
	public virtual bool ClickOnGUI(Vector2 screenPos){return (false);}
	
	public void ToggleClickable(bool newClickableState){
		isClickable = newClickableState;	
	}
	
	protected bool ButtonClick(Rect buttonArea, string text){
		if (isClickable){	
			return (GUI.Button(buttonArea, text));	
		} 
		GUI.Box(buttonArea, text);
		return (false);
	}
	
	protected bool ButtonClick(Rect buttonArea, string text, GUIStyle guiStyle){
		if (isClickable){	
			return (GUI.Button(buttonArea, text, guiStyle));	
		} 
		GUI.Box(buttonArea, text, guiStyle);
		return (false);
	}
}
