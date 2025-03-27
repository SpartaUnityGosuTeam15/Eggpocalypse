using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Move(movementDirection);
        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position);
        return dir;
    }

    private void Move(Vector3 direction)
    {
        float moveSpeed = stateMachine.MoveSpeed;
        Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
        stateMachine.Monster.transform.position += moveVector;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTrans = stateMachine.Monster.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTrans.rotation = Quaternion.Slerp(playerTrans.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected bool IsInChasingRange()
    {
        if (stateMachine.Monster.Health <= 0) return false;
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Monster.ChaseRange;
    }

}
