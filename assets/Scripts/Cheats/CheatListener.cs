using UnityEngine;
using System.Collections;

public class CheatListener : MonoBehaviour {
    public bool isFlying = false;
    public bool isFastMovement = false;
    public bool isNoClip = false;
    public bool isSoundOff = true;
    
    public float speedIncrease = 2;
    
    protected Player player;
    protected LevelManager levelManager;
    protected CharacterController characterController;
    
    //private float currentHorizontalSpeed = 0.0f;
    
    void Start() {
        player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        characterController = GameObject.Find("PlayerCharacter").GetComponent<CharacterController>();
    }
    
    void Update() {
        /*Fly();
        MoveFast();
        ToggleSound();
        KeyboardMovement();*/
    }
    
    void KeyboardMovement(){
        /*float verticalMovement = Input.GetAxisRaw(Strings.ButtonVertical);
        float horizontalMovement = Input.GetAxisRaw(Strings.ButtonHorizontal);	
        
        currentHorizontalSpeed = player.walkSpeed * horizontalMovement;
        
        if(!player.isAffectedByGravity){
            player.currentVerticalSpeed = player.walkSpeed * verticalMovement;	
        }
        
        MovePlayer();*/
    }
    void MovePlayer(){
        // Calculate actual motion
		/*
        Vector3 movement = new Vector3(currentHorizontalSpeed, player.currentVerticalSpeed, 0 );
        movement *= Time.deltaTime;
        
        player.lastReturnedCollisionFlags = characterController.Move(movement);*/
    }
    
    void Fly() {/*
        if (isFlying){
            player.isAffectedByGravity = false;
        }
        
        if (Input.GetKeyDown(KeyCode.F)) {
            isFlying = !isFlying;
            
            if (!isFlying) {
                player.isAffectedByGravity = true;
            }
        }*/
    }
    
    void MoveFast() {		
        if (Input.GetKeyDown(KeyCode.M)) {
            isFastMovement = !isFastMovement;
            
            if (isFastMovement) {
                player.walkSpeed = player.walkSpeed * speedIncrease;
            } else {
                player.walkSpeed = player.walkSpeed / speedIncrease;	
            }
        }	
    }
    
    void NoClip() {		// NOTE DOES NOT WORK
        if (Input.GetKeyDown(KeyCode.N)) {
            isNoClip = !isNoClip;
            
            if (isNoClip) {
                characterController.detectCollisions = false;
            } else {
                characterController.detectCollisions = true;	
            }
        }	
    }
    
    void ToggleSound() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //isSoundOff = !isSoundOff;

            if (!isSoundOff)
            {
                AudioListener.pause = true;
            }
            else
            {
                AudioListener.pause = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Crossfade.CoroutineFadeOverTime("Forest", "Beach"));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Crossfade.CoroutineFadeOverTime("Beach", "Forest"));
        }
    }
}
