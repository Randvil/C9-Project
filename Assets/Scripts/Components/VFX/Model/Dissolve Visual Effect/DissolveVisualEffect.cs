using System.Collections;
using UnityEngine.VFX;
using UnityEngine;
using UnityEngine.Events;

public class DissolveVisualEffect : MonoBehaviour 
{
    [SerializeField]
    private SkinnedMeshRenderer skinnedMesh;
    [SerializeField]
    private VisualEffect dissolveGraph;
    private Material[] skinnedMaterials;

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    public void Start()
    {
        if(skinnedMesh != null)
        {
            skinnedMaterials = skinnedMesh.materials;
        }
    }

    public void ApplyDissolve()
    {
        dissolveGraph.Play();
        StartCoroutine(DissolveCoroutine());
    }

    public IEnumerator DissolveCoroutine()
    {

        if (skinnedMaterials.Length > 0)
        {
            float counter = 0;

            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
