using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpiderBoy : MonoBehaviour, IEnemyBehavior
{
    [SerializeField]
    private float SpawnGroupDelay;

    [SerializeField]
    private float SpawnSpiderDelay;

    [SerializeField]
    private int SpiderGroup;

    [SerializeField]
    private GameObject SpiderPrefab;

    [SerializeField]
    private float searchPlayerDistance;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private LayerMask enemyLayerMask;

    [SerializeField]
    private float senseDelay;

    [SerializeField]
    private float loseSightOfPlayerDistance;

    private float positionX;
    private GameObject player;

    public UnityEvent<eDirection> DirectionalMoveEvent { get; } = new();
    public UnityEvent<eDirection> TurnEvent { get; } = new();
    public UnityEvent<Vector2> FreeMoveEvent { get; } = new();
    public UnityEvent StopEvent { get; } = new();
    public UnityEvent<Vector2> JumpEvent { get; } = new();
    public UnityEvent AttackEvent { get; } = new();
    public UnityEvent<eAbilityType> AbilityEvent { get; } = new();

    private new BoxCollider2D collider;
    private ITurning turning;
    private IDamageHandler damageHandler;

    private void Start()
    {
        positionX = transform.position.x;
        positionX -= 1;

        collider = GetComponent<BoxCollider2D>();
        turning = GetComponent<ITurning>();
        damageHandler = GetComponent<IDamageHandler>();

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
        InvokeRepeating("SpawnSpiders", .5f, SpawnGroupDelay);
    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
        
    }

    private IEnumerator SenseCoroutine()
    {
        while (true)
        {
            SearchPlayer();

            if (player != null && Vector2.Distance(player.transform.position, transform.position) > loseSightOfPlayerDistance)
            {
                player = null;
            }

            yield return new WaitForSeconds(senseDelay);
        }
    }

    //add check if player is near
    public void SpawnSpiders()
    {
        StartCoroutine(SpawnSpiderGroup());
    }

    public IEnumerator SpawnSpiderGroup()
    {
        for (int i = 0; i < SpiderGroup; i++)
        {
            yield return new WaitForSeconds(SpawnSpiderDelay);
            Instantiate(SpiderPrefab, new Vector3(positionX, 0, 0), Quaternion.identity);
        }

    }

    private void ChangeDirection()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                DirectionalMoveEvent.Invoke(eDirection.Left);
                break;

            case eDirection.Left:
                DirectionalMoveEvent.Invoke(eDirection.Right);
                break;
        }
    }

    private bool SearchPlayer()
    {
        Vector2 viewDirection = turning.Direction == eDirection.Right ? Vector2.right : Vector2.left;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, viewDirection, searchPlayerDistance, enemyLayerMask);
        if (hitInfo.collider != null)
        {
            player = hitInfo.collider.gameObject;
            return true;
        }

        return false;
    }

    private void OnTakeDamage(Damage incomingDamage, Damage effectedDamage)
    {
        if (player != null)
            return;

        Collider2D attacker = Physics2D.OverlapCircle(transform.position, attackRadius, enemyLayerMask);
        if (attacker != null)
            player = attacker.gameObject;
    }
}
