using UnityEngine;
using UnityEngine.AI;

public class PetChasingState : IState
{
    private PetStateMachine stateMachine;
    private NavMeshAgent agent;
    
    

    public PetChasingState(PetStateMachine machine)
    {
        stateMachine = machine;
        agent = machine.Pet.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        // Debug.Log($"{stateMachine.Pet.name}플레이어 추적");
        agent.isStopped = false; // 추적 시 이동
    }

    public void Update()
{
    if (stateMachine.Target == null) return;

    float distanceToPlayer = Vector3.Distance(stateMachine.Pet.transform.position, stateMachine.Target.transform.position);

    // 플레이어가 20m 안에 있으면 Attack 상태로 변경
    if (distanceToPlayer <= stateMachine.Pet.PlayerDetectionRange)
    {
        stateMachine.ChangeState(stateMachine.AttackState);
        return;
    }

    // 플레이어를 계속 추적
    agent.SetDestination(stateMachine.Target.transform.position);
}



    public void Exit()
    {
        // Debug.Log($"{stateMachine.Pet.name}추적 멈춤");
    }

    // private Monster FindClosestMonster()
    // {
    //     float minDistance = float.MaxValue;
    //     Monster closestMonster = null;

    //     foreach (var monster in GameObject.FindObjectsOfType<Monster>())
    //     {
    //         if (monster == stateMachine.Pet) continue; // 자기 자신 제외

    //         float distance = Vector3.Distance(stateMachine.Pet.transform.position, monster.transform.position);
    //         if (distance < minDistance && distance <= attackRange)
    //         {
    //             minDistance = distance;
    //             closestMonster = monster;
    //         }
    //     }

    //     return closestMonster;
    // }
}
