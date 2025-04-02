using UnityEngine;
using UnityEngine.AI;

public class Monster : Poolable, IDamageable
{
    protected MonsterStateMachine stateMachine;
    public NavMeshAgent Agent {  get; private set; }
    public Animator Animator { get; private set; }

    public float AttackRange;
    public int id;
    public string MonsterName {  get; private set; }
    public string Description {  get; private set; }
    public Stat Health {  get; private set; }
    public int Damage {  get; private set; }
    public float MoveSpeed { get; private set; }
    public int Meat {  get; private set; }
    public int Exp {  get; private set; }
    public int Gold {  get; private set; }


    private bool isDead = false;

    public virtual void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();

    }

    public virtual void Start()
    {
        InitMonsterData();
        stateMachine = new MonsterStateMachine(this);
        stateMachine.ChangeState(stateMachine.ChasingState);
        stateMachine.Target = GameManager.Instance.player;
    }

    private void Update()
    {
        if (isDead) return;
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("DealDamage", 0f, 1f);
            MonsterDie();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CancelInvoke("DealDamage");
        }
    }
    private void DealDamage()
    {
        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.condition.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        Health.Subtract(damage);
        Transform textPosition = transform.GetChild(1);
        if (textPosition != null)
        {
            UI_Damage.Instance.DamagePool(damage, textPosition.position);
        }
        if(Health.CurValue <= 0)
        {
            MonsterDie();
        }
    }

    private void InitMonsterData()
    {
        if (!DataManager.Instance.monsterDict.TryGetValue(id, out MonsterData data))
        {
            Debug.LogError("몬스터 데이터 x");
            return;
        }

        MonsterName = data.name;
        Description = data.description;
        Health = new Stat(data.health, data.health);
        Damage = data.attack;
        MoveSpeed = data.moveSpeed;
        Agent.speed = MoveSpeed;
        Meat = data.dropMeat;
        Gold = data.dropGold;
        Exp = data.dropExp; 
    }

    [ContextMenu("Dead")]
    public void MonsterDie()
    {
        if (isDead) return;

        isDead = true;
        Animator.SetTrigger("Die");

        PoolManager.Instance.Release(this);
        //Destroy(gameObject, 1.5f);
        DropItems();

    }

    private void DropItems()
    {
        ItemDrop.Instance.DropItem(ItemDrop.Instance.meatPrefab, transform.position, Meat);
        ItemDrop.Instance.DropItem(ItemDrop.Instance.expPrefab, transform.position, Exp);
        ItemDrop.Instance.DropItem(ItemDrop.Instance.goldPrefab, transform.position, Gold);
    }

}
