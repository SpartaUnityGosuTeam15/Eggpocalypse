using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    private MonsterStateMachine stateMachine;
    public NavMeshAgent agent;
    public float ChaseRange;
    public float AttackRange;
    public int Id {  get; private set; }
    public string MonsterName {  get; private set; }
    public string Description {  get; private set; }
    public float Health {  get; private set; }
    public int Damage {  get; private set; }
    public float MoveSpeed {  get; private set; }


    public virtual void Awake()
    {
        stateMachine = new MonsterStateMachine(this);
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InitMonsterData();
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.ChasingState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) TakeDamage(Damage);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage.ToString());
    }

    private void InitMonsterData()
    {
        
    }
}
