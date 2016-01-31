using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {

	public GameObject highlightPrefab;
	private GameObject highlight; 

	// Use this for initialization
	void Start () {
		highlight = Instantiate(highlightPrefab, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity) as GameObject;
		Debug.Log(highlight);
	}
	
	// Update is called once per frame
	void Update () {
		highlight.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
	}
}
