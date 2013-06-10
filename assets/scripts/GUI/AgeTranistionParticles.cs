using UnityEngine;
using System.Collections;
/*
public class AgeTranstionParticles : PauseObject {
	public ParticleSystem emitter;
	public float time = 0;
	public float SWITCHTIME = 4; // seconds
	private bool isChanging = false;
	public int minimumDragDistance = 5;
	public float timeToChange = .6f;
	public LevelManager levelManager;
	public Player playerCharacter;
	bool goingUp;
	bool didChange = false;
	bool facingUp = true;
	
	void Start () {
		emitter.Stop();
		EventManager.instance.mOnDragEvent += new EventManager.mOnDragDelegate (OnDragEvent);
	}
	
	private void OnDragEvent(EventManager EM, DragArgs dragInformation) {
		Vector2 inputChangeSinceLastTick = dragInformation.dragMagnitude;
		if (inputChangeSinceLastTick.y > 0 &&
			inputChangeSinceLastTick.x == 0 && inputChangeSinceLastTick.magnitude > minimumDragDistance) {
			OnDragUp();
		}
		else if (inputChangeSinceLastTick.y < 0 &&
			inputChangeSinceLastTick.x == 0 && inputChangeSinceLastTick.magnitude > minimumDragDistance) {
			OnDragDown();
		}
	}
	
	protected virtual void OnDragDown() {
		if (CanShift() && levelManager.CanAgeTransitionDown()) {
			goingUp = false;
			if (facingUp){
				//ToggleEmitter();
			}
			DoFade();
		}
	}
	
	protected virtual void OnDragUp() {
		if (CanShift() && levelManager.CanAgeTransitionUp()) {
			goingUp = true;
			if (!facingUp){
				//ToggleEmitter();
			}
			DoFade();
		}
	}
	
	private bool CanShift(){
		return (playerCharacter.State != typeof(MoveState) && !isChanging);
	}
	
	private void DoFade(){
		Debug.Log("Doing fade");
		emitter.Play();
		time = 0;
		isChanging = true;
		didChange = false;
		EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(true));
	}
	
	protected void Update() {
		if (isChanging){
			time += Time.deltaTime;
			if (!didChange && time >= SWITCHTIME * timeToChange){
				Debug.Log("Changing ages");
				if (goingUp){
					levelManager.ShiftUpAge();
				} else {
					levelManager.ShiftDownAge();
				}
				didChange = true;
			}
			if (time > SWITCHTIME){
				isChanging = false;
				emitter.Stop();
				Debug.Log("now in " + CharacterAgeManager.currentAge);
				EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(false));
			}
		}
	}
}
*/