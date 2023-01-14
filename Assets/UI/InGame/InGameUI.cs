using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public PanelManager panelManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;

            panelManager.CurrentPanel = panelManager.docs[1].rootVisualElement;            
        }
    }
}
