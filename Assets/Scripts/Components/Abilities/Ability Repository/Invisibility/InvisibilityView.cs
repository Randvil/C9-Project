using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityView
{
    private MonoBehaviour owner;
    private SkinnedMeshRenderer[] skinnedMeshes;

    private float fadeTime;
    private float appearanceTime;

    private Coroutine fadeCoroutine;
    private Coroutine appearCoroutine;

    public InvisibilityView(MonoBehaviour owner, Invisibility invisibility, SkinnedMeshRenderer[] skinnedMeshes)
    {
        this.owner = owner;
        this.skinnedMeshes = skinnedMeshes;

        fadeTime = invisibility.FadeTime;
        appearanceTime = invisibility.AppearanceTime;

        invisibility.StartFadeEvent.AddListener(OnStartInvisibility);
        invisibility.BreakInvisibilityEvent.AddListener(OnBrekInvisibility);
    }

    private void OnStartInvisibility()
    {
        if (appearCoroutine != null)
        {
            owner.StopCoroutine(appearCoroutine);
            appearCoroutine = null;
        }

        fadeCoroutine = owner.StartCoroutine(ChangeTransparency(-(Time.fixedDeltaTime / fadeTime)));
    }

    private void OnBrekInvisibility()
    {
        if (fadeCoroutine != null)
        {
            owner.StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        appearCoroutine = owner.StartCoroutine(ChangeTransparency(Time.fixedDeltaTime / appearanceTime));
    }

    private IEnumerator ChangeTransparency(float deltaAlpha)
    {
        if (skinnedMeshes.Length == 0)
        {
            Debug.LogError("Can't change transparency because there is no meshes");
            yield break;
        }

        Material firstMaterial = skinnedMeshes[0].materials[0];

        while (ChangeAlphaCondition(deltaAlpha, firstMaterial))
        {
            for (int i = 0; i < skinnedMeshes.Length; i++)
            {
                foreach (Material material in skinnedMeshes[i].materials)
                {
                    Color newColor = material.color;
                    newColor.a += deltaAlpha;
                    material.color = newColor;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        bool ChangeAlphaCondition(float deltaAlpha, Material firstMaterial)
        {
            if (deltaAlpha < 0)
            {
                return firstMaterial.color.a > -deltaAlpha;
            }
            else
            {
                return firstMaterial.color.a < 1f - deltaAlpha;
            }            
        }
    }
}
