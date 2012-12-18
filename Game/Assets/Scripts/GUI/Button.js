#pragma strict

public static function Create(area : Rect, text : String, event : Function){
	if (GUI.Button(area, text)){
		event();
	}
}