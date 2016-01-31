using UnityEngine;
using System.Collections;

public class SelectPlayer : MonoBehaviour {

	public bool ready = false;
	public GameObject child;
	public int ID = 0;

	// Update is called once per frame
	void Update () { 
		if (Input.GetButtonDown ("CTRL" + ID +"_jump")) {
			ready = !ready;
		}

		if (ready) {
			child.SetActive (true);
		} else {
			child.SetActive (false);
		}
	}
}
