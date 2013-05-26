using UnityEngine;
using System.Collections;

public class LeadPlayerToState : MoveThenDoState {
	private Player _player;
	private ShowOneOffChatAction waitYell;
	private float timeToYellAgain = 0;
	private static float NEAR_PLAYER = 6f;
	private static float TIME_TO_YELL_AGAIN = 5f;
	
	public LeadPlayerToState(Character toControl, Player player, Vector3 goal) : base(toControl, goal, new MarkTaskDone(toControl)){
		_player = player;
		if (toControl is NPC) {
			waitYell = new ShowOneOffChatAction((NPC)toControl, "Come back. I'm leading the way.");
		} else {
			Debug.LogWarning("Player trying to use LeadPlayerToThenDoState");
		}
	}
	
	public override void Update () {
		timeToYellAgain -= Time.deltaTime;
		if (Utils.InDistance(character.gameObject, _player.gameObject, NEAR_PLAYER)) {
			base.Update();
		} else {
			character.PlayAnimation(Strings.animation_stand);
			if (timeToYellAgain <= 0) {
				waitYell.Perform();
				timeToYellAgain = TIME_TO_YELL_AGAIN;
			}
		}
	}
}
