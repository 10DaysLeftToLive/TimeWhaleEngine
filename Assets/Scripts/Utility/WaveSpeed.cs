using UnityEngine;
using System.Collections;

public class WaveSpeed : MonoBehaviour {

	private float startFps = 15f;
	
	void Start () {
		Random.seed = (int)Time.timeSinceLevelLoad;
		SmoothMoves.BoneAnimation[] animations = gameObject.GetComponentsInChildren<SmoothMoves.BoneAnimation>();
		for (int i = 0; i < animations.Length; ++i) {
			foreach (SmoothMoves.AnimationStateSM wave in animations[i]) {
				wave.fps = startFps + Random.Range(-1f, 1f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
