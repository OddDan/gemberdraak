using UnityEngine;
using System.Collections;

public class GoreExploder : MonoBehaviour
{
	private Rigidbody[] children;

	void Awake ()
	{
		children = GetComponentsInChildren<Rigidbody> ();

		foreach (Rigidbody rigidbody in children)
		{
			//rigidbody.transform.position += Random.onUnitSphere * 0.05f;
			rigidbody.transform.rotation = Random.rotation;

			//rigidbody.AddExplosionForce (1000f,  transform.position, 1f);
			rigidbody.AddForce(Random.onUnitSphere * 100);
		}

		Destroy (this);
	}
}
