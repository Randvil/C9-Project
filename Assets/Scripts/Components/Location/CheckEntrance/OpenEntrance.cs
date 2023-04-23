using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEntrance : MonoBehaviour
{
    private ICheckEntrance checkEntrance;
    private bool isOpen;

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
            gameObject.SetActive(false);
        }
    }
}
