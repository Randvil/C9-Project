using System.Collections;
using UnityEngine.VFX;
using UnityEngine;
using System.Collections.Generic;

public class EnemyVisualEffect : MonoBehaviour 
{
    [SerializeField]
    private SkinnedMeshRenderer[] skinnedMeshes;
    [SerializeField]
    private VisualEffect dissolveGraph;

    private List<Material[]> skinnedMaterials = new();

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    public void Awake()
    {
        if(skinnedMeshes != null)
        {
            foreach (SkinnedMeshRenderer mesh in skinnedMeshes)
            {
                skinnedMaterials.Add(mesh.materials);
            }
        }

    }

    public void ApplyDissolve()
    {
        dissolveGraph.Play();
        StartCoroutine(DissolveCoroutine());
    }

    public IEnumerator DissolveCoroutine()
    {
        foreach(Material[] skinnedMat in skinnedMaterials)
        {
            if (skinnedMat.Length > 0)
            {
                float counter = 0;

                while (skinnedMat[0].GetFloat("_DissolveAmount") < 1)
                {
                    foreach (Material mat in skinnedMat)
                    {
                        counter += dissolveRate;
                        mat.SetFloat("_DissolveAmount", counter);
                    }
                    
                    yield return new WaitForSeconds(refreshRate);
                }

            }
        }
    }

    public void ApplyHurtEffect(DamageInfo damageInfo)
    {
        StartCoroutine(HurtEffectCoroutine());
    }

    public IEnumerator HurtEffectCoroutine()
    {
        skinnedMaterials[0][0].SetFloat("_IsHurt", 1);
        yield return new WaitForSeconds(0.1f);
        skinnedMaterials[0][0].SetFloat("_IsHurt", 0);
    }
}
