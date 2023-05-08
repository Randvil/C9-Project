using System.Linq;

public class PlayerCreator : Creator
{
    public Player PlayerComponent { get => newGameObject.GetComponent<Player>(); }

    public override void LoadDataToObject(GameData data)
    {
        newGameObject.transform.position = data.CurrentGameData.position;
        newGameObject.GetComponent<Player>().HealthManager.SetCurrentHealth(data.CurrentGameData.playerHealth);
        newGameObject.GetComponent<Player>().EnergyManager.ChangeCurrentEnergy(data.CurrentGameData.playerEnergy);
        foreach (AbilityPair ability in data.CurrentGameData.learnedAbilities)
        {
            IAbility learnedAbility = newGameObject.GetComponent<Player>().AbilityManager.Abilities.FirstOrDefault(x => x.Value.Type == ability.abilityType).Value;
            newGameObject.GetComponent<Player>().AbilityManager.LearnAbility(ability.abilityType, ability.pos);
        }
    }
}