using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class readyCheck : MonoBehaviour {

	public SelectPlayer[] players;
	
	// Update is called once per frame
	void Update () {
		bool canstart = true;
		foreach (SelectPlayer player in players) {
			if (!player.ready) {
				canstart = false;
			}
		}
		if (canstart) {
			SceneManager.LoadScene (1);
		}
	}
}
