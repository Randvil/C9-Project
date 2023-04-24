using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaticUICreator : Creator
{
    public PanelManager PanelManager { get => newGameObject.GetComponent<PanelManager>(); }

    public UIDocument UIDocument { get => newGameObject.GetComponentInChildren<UIDocument>(); }
}
