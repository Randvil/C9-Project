using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWebView
{
    private Material[] matArray;
    private SkinnedMeshRenderer mesh;

    public RemoveWebView(IEffectManager effectManager, SkinnedMeshRenderer mesh)
    {
        this.mesh = mesh;
        effectManager.EffectEvent.AddListener(RemoveWeb);
    }

    private void RemoveWeb(eEffectType type, eEffectStatus status)
    {
        if (type == eEffectType.Root && status == eEffectStatus.Cleared)
        {
            matArray = mesh.materials;
            matArray[1] = matArray[0];
            mesh.materials = matArray;
        }
    }
}
