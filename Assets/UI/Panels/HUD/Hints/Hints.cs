using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hints : MonoBehaviour
{
	private const string hiddenClass = "hidden-hint";

	public float HintLifeTime { get; private set; } = 4f;

	private VisualElement hintContainer;

	private readonly Dictionary<string, Label> hints = new();
	
	//[SerializeField] private List<InteractiveObject> interactiveObjects = new();

	private void Awake()
	{
		//interactiveObjects.ForEach
		//	(obj => obj.CanInteract.AddListener(CanInteractHint));

		hintContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("hints");

		hintContainer.Query<Label>().ForEach(label =>
		{
			label.AddToClassList(hiddenClass);
			hints.Add(label.name, label);
		});
	}

	private void CanInteractHint()
	{
		Label hint = hints["ladder"];
		if (!hint.ClassListContains(hiddenClass))
			return;

		hint.RemoveFromClassList(hiddenClass);

		StartCoroutine(HideHint(hint));
	}

	private IEnumerator<object> HideHint(Label target)
	{
		yield return new WaitForSecondsRealtime(HintLifeTime);

		target.AddToClassList(hiddenClass);
	}
}
