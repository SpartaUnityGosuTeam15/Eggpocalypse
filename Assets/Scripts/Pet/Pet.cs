using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour, IDamageable
{
    protected PetStateMachine stateMachine;
    public NavMeshAgent Agent { get; private set; }
    public Collider attackCollider;
    public GameObject biteArea; // 물어뜯는 범위  
    public Animator Animator { get; private set; }

    public float AttackRange = 2f;
    public float PlayerDetectionRange = 10f;
    public int Id { get; private set; }
    public string Description { get; private set; }
    public int Damage { get; private set; }
    public int health{ get; private set; }
    public int skillId { get; private set; }
    public float MoveSpeed { get; private set; }
    public Player Target { get; private set; }
    public bool isAttacking { get; set; } // 공격 상태 여부

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        attackCollider = GetComponent<Collider>();
    }

    public virtual void Start()
    {
        //InitDragonData();
        stateMachine = new PetStateMachine(this);
        stateMachine.Target = GameManager.Instance.player;
        stateMachine.ChangeState(stateMachine.ChasingState);
        
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public void InitializeStats(int eggHealth, int eggAttack, int eggSkillId)
    {
        health = eggHealth;
        Damage = eggAttack;
        skillId = eggSkillId;

        Debug.Log($"Pet 생성됨 - HP: {health}, 공격력: {Damage}, 스킬 ID: {skillId}");
    }
    // private void InitDragonData()
    // {
    //     BuildingData buildingData  = DataManager.Instance.buildDict.TryGetValue(Id, out BuildingData data) ? data : null;

    //     if (buildingData != null && buildingData.type == BuildingType.Dragon)
    //     {

    //         Damage = buildingData.attack;
    //         Agent.speed = MoveSpeed;
    //     }
    // }
    public void Feed()
    {
         //체력회복
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Monster") && biteArea != null && other == biteArea)
        {
            IDamageable monster = other.GetComponent<IDamageable>();
            if (monster != null)
            {
                monster.TakeDamage(Damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
