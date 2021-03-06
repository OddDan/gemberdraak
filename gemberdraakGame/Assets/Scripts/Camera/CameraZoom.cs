using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public Transform[] points;

	public float minZoom;
	public float staticOffset;
	public float scalar;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		CalculateCenteroid ();
	}

	public void SetFocus (int i, GameObject go){
		points[i] = go.transform;
	}

	public void RemoveFocus (int i){
		points[i] = GameManager._GM.transform;
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

		Vector3 direction = new Vector3 (0, 1, -0.9f);
		offset = (((offset * scalar) + staticOffset) < minZoom) ? minZoom : ((offset * scalar) + staticOffset);

		transform.position = Vector3.MoveTowards(transform.position ,center + (direction * offset), 0.5f);
	}
}
