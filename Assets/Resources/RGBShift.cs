using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBShift : MonoBehaviour
{
	// Start is called before the first frame update
	public float shift;
	

	public Material rgbOffset;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		BlitRGBOffset(shift, source, destination);
	}

	private void BlitRGBOffset(float shift , RenderTexture src, RenderTexture dst)
	{
		rgbOffset.SetFloat("_Shift",shift);
		Graphics.Blit(src, dst, rgbOffset);
	}
}
