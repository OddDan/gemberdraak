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
	}

	// Use this for initialization
	void Start () {
		foreach (GameObject player  in players) {
			player.GetComponent<MovementController> ().Mutate (0);
		}
		players[Random.Range (0, players.Length)].gameObject.GetComponent<MovementController>().Mutate(1);
	}

	public void ReleaseSoul(int playerID){
		GameObject soul = GameObject.Instantiate(soulPrefab[playerID-1], new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
		soul.GetComponent<Soul>().target = new Vector3(soulStaches[playerID-1].transform.position.x, soulStaches[playerID-1].transform.position.y + 1.5f*playerScores[playerID-1], soulStaches[playerID-1].transform.position.z);
		soul.GetComponent<Soul>().ID = playerID;

		Camera.main.GetComponent<CameraZoom>().SetFocus(playerID-1, soul);
	}

}
