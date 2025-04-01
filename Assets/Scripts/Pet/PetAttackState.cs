using UnityEngine;
using UnityEngine.AI;

public class PetAttackState : IState
{
    private PetStateMachine stateMachine;
    private NavMeshAgent agent;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    public PetAttackState(PetStateMachine machine)
    {
        stateMachine = machine;
        agent = machine.Pet.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        // 공격 상태로 전환될 때의 초기화
    }

    public void Update()
    {
        if (stateMachine.Target == null) return;

        // 플레이어와의 거리 체크 (공격 범위 내에 있는지 확인)
        float distanceToPlayer = Vector3.Distance(stateMachine.Pet.transform.position, stateMachine.Target.transform.position);

        // 플레이어가 20m 밖으로 나가면 다시 추적 상태로 변경
        if (distanceToPlayer > stateMachine.Pet.PlayerDetectionRange)
        {
            stateMachine.Pet.Animator.SetBool("Attack", false);
            stateMachine.Pet.Animator.SetBool("Move", true);
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        // 플레이어가 공격 범위 내에 있고, 근처에 몬스터가 있는 경우에만 공격
        if (distanceToPlayer <= stateMachine.Pet.AttackRange)
        {
            // 근처에 몬스터가 있는지 확인
            Monster closestMonster = FindClosestMonster();
            if (closestMonster != null)
            {
                // 공격 애니메이션 실행
                stateMachine.Pet.Animator.SetBool("Move", false);
                stateMachine.Pet.Animator.SetBool("Attack", true);
                Attack(closestMonster);
                Debug.Log("몬스터 발견 공격시작");
            }
        }
    }

    private Monster FindClosestMonster()
    {
        float minDistance = float.MaxValue;
        Monster closestMonster = null;

        // "Monster" 태그를 가진 게임 오브젝트 중 가장 가까운 몬스터를 찾음
        foreach (var monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            float distance = Vector3.Distance(stateMachine.Pet.transform.position, monster.transform.position);
            if (distance < minDistance && distance <= stateMachine.Pet.AttackRange)
            {
                minDistance = distance;
                closestMonster = monster.GetComponent<Monster>();
            }
        }

        return closestMonster;
    }

    public void Exit()
    {
        // 공격 상태 종료 시
        Debug.Log($"{stateMachine.Pet.name} 공격 멈춤.");
    }

    private void Attack(Monster targetMonster)
    {
        if (targetMonster != null)
        {
            // 공격 대상 몬스터에게 데미지 적용
            targetMonster.TakeDamage(stateMachine.Pet.Damage);
        }
    }
}
