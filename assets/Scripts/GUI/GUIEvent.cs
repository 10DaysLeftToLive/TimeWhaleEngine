using UnityEngine;
using System.Collections;

public enum GUIEventType {
	NULLEVENT, // placeholder event
	
	
}

public class GUIEvent {
	public GUIEventType type = GUIEventType.NULLEVENT;
}
