#pragma strict

private var quit : Function = Quit;
public var background : Texture;

function Start () {
	
}

function OnGUI(){
	GUI.DrawTexture(ScreenRectangle.NewRect(0,0,1,1),background); 
}

function Update () {

}

function Quit(){
	Debug.Log("Quitting");
	Application.Quit();
}