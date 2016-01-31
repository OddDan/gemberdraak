using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
	private static Material material;

	private bool casting = false;
	private float time = 0f;

	void Awake ()
	{
		material = GetComponent<Renderer> ().material;
		material.SetFloat ("_Theta", 70f);

		//	Disable the renderer to make lightning bolt invisible when not in use.
		GetComponent<Renderer> ().enabled = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			CastLightning (Vector3.zero);

		if (casting)
		{
			if (time < 1f)
			{
				time += Time.deltaTime * 3f;
			}
			else
			{
				GetComponent<Renderer> ().enabled = false;
				casting = false;
			}

			material.SetFloat ("_Theta", (1 - time) * 70);
		}
	}

	public void CastLightning (Vector3 position)
	{
		GetComponent<Renderer> ().enabled = true;
		transform.position = position;

		casting = true;
		time = 0f;

		material.SetFloat ("_Theta", 70);
	}
}
