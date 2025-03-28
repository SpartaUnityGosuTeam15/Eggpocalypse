using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    private MonsterStateMachine stateMachine;
    public NavMeshAgent Agent {  get; private set; }
   
    public Animator Animator { get; private set; }

    public float AttackRange;
    public int Id {  get; private set; }
    public string MonsterName {  get; private set; }
    public string Description {  get; private set; }
    public float Health {  get; private set; }
    public int Damage {  get; private set; }
    public float MoveSpeed {  get; private set; }


    public virtual void Awake()
    {

        Animator = GetComponentInChildren<Animator>();
        stateMachine = new MonsterStateMachine(this);
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InitMonsterData();
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.ChasingState);
        stateMachine.Target = GameManager.Instance.player;
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
