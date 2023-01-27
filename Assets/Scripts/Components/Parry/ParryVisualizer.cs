using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject textGO;

    private IParry parry;

    private void Start()
    {
        parry = GetComponent<Parry>();
        parry.StartParryEvent.AddListener(OnStartParry);
        parry.StopParryEvent.AddListener(OnStopParry);
    }

    private void OnStartParry()
    {
        textGO.SetActive(true);
    }

    private void OnStopParry()
    {
        textGO.SetActive(false);
    }
}
