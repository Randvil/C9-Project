using System.Collections.Generic;

public class PlayerCreator : Creator
{
    public Player PlayerComponent { get => newGameObject.GetComponent<Player>(); }

    public override void LoadDataToObject(GameData data)
    {
        newGameObject.transform.position = data.CurrentGameData.position;
        newGameObject.GetComponent<Player>().HealthManager.ChangeCurrentHealth(-(PlayerComponent.HealthManager.Health.currentHealth - data.CurrentGameData.playerHealth));
        newGameObject.GetComponent<Player>().EnergyManager.ChangeCurrentEnergy(data.CurrentGameData.playerEnergy);
        foreach(AbilityPair ability in data.CurrentGameData.learnedAbilities)
        {
            newGameObject.GetComponent<Player>().AbilityManager.LearnAbility(ability.abilityType);
        }
    }

}
