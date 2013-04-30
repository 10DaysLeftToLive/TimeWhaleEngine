using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCConvoChance : NPCConversation {
	protected int _talkChanceInitial;
	public int TalkChanceInitial {
		get {return _talkChanceInitial;}
	}
	public int _talkChanceCurrent;
	
	public NPCConvoChance() {
		_talkChanceInitial = _talkChanceCurrent = 0;
	}
	
	public NPCConvoChance(int talkChance) {
		_talkChanceInitial = _talkChanceCurrent = talkChance;
	}
}
