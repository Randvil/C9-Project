using DG.Tweening;
using NS.RomanLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectUiDependencies : MonoBehaviour
{
    public IEffectManager EffectManager { get; set; }

    private VisualElement effectContainer;
    private Dictionary<eEffectType, VisualElement> effectViews = new(4);

    private List<eEffectType> effectTypeOrder = new(4);

    private void Awake()
    {
        effectContainer = GetComponentInParent<PanelManager>().panels[0].Q<VisualElement>("effects");
        effectViews.Add(eEffectType.DoT, effectContainer.Q<VisualElement>("DoT"));
        effectViews.Add(eEffectType.Slow, effectContainer.Q<VisualElement>("Slow"));
        effectViews.Add(eEffectType.Root, effectContainer.Q<VisualElement>("Root"));
        effectViews.Add(eEffectType.Stun, effectContainer.Q<VisualElement>("Stun"));

        foreach (VisualElement effectView in effectViews.Values)
            effectView.AddToClassList("hidden-effect");
    }

    private void Start()
    {
        EffectManager.EffectEvent.AddListener(EffectStatusUpdate);
    }

    private void EffectStatusUpdate(eEffectType type, eEffectStatus status)
    {
        if (type == eEffectType.Damage || type == eEffectType.Slow)
            return;
        Debug.Log(type + " " + status);

        switch (status)
        {
            case eEffectStatus.Added:
                if (effectTypeOrder.Contains(type))
                    UpdateEffectDuration(effectViews[type], type);
                else
                    EffectAdded(type);
                break;

            case eEffectStatus.Removed:
                if (type == eEffectType.DoT && EffectManager.GetMaxEffectDuration(type) == 0f)
                    EffectCleared(type);
                break;

            case eEffectStatus.Cleared:
                EffectCleared(type);
                break;
        }
    }

    private void UpdateEffectDuration(VisualElement effectView, eEffectType type)
    {
        RadialFill radialFill = effectView.Q<RadialFill>();
        radialFill.value = 1f;
        DOTween.To(x => radialFill.value = x, 1f, 0f, EffectManager.GetMaxEffectDuration(type)).SetEase(Ease.Linear);
    }

    private void EffectAdded(eEffectType type)
    {
        VisualElement effectView = effectViews[type];
        effectView.AddToClassList("effect" + (effectTypeOrder.Count + 1));

        effectTypeOrder.Add(type);

        effectView.RemoveFromClassList("hidden-effect");

        UpdateEffectDuration(effectView, type);
    }

    private void EffectCleared(eEffectType type)
    {
        RemoveEffectAt(type, effectTypeOrder.IndexOf(type));
    }

    private void RemoveEffectAt(eEffectType type, int pos)
    {
        VisualElement removedEffect = effectViews[type];

        removedEffect.AddToClassList("hidden-effect");

        if (effectTypeOrder.Count > pos + 1)
        {
            for (int i = pos; i < effectTypeOrder.Count - 1; i++)
            {
                VisualElement nextEffect = effectViews[effectTypeOrder[i + 1]];
                nextEffect.AddToClassList("effect" + (i + 1));
                nextEffect.RemoveFromClassList("effect" + (i + 2));
            }
        }
        
        removedEffect.RemoveFromClassList("effect" + pos);

        effectTypeOrder.RemoveAt(pos);
    }
}
