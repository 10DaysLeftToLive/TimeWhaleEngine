using UnityEngine;
using System.Collections;

public class npcManager : MonoBehaviour {
	
	public GameObject destination;
	public PathFinding pathFinding;
	
	private Component[] npcs;
	private GameObject finish;
	private bool findingPath = false;
	private int zCameraOffset = 10;
	
	// Use this for initialization
	void Start () {
		npcs = GetComponentsInChildren<npcClass>();
		foreach(npcClass npc in npcs){
			if (npc.npcName == "Susan"){
				npc.SetDisposition(4);
			}else if (npc.npcName == "Charlie"){
				npc.SetDisposition(1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pathFinding != null && findingPath){
			pathFinding.Update();
			
			if (pathFinding.foundPath == 2){
				foreach(npcClass npc in npcs){
					Debug.Log("Manager recieved path");
					npc.NpcMove(pathFinding.FoundPath());
				}
				Destroy(finish);
				pathFinding = null;
				findingPath = false;
			}else if (pathFinding.foundPath == 1){
				Debug.Log("no path found");
				Destroy(finish);
				pathFinding = null;
				findingPath = false;
			}
		}
		foreach(npcClass npc in npcs){
			if(Input.GetKey("b")){
				npc.UpdateText("" + npc.GetDisposition());
			}else{
				npc.UpdateText(npc.npcName);
			}
			
			if (Input.GetKeyDown("m")){
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pathFinding = null;
				if (finish != null) Destroy(finish);
				int mask = (1 << 9);
				RaycastHit hit;
				if (Physics.Raycast(new Vector3(pos.x, pos.y, Camera.main.transform.position.z+zCameraOffset+.5f), Vector3.down, out hit,mask)) {
					Vector3 hitPos = hit.transform.position;
					finish = (GameObject)Instantiate(destination,new Vector3(pos.x, hitPos.y +1.5f, Camera.main.transform.position.z+zCameraOffset),this.transform.rotation);
					pathFinding = new PathFinding();
					pathFinding.StartPath(npc.GetPos() ,new Vector3(pos.x, hitPos.y -.5f, .5f), .5f);
					findingPath = true;
				}
			}
		}	
	}
}
