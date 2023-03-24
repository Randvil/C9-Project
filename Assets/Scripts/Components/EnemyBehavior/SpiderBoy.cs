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
    private float attackRadius;

    [SerializeField]
    private LayerMask enemyLayerMask;

    private float positionX;
    private GameObject player;
    private bool playerInRadius = false;

    public UnityEvent<eDirection> DirectionalMoveEvent { get; } = new();
    public UnityEvent<eDirection> TurnEvent { get; } = new();
    public UnityEvent<Vector2> FreeMoveEvent { get; } = new();
    public UnityEvent StopEvent { get; } = new();
    public UnityEvent<Vector2> JumpEvent { get; } = new();
    public UnityEvent AttackEvent { get; } = new();
    public UnityEvent<eAbilityType> AbilityEvent { get; } = new();

    public UnityEvent SpawnEvent;

    private new BoxCollider2D collider;
    private ITurning turning;
    private IDamageHandler damageHandler;
    private ITeam team;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        turning = GetComponent<ITurning>();
        damageHandler = GetComponent<IDamageHandler>();

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
    }

    public void Update()
    {
        TurnToPlayer();
        Act();
    }
    public void Activate()
    {

    }

    public void Deactivate()
    {

    }

    public void SpawnSpiders()
    {
        SpawnEvent.Invoke();
        StartCoroutine(SpawnSpiderGroup());
        
    }

    public void Act()
    {
        if (playerInRadius == false) 
            CancelInvoke("SpawnSpiders");
        if (playerInRadius && !IsInvoking("SpawnSpiders"))
            InvokeRepeating("SpawnSpiders", 0.1f, SpawnGroupDelay);
    }

    public IEnumerator SpawnSpiderGroup()
    {
        positionX = transform.position.x;
        
        if (turning.Direction == eDirection.Left) 
            positionX += 1;
        else positionX -= 1;

        for (int i = 0; i < SpiderGroup; i++)
        {
            yield return new WaitForSeconds(SpawnSpiderDelay);
            Instantiate(SpiderPrefab, new Vector3(positionX, transform.position.y + 0.1f, 0), Quaternion.identity);
        }

    }
    private void TurnToPlayer()
    {
        if (PlayerPosX() != 0f)
        {
            if (transform.position.x > PlayerPosX() && turning.Direction != eDirection.Right)
                ChangeDirection();
            if (transform.position.x < PlayerPosX() && turning.Direction != eDirection.Left)
                ChangeDirection();
        }        
    }

    private void ChangeDirection()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                TurnEvent.Invoke(eDirection.Left);
                break;

            case eDirection.Left:
                TurnEvent.Invoke(eDirection.Right);
                break;
        }
    }

    //also checks if player in attackRadius
    private float PlayerPosX()
    {
        Collider2D[] objectsNear = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        if (objectsNear.Length == 0)
        {
            playerInRadius = false;
            return 0f;
        }
            
        foreach (Collider2D obj in objectsNear)
        {
            obj.TryGetComponent(out team);
            if (team != null && team.Team == eTeam.Player)
            {
                playerInRadius = true;
                return obj.transform.position.x;
            }
        }
        playerInRadius = false;
        return 0f;
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
