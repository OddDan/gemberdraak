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

		if(d.magnitude > 0.1){
			velocity = d * 0.02f;
		}else if(isInFocus){
			Camera.main.GetComponent<CameraZoom>().SetFocus(ID-1, GameManager._GM.players[ID-1]);
			isInFocus = false;
		}
		
		transform.position += velocity;
	}
}
