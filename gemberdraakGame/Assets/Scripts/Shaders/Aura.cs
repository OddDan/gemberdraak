using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour
{
	private Material material;

	void Awake ()
	{
		material = GetComponent<Renderer> ().material;

		material.SetFloat ("_Height", 0.1f);
	}

	void Update ()
	{
		material.SetFloat ("_Opacity", (Mathf.Sin (Time.time) + 2) / 8);

		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//		AuraBurst(1f);
		//}
	}

	public void AuraBurst (float time)
	{	
		Debug.Log("BLO");
		StopAllCoroutines ();
		StartCoroutine (AuraBurstCoroutine (time));
	}

	private IEnumerator AuraBurstCoroutine (float time)
	{
		float t = 0;

		while (t < 1)
		{
			float value = (1 - Mathf.Sin (2 * Mathf.PI * t + Mathf.PI / 2)) / 6;

			material.SetFloat ("_Opacity", Mathf.Lerp(material.GetFloat("_Opacity"), value, Time.deltaTime * 60));
			material.SetFloat ("_Height", value + 0.1f);

			t += Time.deltaTime / time;

			yield return new WaitForEndOfFrame();
		}

		material.SetFloat ("_Height", 0.1f);

		yield return null;
	}
}
