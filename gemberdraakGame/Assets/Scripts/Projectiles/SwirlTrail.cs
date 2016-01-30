using UnityEngine;
using System.Collections;

public class SwirlTrail : MonoBehaviour {

	Transform target;
	Vector3 axis;
	float angle = 0;

	void Awake(){
		target = transform.parent;
	}

	void Update(){
		transform.RotateAround (target.position, target.up, 360*Time.deltaTime);
	}
}
