using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public Stat Health = new Stat(100, 100);
    public int attack = 10;
    public int level = 1;
    public Stat Exp = new Stat(0, 10);
    public int meat = 0;

    [SerializeField] private StatEventChannel OnHealthStatChanged;
    [SerializeField] private StatEventChannel OnExpStatChanged;

    [SerializeField] private IntEventChannel OnLevelChanged;

    private void Start()
    {
        InvokeRepeating(nameof(Test), 0, 0.5f);
    }

    void Test()
    {
        TakeDamage(1);
        GainExp(1);
    }

    public void TakeDamage(int damage)
    {
        Health.Subtract(damage);
        OnHealthStatChanged.RaiseEvent(Health);

        if(Health.CurValue <= 0)
        {
            Dead();
        }
    }

    public void Heal(int amount)
    {
        Health.Add(amount);
    }

    public void GainExp(int amount)
    {
        if(Exp.CurValue + amount >= Exp.MaxValue)
        {
            Exp.Set(Exp.CurValue + amount - Exp.MaxValue);
            LevelUp();
        }
        else
        {
            Exp.Add(amount);
        }
        OnExpStatChanged.RaiseEvent(Exp);
    }

    public void LevelUp()
    {
        level++;
        OnLevelChanged.RaiseEvent(level);
        //��ų ���� �޼���
        //�Ͻ� ����
    }

    void Dead()
    {
        Debug.Log("�׾���!");
    }
}
