using UnityEngine;

[CreateAssetMenu(fileName = "New Horror Manager Data", menuName = "Component Data/Model/New Horror Manager Data", order = 125)]
public class HorrorManagerData : ScriptableObject
{
    public Horror initialHorror = new()
    {
        maxHorror = 100f,
        currentHorror = 0f
    };
}
