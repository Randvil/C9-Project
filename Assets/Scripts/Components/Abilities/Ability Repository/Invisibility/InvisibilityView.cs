using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityView
{
    private MonoBehaviour owner;
    private SkinnedMeshRenderer[] skinnedMeshes;

    private float visibilityRate;
    private float refreshTime;

    public InvisibilityView(MonoBehaviour owner, Invisibility invisibility, SkinnedMeshRenderer[] skinnedMeshes, InvisibilityData data)
    {
        this.owner = owner;
        this.skinnedMeshes = skinnedMeshes;

        visibilityRate = data.visibilityRate;
        refreshTime = data.refreshRate;

        invisibility.StartFadeEvent.AddListener(OnStartFadeEvent);
        invisibility.BreakInvisibilityEvent.AddListener(OnBreakInvisibility);
    }

    private void OnBreakInvisibility()
    {
        owner.StartCoroutine(VisibilityCoroutine());
    }

    private void OnStartFadeEvent()
    {
        
    }

    public IEnumerator VisibilityCoroutine()
    {
        foreach (SkinnedMeshRenderer mesh in skinnedMeshes)
        {
            float counter = 0;
            while (mesh.material.GetFloat("_Opacity") < 1)
            {
                counter += visibilityRate;
                foreach (Material material in mesh.materials)
                {
                    material.SetFloat("_Opacity", counter);
                    
                }
                yield return new WaitForSecondsRealtime(refreshTime);
            }
            mesh.material.SetFloat("_IsVisible", 1);
        }
    }
}
