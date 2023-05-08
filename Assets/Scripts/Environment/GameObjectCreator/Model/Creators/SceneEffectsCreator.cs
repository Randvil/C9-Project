using UnityEngine.Rendering;

public class SceneEffectsCreator : Creator
{
    public Volume Volume { get => newGameObject.GetComponentInChildren<Volume>(); }
}
