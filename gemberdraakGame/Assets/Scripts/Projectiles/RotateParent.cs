using UnityEngine;
using System.Collections;

public class RotateParent : MonoBehaviour {

	Vector3 axis;

	void Awake(){
		axis = GetRandomAxis ();
		transform.Rotate (axis);
	}


	Vector3 GetRandomAxis(){
		Debug.Log (new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360)));
		return new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
	}
}
