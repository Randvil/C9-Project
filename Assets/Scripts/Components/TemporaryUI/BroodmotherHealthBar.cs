using UnityEngine;
using UnityEngine.UIElements;

class BroodmotherHealthBar : HealthBarUITK
{
    public BroodmotherHealthBar(UIDocument doc, IHealthManager healthManager, IDeathManager deathManager, string name)
        : base(doc, healthManager, deathManager)
    {
        root = root.Q<VisualElement>("bossStats");
        healthBar = root.Q<ProgressBar>(name);
    }
}