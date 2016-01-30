using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager _GM;
	public GameObject playerPrefab;
	public float respawnTime = 3;

	GameObject[] players;

	void Awake(){
		if (_GM == null) {
			DontDestroyOnLoad (gameObject);
			_GM = this;
		}
		else if(_GM != this){
			Destroy(gameObject);
		}

		players = GameObject.FindGameObjectsWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		foreach (GameObject player  in players) {
			player.GetComponent<MovementController> ().Mutate (0);
		}
		players[Random.Range (0, players.Length)].gameObject.GetComponent<MovementController>().Mutate(1);
	}
}
