using UnityEngine;
using System.Collections;
using System;

public class EventManager : ManagerSingleton<EventManager> {
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
	
	//EVENT: OnClickOnNoObject
    public delegate void mOnClickedNoObjectDelegate(EventManager EM, ClickPositionArgs e);
    //Event
    public event mOnClickedNoObjectDelegate mOnClickNoObjectEvent;
    //Riser
    public void RiseOnClickedNoObjectEvent(ClickPositionArgs clickPosition){
        if(mOnClickNoObjectEvent != null)  mOnClickNoObjectEvent(this,clickPosition);
    }
	
	//EVENT: OnPauseToggle
    public delegate void mOnPauseToggleDelegate(EventManager EM, PauseStateArgs e);
    //Event
    public event mOnPauseToggleDelegate mOnPauseToggleEvent;
    //Riser
    public void RiseOnPauseToggleEvent(PauseStateArgs pauseState){
        if(mOnPauseToggleEvent != null)  mOnPauseToggleEvent(this,pauseState);
    }
	
	//EVENT: OnClickOnObjectAwayFromPlayer
    public delegate void mOnClickOnObjectAwayFromPlayerDelegate(EventManager EM, ClickedObjectArgs e);
    //Event
    public event mOnClickOnObjectAwayFromPlayerDelegate mOnClickOnObjectAwayFromPlayerEvent;
    //Riser
    public void RiseOnClickOnObjectAwayFromPlayerEvent(ClickedObjectArgs e){
        if(mOnClickOnObjectAwayFromPlayerEvent != null)  mOnClickOnObjectAwayFromPlayerEvent(this,e);
    }
	
	//EVENT: onClickOnPlayer
	public delegate void mOnClickOnPlayerDelegate(EventManager EM);
    //Event
    public event mOnClickOnPlayerDelegate mOnClickOnPlayerEvent;
    //Riser
    public void RiseOnClickOnPlayerEvent(){
        if(mOnClickOnPlayerEvent != null)  mOnClickOnPlayerEvent(this);
    }
	
	//EVENT: onNPCInteraction
	public delegate void mOnNPCInteractionDelegate(EventManager EM, NPCInteraction interaction);
    //Event
    public event mOnNPCInteractionDelegate mOnNPCInteractionEvent;
    //Riser
    public void RiseOnNPCInteractionEvent(NPCInteraction interaction){
        if(mOnNPCInteractionEvent != null)  mOnNPCInteractionEvent(this, interaction);
    }
	
	//EVENT: onPlayerPickupItem
	public delegate void mOnPlayerPickupItemDelegate(EventManager EM, PickUpStateArgs pickedUpItem);
    //Event
    public event mOnPlayerPickupItemDelegate mOnPlayerPickupItemEvent;
    //Riser
    public void RiseOnPlayerPickupEvent(PickUpStateArgs interaction){
        if(mOnPlayerPickupItemEvent != null)  mOnPlayerPickupItemEvent(this, interaction);
    }
	
	//EVENT: onPlayerTriggerCollision
	public delegate void mOnPlayerTriggerCollisionDelegate(EventManager EM, TriggerCollisionArgs triggerCollided);
    //Event
    public event mOnPlayerTriggerCollisionDelegate mOnPlayerTriggerCollisionEvent;
    //Riser
    public void RiseOnPlayerTriggerCollisionEvent(TriggerCollisionArgs interaction){
        if(mOnPlayerTriggerCollisionEvent != null)  mOnPlayerTriggerCollisionEvent(this, interaction);
    }
	
	//EVENT: onPlayerTriggerCollision
	public delegate void mOnDragDelegate(EventManager EM, DragArgs dragMagnitde);
    //Event
    public event mOnDragDelegate mOnDragEvent;
    //Riser
    public void RiseOnDragEvent(DragArgs dragMagnitude) {
        if(mOnDragEvent != null)  mOnDragEvent(this, dragMagnitude);
    }
}

public class ClickPositionArgs : EventArgs {
    public Vector3 position;
	public ClickPositionArgs(Vector2 _position){
		position = new Vector3(_position.x, _position.y, 0);
	}
	public ClickPositionArgs(Vector3 _position){
		position = _position;
	}
}

public class ClickedObjectArgs : EventArgs {
	public GameObject clickedObject;
	public ClickedObjectArgs(GameObject _clickedObject){
		clickedObject = _clickedObject;
	}
}

public class PauseStateArgs : EventArgs {
	public bool isPaused;
	public PauseStateArgs(bool _isPaused){
		isPaused = _isPaused;
	}
}

public class PickUpStateArgs : EventArgs {
	public GameObject itemPickedUp;
	public PickUpStateArgs(GameObject _itemPickedUp){
		itemPickedUp = _itemPickedUp;
	}
}

public class TriggerCollisionArgs : EventArgs {
	public GameObject triggerCollided;
	public TriggerCollisionArgs(GameObject _triggerCollided){
		triggerCollided = _triggerCollided;
	}
}

public class DragArgs : EventArgs {
	public Vector2 dragMagnitude;
	public DragArgs(Vector2 _dragMagnitude){
		dragMagnitude = _dragMagnitude;
	}
}
