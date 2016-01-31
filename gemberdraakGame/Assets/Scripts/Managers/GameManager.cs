using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager _GM;
	public GameObject playerPrefab;
	public GameObject[] soulPrefab;
	public float respawnTime = 3;
	public float[] playerScores = {0, 0, 0, 0};

	public GameObject[] players;
	public GameObject[] soulStaches;

	void Awake(){
		if (_GM == null) {
			DontDestroyOnLoad (gameObject);
			_GM = this;
		}
		else if(_GM != this){
			Destroy(gameObject);
		}
		MutateAll ();
	}

	void OnLevelWasLoaded(int level){
		if (level == 1) {
			MutateAll ();
		}
	}

	public void ReleaseSoul(int playerID){
		GameObject soul = GameObject.Instantiate(soulPrefab[playerID-1], new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
		soul.GetComponent<Soul>().target = new Vector3(soulStaches[playerID-1].transform.position.x, soulStaches[playerID-1].transform.position.y + 1.5f*playerScores[playerID-1], soulStaches[playerID-1].transform.position.z);
		soul.GetComponent<Soul>().ID = playerID;

		Camera.main.GetComponent<CameraZoom>().SetFocus(playerID-1, soul);
	}

	public void MutateAll(){
		players [0] = GameObject.Find ("Player (1)");
		players [1] = GameObject.Find ("Player (2)");
		players [2] = GameObject.Find ("Player (3)");
		players [3] = GameObject.Find ("Player (4)");
		soulStaches [0] = GameObject.Find ("Soul Stash 1");
		soulStaches [1] = GameObject.Find ("Soul Stash 2");
		soulStaches [2] = GameObject.Find ("Soul Stash 3");
		soulStaches [3] = GameObject.Find ("Soul Stash 4");
		foreach (GameObject player  in players) {
			player.GetComponent<MovementController> ().Mutate (0);
		}
		players[Random.Range (0, players.Length)].gameObject.GetComponent<MovementController>().Mutate(1);
	}

}
