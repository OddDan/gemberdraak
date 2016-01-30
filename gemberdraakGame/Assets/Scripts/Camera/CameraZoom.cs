using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public Transform[] points;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		CalculateCenteroid ();

	}

	void CalculateCenteroid (){
		Vector3 total = Vector3.zero;
		foreach (Transform point in points) {
			total += point.position;
		}

		total = total / points.Length;
		CalculateZoom (total);
	}

	void CalculateZoom(Vector3 center){
		float offset = 0;

		foreach (Transform point in points) {
			if (Vector3.Distance (point.position, center) > offset) {
				offset = Vector3.Distance (point.position, center);	
			}
		}

		Vector3 direction = new Vector3 (0, 1, -1);
		offset = (((offset * 1.4f) + 10f) < 20) ? 20 : ((offset * 1.4f) + 10f);

		transform.position = Vector3.MoveTowards(transform.position,center + (direction * offset),1f);
	}
}
