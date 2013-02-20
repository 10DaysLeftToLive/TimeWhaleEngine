using UnityEngine;
using System.Collections;

public class Chat : MonoBehaviour {
	GameObject obj;
	//GameObject sister;
	//public Texture test;
	Texture btn1, btn2;
	Vector3 pos, screenPos;
	Vector2 size;
	string msg;
	int charPerLine = 20;
	Rect rect;
	bool active, button;
	Vector2 offset;
	
	// Use this for initialization
	void Start () {
		/*sister = GameObject.Find("Sister");
		CreateChatBox(sister, "I'm a little teapot short and stout, here is my handle and here is my spout.");
		CreateChatButtons(test, test);*/
		
		size.x = 150;
		size.y = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		if (active){
			screenPos = Camera.main.WorldToScreenPoint (obj.transform.position);
			rect = new Rect(screenPos.x + offset.x/2, Screen.height - screenPos.y - offset.y*.75f, size.x, size.y);
			GUI.Box (rect, msg);
			if (button){
				rect = new Rect(screenPos.x + offset.x/2, Screen.height - screenPos.y - offset.y*.75f+size.y, size.x/2, size.y/2);
				if (GUI.Button(rect, btn1)){
					Debug.Log("Button 1 clicked!");	
				}
				rect = new Rect(screenPos.x + offset.x/2+size.x/2, Screen.height - screenPos.y - offset.y*.75f+size.y, size.x/2, size.y/2);
				if (GUI.Button(rect, btn2)){
					Debug.Log("Button 2 clicked!");	
				}
			}
		}
	}
	
	public void CreateChatBox(GameObject obj, string text){
		this.obj = obj;
		msg = text;
		
		Vector3 minBounds = Camera.main.WorldToScreenPoint (obj.renderer.bounds.min);
		Vector3 maxBounds = Camera.main.WorldToScreenPoint (obj.renderer.bounds.max);
		offset.x = Mathf.Abs(minBounds.x - maxBounds.x);
		offset.y = Mathf.Abs(minBounds.y - maxBounds.y);
		
		if (msg.Length > charPerLine){
			int lastIndex = 0;
			
			for (int i = 1; i <= msg.Length/charPerLine; i++){
				int index = charPerLine*i;
				do {
					--index;
				}while(msg[index] != ' ');

				msg = msg.Insert(index, "\n");
			}
			
		}
		
		screenPos = Camera.main.WorldToScreenPoint (obj.transform.position);
		rect = new Rect(screenPos.x + offset.x/2, Screen.height - screenPos.y - offset.y*.75f, size.x, size.y);
		active = true;
	}
	
	public void RemoveChatBox(){
		active	= false;
		button = false;
	}
	
	public void CreateChatButtons(Texture bt1, Texture bt2){
		btn1 = bt1;
		btn2 = bt2;
		button = true;
	}
}
