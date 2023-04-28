using System.Collections;
using UnityEngine;

public class Interact : IInteract
{
    private MonoBehaviour owner;
    private GameObject character;

    private float searchPeriod = 0.1f;

    private IInteractive interactive;
    private Coroutine searchCoroutine;

    public bool IsInteracting => interactive.IsInteracting;
    public bool CanInteract => interactive != null;

    public Interact(MonoBehaviour owner, GameObject character, InteractData interactData)
    {
        this.owner = owner;
        this.character = character;

        searchPeriod = interactData.searchPeriod;

        searchCoroutine = owner.StartCoroutine(SearchInteractiveObject());
    }

    private IEnumerator SearchInteractiveObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(searchPeriod);

            if (character == null)
            {
                owner.StopCoroutine(searchCoroutine);
                break;
            }

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
            owner.StopCoroutine(searchCoroutine);
        }
    }

    public void BreakInteraction()
    {
        interactive.StopInteraction();

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
