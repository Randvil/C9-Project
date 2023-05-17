using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Hints : MonoBehaviour
{
	private const string hiddenClass = "hidden-hint";

	private VisualElement hintContainer;

	private readonly Dictionary<string, Label> hints = new();
	
	private List<Hintable> interactiveObjects;

	private void Awake()
	{
		interactiveObjects = FindObjectsOfType<Hintable>().ToList(); //~0.0015s
		interactiveObjects.ForEach(obj =>
		{
			obj.ShowHint.AddListener(ShowHint);
			obj.HideHint.AddListener(HideHint);
		});

		hintContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("hints");
		hintContainer.Query<Label>().ForEach(label =>
		{			
			label.AddToClassList(hiddenClass);
			hints.Add(label.name, label);
		});
	}

	//public void AddHintable(Hintable hintable) => interactiveObjects.Add(hintable);

	private void ShowHint(string labelName)
	{
		Label hint = hints[labelName];
		if (!hint.ClassListContains(hiddenClass))
			return;

		hint.RemoveFromClassList(hiddenClass);
	}

	private void HideHint(string labelName)
	{
		hints[labelName].AddToClassList(hiddenClass);
	}
}
