using UnityEngine;
using System.Collections;

public class npcClass : MonoBehaviour {
	
	public string npcName;
	public TextMesh chat;
	public GameObject img;
	public GameObject player;
	
	private int npcDisposition;
	private string message;
	private string outOfRangeMessage = "!";
	private bool imgcreation = false;
	private float distance;
	private Vector3 playerPos;
	private Vector3 npcPos;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = player.transform.position;
		npcPos = this.transform.position;
		distance = Mathf.Abs(playerPos.x - npcPos.x);
		if (distance < 5){
			chat.text = message;
		}else {
			chat.text = outOfRangeMessage;
		}
		//print(playerPos.x);
		// TODO change to proximity with player char
		/*if(Input.GetKey("s")){
			DisplayImage();
			chat.text = message;
		}else if(Input.GetKey("c")){
			chat.text = message;
		}else {
			chat.text = outOfRangeMessage;	
		}
		*/
		
	}
	
	public void UpdateText(string message){
		this.message = message;
	}
	
	public void UpdateOutOfRange(string message){
		outOfRangeMessage = message;
	}
	
	public void DisplayImage(){
		if (imgcreation == false){
		imgcreation = true;
		GameObject newImg = (GameObject)Instantiate(img,this.transform.position,this.transform.rotation);
		//yield return new WaitForSeconds(5);
		Delay(5);
		DestroyObject(newImg);
		}
	}
	
	public IEnumerator Delay(int delay)
	{
		        yield return new WaitForSeconds( delay );
	}

	
	#region disposition
	public void SetDisposition(int disp) {
		npcDisposition = disp;
	}
	
	public int GetDisposition(){
		return npcDisposition;	
	}
	
	public int UpdateDisposition(int disp) {
		npcDisposition += disp;
		return npcDisposition;
	}
	#endregion
}
