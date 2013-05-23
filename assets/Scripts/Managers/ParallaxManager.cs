using UnityEngine;
using System.Collections;

public class ParallaxManager : MonoBehaviour {
	public float speedOne = 0.175F;
	public float speedTwo = 0.1F;
	public float speedThree = 0.05F;
	public float speedFour = 0.025F;
	private Vector3 newPos;
	private Player player;
	private Vector3 prevPos;
	private Vector3 deltaPos;
	public Vector3 DeltaPos {
		get { return deltaPos; }
	}
	private GameObject[] parallaxObjects;
	
	void Start () {
		
	}
	
	public void Init() {
		player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
		prevPos = player.transform.position;
        parallaxObjects = GameObject.FindGameObjectsWithTag(Strings.PARALLAX);
	}
	
	void Update () {
		if (parallaxObjects != null) {
			deltaPos = prevPos - player.transform.position;
			prevPos = player.transform.position;
			foreach (GameObject obj in parallaxObjects) {
				switch(LayerMask.LayerToName(obj.layer)) {
					case Strings.PARALLAXONE:
						moveObject(speedOne, obj);
						break;
					case Strings.PARALLAXTWO:
						moveObject(speedTwo, obj);
						break;
					case Strings.PARALLAXTHREE:
						moveObject(speedThree, obj);
						break;
					case Strings.PARALLAXFOUR:
						moveObject(speedFour, obj);
						break;
					default:
						break;
				}
			}
		}
	}
	
	private void moveObject(float speed, GameObject obj) {
		newPos = obj.transform.position;
		newPos.x += deltaPos.x * speed;
		obj.transform.position = newPos;
	}
}
