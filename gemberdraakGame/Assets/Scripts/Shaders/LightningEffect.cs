using UnityEngine;
using System.Collections;

public class LightningEffect : MonoBehaviour
{
	public Shader shader;
	public static bool casting = false;

	private Material mat;

	void Awake ()
	{
		mat = new Material (shader);
	}

	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		if (casting)
		{
			Graphics.Blit (src, dst, mat);
			//casting = false;
		}
	}
}
