using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

	public GameObject projectile;
	public MovementController mc;

	void Awake(){
		mc = gameObject.GetComponent<MovementController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton (mc.ctrlName + "jump")) {
			
		}
	}
}
