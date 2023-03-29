using System.Collections;
using UnityEngine;

public class Interact : IInteract
{
    private GameObject character;
    private InteractData interactData;

    private IInteractive interactive;
    private Coroutine searchCoroutine;

    public bool IsInteracting => interactive.IsInteracting;
    public bool CanInteract => interactive != null;

    public Interact(GameObject character, InteractData interactData)
    {
        this.character = character;
        this.interactData = interactData;

        searchCoroutine = Coroutines.StartCoroutine(SearchInteractiveObject());
    }

    private IEnumerator SearchInteractiveObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(interactData.searchPeriod);

            if (interactive != null)
            {
                interactive.HideTooltip();
                interactive = null;
            }

            Collider2D[] colliders = Physics2D.OverlapPointAll(character.transform.position);
            if (colliders.Length == 0)
            {
                continue;
            }

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out interactive) == true)
                {
                    interactive.ShowTooltip();
                    break;
                }
            }
        }
    }

    public void StartInteraction()
    {
        interactive.StartInteraction();

        if (searchCoroutine != null)
        {
            Coroutines.StopCoroutine(ref searchCoroutine);
        }
    }

    public void BreakInteraction()
    {
        interactive.StopInteraction();

        if (searchCoroutine == null)
        {
            searchCoroutine = Coroutines.StartCoroutine(SearchInteractiveObject());
        }
    }

    public void NextStep()
    {
        interactive.NextStep();
    }
}
