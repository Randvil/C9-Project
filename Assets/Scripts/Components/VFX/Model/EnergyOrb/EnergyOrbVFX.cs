using UnityEngine.VFX;
using UnityEngine;
using System.Collections;

public class EnergyOrbVFX : MonoBehaviour
{
    [SerializeField]
    private VisualEffect energyOrbGraph;

    public void Awake()
    {
        energyOrbGraph.playRate = 20f;
        StartCoroutine(ChangeRateCoroutine());
    }

    public IEnumerator ChangeRateCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        energyOrbGraph.playRate = 1f;
    }

}
