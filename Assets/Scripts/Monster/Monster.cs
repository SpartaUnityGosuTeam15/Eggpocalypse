using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(int damage);
}
public class Monster : MonoBehaviour, IDamagable
{
    private MonsterStateMachine stateMachine;
    public float ChaseRange;
    public float AttackRange;

    public int Id {  get; private set; }
    public string MonsterName {  get; private set; }
    public string Description {  get; private set; }
    public float Health {  get; private set; }
    public int Damage {  get; private set; }
    public float MoveSpeed {  get; private set; }


    private void Awake()
    {
        stateMachine = new MonsterStateMachine(this);
    }

    private void Start()
    {
        InitMonsterData();
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
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
