using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent (typeof (Sprite))]
public class StairsZDepthTracker : MonoBehaviour {
	private Player player;
	private Sprite smSprite;
	
	private readonly float BEHIND_PLAYER_LOCAL_Z = -1;
	private readonly float IN_FRONT_PLAYER_LOCAL_Z = -5;
	
	// Use this for initialization
	void Start () {
		try{
			player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		}catch{
			Debug.LogWarning("Stairs could not find PlayerCharacter");	
		}
		smSprite = GetComponent<Sprite>();
		smSprite.SetSizeMode(Sprite.SIZE_MODE.Absolute);
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y < (transform.position.y - smSprite.size.y/2)){
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, BEHIND_PLAYER_LOCAL_Z); 
		}else{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, IN_FRONT_PLAYER_LOCAL_Z); 	
		}
	}
}
