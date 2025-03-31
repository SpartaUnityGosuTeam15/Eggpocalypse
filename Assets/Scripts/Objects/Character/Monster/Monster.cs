using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour, IDamageable
{
    protected MonsterStateMachine stateMachine;
    public NavMeshAgent Agent {  get; private set; }
    public Animator Animator { get; private set; }

    public float AttackRange;
    public int Exp;
    public int id;
    public string MonsterName {  get; private set; }
    public string Description {  get; private set; }
    public Stat Health {  get; private set; }
    public int Damage {  get; private set; }
    public float MoveSpeed {  get; private set; }
    
    private bool isDead = false;

    public virtual void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();

    }

    public virtual void Start()
    {
        InitMonsterData();
        Cursor.lockState = CursorLockMode.Locked;
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
            Debug.Log(Damage);
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
    }

    public void MonsterDie()
    {
        if (isDead) return;

        isDead = true;
        Animator.SetTrigger("Die");
        
        Destroy(gameObject, 1f);
        DropItems();

    }

    private void DropItems()
    {
        ItemDrop.Instance.DropMeat(transform.position);
        ItemDrop.Instance.DropExp(transform.position, Exp);
    }

}
