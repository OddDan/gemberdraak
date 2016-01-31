using UnityEngine;
using System.Collections;

public class GoreExploder : MonoBehaviour
{
	private Rigidbody[] children;

	void Awake ()
	{
		children = GetComponentsInChildren<Rigidbody> ();


	}

	void Start ()
	{
		foreach (Rigidbody rigidbody in children)
		{
			//rigidbody.transform.position += Random.onUnitSphere * 0.2f;
			//rigidbody.AddExplosionForce (1000f,  transform.position, 1f);
			rigidbody.AddForce(Random.onUnitSphere * 100);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
