public abstract class State
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class PetStateMachine : StateMachine
{
    public Pet Pet { get; }
    public PetChasingState ChasingState { get; }
    public PetAttackState AttackState { get; }

    public Player Target { get; set; }
    public GameManager gameManager;
    public Monster TargetMonster { get; set; } // 공격할 몬스터

    public PetStateMachine(Pet pet)
    {
        Pet = pet;
        ChasingState = new PetChasingState(this);
        AttackState = new PetAttackState(this);
        
    }
}
