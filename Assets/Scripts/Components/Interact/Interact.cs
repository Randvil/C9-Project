using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour, IInteract
{
    [SerializeField]
    private float interactRadius;
    public float InteractRadius { get => interactRadius; }

    private ITeam team;
    public GameObject CheckInteractiveObjectsNear()
    {
        Collider2D[] objectsNear = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        if (objectsNear.Length == 0)
            return null;

        foreach (Collider2D obj in objectsNear)
        {
            obj.TryGetComponent(out team);
            if (!(team != null && team.Team == eTeam.Player))
            {
                if (obj.gameObject.GetComponent<IInteractive>() != null)
                {
                    return obj.gameObject;
                }
            }
        }
        return null;
    }
}
