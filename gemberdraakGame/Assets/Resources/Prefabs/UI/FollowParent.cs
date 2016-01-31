using UnityEngine;
using System.Collections;

public class FollowParent : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.parent.position.x, 0.4f, transform.parent.position.z);
	}
}
