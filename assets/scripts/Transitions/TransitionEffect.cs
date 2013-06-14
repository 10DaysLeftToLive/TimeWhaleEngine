using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public abstract class TransitionEffect : PauseObject {
	protected ParticleSystem emitter;
	protected Camera cameraMain;
	protected DragDirection directionFacing = DragDirection.Up;// defaults to up
	protected bool isChanging = false;
	
	protected float time = 0;
	protected bool didChange = false;
	
	public int minimumDragDistance = 5;
	public float timeToChange = .6f;
	public float SWITCHTIMESECONDS = .5f; // seconds
	
	#region Setup
	void Start () {
		cameraMain = Camera.main;
		if (cameraMain == null){
			Debug.LogError("No main camera found");
			cameraMain = GameObject.Find("Camera").GetComponent<Camera>();
		}
		emitter = GetComponent<ParticleSystem>();
		emitter.Stop();
		EventManager.instance.mOnDragEvent += new EventManager.mOnDragDelegate (OnDragEvent);
	}
	
	protected virtual void Init(){
		PlaceEmitter(emitter, cameraMain, DragDirection.Up);
	}
	
	protected void PlaceEmitter(ParticleSystem emitterObj, Camera camera, DragDirection directionToFace){
		Vector3 localPos = emitterObj.transform.localPosition;
		if (directionFacing != directionToFace){
			ToggleEmitter();
		}
		localPos.y = 13 * (float)directionToFace;
		emitterObj.transform.localPosition = localPos;
	}
	#endregion
	
	protected abstract void OnDragEvent(EventManager EM, DragArgs dragInformation);
	
	protected void DoFade(){
		emitter.Play();
		time = 0;
		isChanging = true;
		didChange = false;
		EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(true));
	}
	
	protected void ToggleEmitter(){
		Vector3 newPos = emitter.transform.localPosition;
		newPos.y *= -1;
		emitter.transform.localPosition = newPos;
		
		emitter.startSpeed *= -1;
		directionFacing = (directionFacing == DragDirection.Up ? DragDirection.Down : DragDirection.Up);
	}
	
	protected void Update() {
		if (isChanging){
			time += Time.deltaTime;
			if (!didChange && time >= SWITCHTIMESECONDS * timeToChange){
				DoSwitchAction();
				didChange = true;
			}
			if (time > SWITCHTIMESECONDS){
				isChanging = false;
				emitter.Stop();
				EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(false));
			}
		}
	}
	
	protected virtual void DoSwitchAction(){}
	protected virtual bool CanShift(){return (true);}
}

public enum DragDirection{
	Up = 1,
	Down = -1
}