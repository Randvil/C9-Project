using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLocationEntry : MonoBehaviour
{
    private ICheckEntrance checkEntrance;
    private bool isOpen;
    [SerializeField]
    private GameObject locationEntry;

    public void Start()
    {
        checkEntrance = GetComponent<ICheckEntrance>();
    }

    public void Update()
    {
        isOpen = checkEntrance.EntranceOpen();

        if (isOpen)
        {
            checkEntrance.EntranceOpenEvent.Invoke();
            locationEntry.SetActive(true);
        } 
        else locationEntry.SetActive(false);
    }
}
