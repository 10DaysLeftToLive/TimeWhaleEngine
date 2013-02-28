using UnityEngine;
using System.Collections;

public class GrabableObject : MonoBehaviour {
	GameObject _player;
	bool _attachedToPlayer = false;
	private float xDifToPlayer;
	private float initialY;
	
	void Update () {
		if (_attachedToPlayer){
			Vector3 newPos = _player.transform.position;
			newPos.x += xDifToPlayer;
			newPos.y = initialY;
			this.transform.position = newPos;
		}
	}
	
	public void AttachToPlayer(GameObject player){
		float differenceBetweenCenters = (this.collider.bounds.size.x + player.collider.bounds.size.x)/2;
		initialY = transform.position.y;
		
		if (this.transform.position.x > player.transform.position.x){
			xDifToPlayer = differenceBetweenCenters;
		} else {
			xDifToPlayer = -differenceBetweenCenters;
		}
		_player = player;
		_attachedToPlayer = true;
	}
	
	public void DetachFromPlayer(){
		_attachedToPlayer = false;
	}
}
