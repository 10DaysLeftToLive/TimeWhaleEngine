using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour {
public float speedOne = 0.075F;
	public float speedTwo = 0.05F;
	public float speedThree = 0.025F;
	private GameObject[] cloudObjects;
	private Vector3 newPos;
	private static int ISLAND_START = -40;
	private static int ISLAND_END = 65;
	
	void Start () {
		
	}
	
	public void Init() {
        cloudObjects = GameObject.FindGameObjectsWithTag(Strings.CLOUD);
	}
	
	void Update () {
		if (cloudObjects != null) {
			foreach (GameObject obj in cloudObjects) {
				switch(LayerMask.LayerToName(obj.layer)) {
					case Strings.CLOUDONE:
						moveObject(speedOne, obj);
						break;
					case Strings.CLOUDTWO:
						moveObject(speedTwo, obj);
						break;
					case Strings.CLOUDTHREE:
						moveObject(speedThree, obj);
						break;
					default:
						break;
				}
			}
		}
	}
	
	private void moveObject(float speed, GameObject obj) {
		newPos = obj.transform.position;
		newPos.x += speed;
		if (newPos.x > ISLAND_END) {
			newPos.x = ISLAND_START;
		}
		obj.transform.position = newPos;
	}
}
