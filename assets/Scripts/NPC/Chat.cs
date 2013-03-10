using UnityEngine;
using System.Collections;

public class Chat : MonoBehaviour {
	GameObject obj;
	//GameObject sister;
	//public Texture test;
	public Texture btn1, btn2;
	Vector3 pos, screenPos;
	Vector2 size;
	string msg;
	int charPerLine = 20;
	Rect rect;
	bool isActive;
	Vector2 offset;
	
	public delegate void ButtonClickDelegate();
	ButtonClickDelegate leftButtonClickDelegate;
	ButtonClickDelegate rightButtonClickDelegate;
	private bool showLeftButton = false;
	private bool showRightButton = false;
	
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
		if (isActive){
			screenPos = Camera.main.WorldToScreenPoint (obj.transform.position);
			rect = new Rect(screenPos.x + offset.x/2, Screen.height - screenPos.y - offset.y*.75f, size.x, size.y);
			GUI.Box (rect, msg);
			if (showLeftButton){
				rect = new Rect(screenPos.x + offset.x/2, Screen.height - screenPos.y - offset.y*.75f+size.y, size.x/2, size.y/2);
				if (GUI.Button(rect, "Talk With[Not Implemented]")){
					leftButtonClickDelegate();
				}
			}
			if (showRightButton){
				rect = new Rect(screenPos.x + offset.x/2+size.x/2, Screen.height - screenPos.y - offset.y*.75f+size.y, size.x/2, size.y/2);
				if (GUI.Button(rect, "Give Item")){
					rightButtonClickDelegate();
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
		isActive = true;
	}
	
	public void RemoveChatBox(){
		isActive	= false;
		showRightButton = false;
		showLeftButton = false;
	}
	
	public void CreateChatButtons(Texture bt1, Texture bt2){
		btn1 = bt1;
		btn2 = bt2;
	}
	
	private void SetLeftButton(ButtonClickDelegate leftButtonClick){
		leftButtonClickDelegate += leftButtonClick;
		showLeftButton = true;
	}
	
	private void SetRightButton(ButtonClickDelegate rightButtonClick){
		rightButtonClickDelegate += rightButtonClick;
		showRightButton = true;
	}
	
	// We will have the single constructor set the right button as we will only need the right for items some of the time
	public void SetButtonCallbacks(ButtonClickDelegate leftButtonClick){
		SetLeftButton(leftButtonClick);
	}
	
	public void SetButtonCallbacks(ButtonClickDelegate leftButtonClick, ButtonClickDelegate rightButtonClick){
		SetLeftButton(leftButtonClick);
		SetRightButton(rightButtonClick);
	}
}
