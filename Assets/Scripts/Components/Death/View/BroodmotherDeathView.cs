using System.Collections;
using UnityEngine;

public class BroodmotherDeathView
{
    private MonoBehaviour owner;
    private EnemyVisualEffect enemyVisualEffect;

    public BroodmotherDeathView(MonoBehaviour owner, IDeathManager deathManager, EnemyVisualEffect enemyVisualEffect)
    {
        this.owner = owner;
        this.enemyVisualEffect = enemyVisualEffect;

        deathManager.DeathEvent.AddListener(StartDeathView);
    }

    public void StartDeathView()
    {
        owner.StartCoroutine(DeathViewCoroutine());
    }

    public IEnumerator DeathViewCoroutine()
    {
        owner.GetComponent<Broodmother>().DeactivateBroodmotherBehaviour();
        yield return new WaitForSeconds(3.3f);
        enemyVisualEffect.ApplyDissolve();
        owner.GetComponent<Broodmother>().DestroyBroodmotherObject();
    }
}
