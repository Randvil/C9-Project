using UnityEngine.VFX;
using UnityEngine;

public abstract class VisualEffectView : MonoBehaviour
{
    [SerializeField]
    private VisualEffect graph;
    public VisualEffect Graph { get => graph; set => graph = value; }
}
