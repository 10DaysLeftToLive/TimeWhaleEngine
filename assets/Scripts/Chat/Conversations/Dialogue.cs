using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialogue {
	public int _npc;
	private string _textToSay;
	public string _TextToSay {
		get {return _textToSay;}
	}
	
	public Dialogue(int npc, string textToSay) {
		_npc = npc;
		_textToSay = textToSay;
	}
}
