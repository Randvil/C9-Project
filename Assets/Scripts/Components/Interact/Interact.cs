using System.Collections;
using UnityEngine;

public class Interact : IInteract
{
    private MonoBehaviour owner;
    private GameObject character;

    private float searchPeriod = 0.1f;
    private LayerMask interactiveObjectLayer;

    private IInteractive interactive;
    private Coroutine searchCoroutine;

    public bool IsInteracting => interactive.IsInteracting;
    public bool CanInteract => interactive != null;

    public Interact(MonoBehaviour owner, GameObject character, InteractData interactData)
    {
        this.owner = owner;
        this.character = character;

        searchPeriod = interactData.searchPeriod;
        interactiveObjectLayer = interactData.interactiveObjectLayer;

        searchCoroutine = owner.StartCoroutine(SearchInteractiveObject());
    }

    private IEnumerator SearchInteractiveObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(searchPeriod);

            if (interactive != null)
            {
                interactive.HideTooltip();
                interactive = null;
            }

            Collider2D collider = Physics2D.OverlapPoint(character.transform.position, interactiveObjectLayer);
            if (collider == null)
            {
                continue;
            }

            if (collider.TryGetComponent(out interactive) == true)
            {
                interactive.ShowTooltip();
            }
        }
    }

    public void StartInteraction()
    {
        if (interactive != null)
        {
            interactive.StartInteraction(character);
        }        

        if (searchCoroutine != null)
        {
            owner.StopCoroutine(searchCoroutine);
            searchCoroutine = null;
        }
    }

    public void BreakInteraction()
    {
        if (interactive != null)
        {
            interactive.StopInteraction();
            interactive = null;
        }        

        if (searchCoroutine == null)
        {
            searchCoroutine = owner.StartCoroutine(SearchInteractiveObject());
        }
    }

    public void NextStep()
    {
        interactive.NextStep();
    }
}
