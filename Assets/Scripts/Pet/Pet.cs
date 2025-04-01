using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour, IDamageable
{
    protected PetStateMachine stateMachine;
    public NavMeshAgent Agent { get; private set; }
    public Collider attackCollider;

    public Animator Animator { get; private set; }

    public float AttackRange = 2f; // 공격 범위
    public float PlayerDetectionRange = 10f; // 플레이어 탐지 범위 (기즈모용)
    public int Id { get; private set; }
    public string MonsterName { get; private set; }
    public string Description { get; private set; }
    //public float Health { get; private set; }
    public int Damage { get; private set; }
    public float MoveSpeed { get; private set; }

    public Player Target { get; private set; }

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        attackCollider = GetComponent<Collider>();
    }

    public virtual void Start()
    {
        InitDragonData();

        // stateMachine 초기화 및 타겟 설정
        stateMachine = new PetStateMachine(this);
        stateMachine.Target = GameManager.Instance.player;

        // 추적 상태로 변경
        stateMachine.ChangeState(stateMachine.ChasingState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Player"))
        //     TakeDamage(Damage);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Received {damage} damage.");
    }

    private void InitDragonData()
    {
        BuildingData data  = GameManager.Instance.GetBuildingData(Id);
        if (data != null&&data.type == BuildingType.Dragon)
        {
            Damage = data.attack;
            Agent.speed = MoveSpeed;
        }
    }

        private void OnDrawGizmosSelected()
    {
        //  stateMachine이 아직 생성되지 않은 경우 기본값 사용
        float detectionRange = PlayerDetectionRange;

        // 빨간색으로 플레이어 감지 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 파란색으로 공격 범위 표시
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

}
