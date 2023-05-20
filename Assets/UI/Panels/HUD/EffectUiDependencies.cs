using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectUiDependencies : MonoBehaviour
{
    public IEffectManager EffectManager { get; set; }

    private VisualElement effectContainer;

    private void Awake()
    {
        effectContainer = GetComponentInParent<PanelManager>().panels[0].Q<VisualElement>("effects");
    }

    private void Start()
    {
        EffectManager.EffectEvent.AddListener(EffectStatusUpdate);
    }

    private void EffectStatusUpdate(eEffectType type, eEffectStatus status)
    {
        if (type == eEffectType.Damage)
            return;

        switch (status)
        {
            case eEffectStatus.Added:
                EffectAdded(type);
                break;

            case eEffectStatus.Removed:
                break;

            case eEffectStatus.Cleared:
                EffectCleared(type);
                break;
        }
    }

    private void EffectAdded(eEffectType type)
    {

    }

    private void EffectCleared(eEffectType type)
    {

    }
}
