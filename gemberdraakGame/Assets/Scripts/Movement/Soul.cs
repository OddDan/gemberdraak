using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour {

	public Vector3 target;
	public int ID;
	private Vector3 velocity;
	private bool isInFocus = true;

	// Use this for initialization
	void Start () {
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 d = target - transform.position;

		if(d.magnitude > 1){
			velocity = d * 0.05f;
		}else if(isInFocus){
			//GameManager._GM.players[ID-1].transform.position = target * 0.8f;
			GameManager._GM.players[ID-1].GetComponent<MovementController> ().Respawn();
			isInFocus = false;
		}
		
		transform.position += velocity;
	}
}
