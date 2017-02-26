using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour {
    public Material EffectMaterial;
    public Animator animator;
    [Range(-1,1)]
    public float mag = 0;
    [Range(-1, 1)]
    public float saturation = 1;

    public AnimationCurve timeCurve;
    public void startWarp()
    {
        animator.SetTrigger("warp");

    }

void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        EffectMaterial.SetFloat("_Tween", mag);
        EffectMaterial.SetFloat("_saturation", saturation);
        Graphics.Blit(src, dst, EffectMaterial);
    }
	
}
