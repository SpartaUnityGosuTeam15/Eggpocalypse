using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public Stat Health = new Stat(100, 100);
    public float attack = 10f;

    public void TakeDamage(int damage)
    {
        Health.Subtract(damage);

        if(Health.CurValue <= 0)
        {
            Dead();
        }
    }

    public void Heal(int amount)
    {
        Health.Add(amount);
    }

    void Dead()
    {
        Debug.Log("ав╬З╢ы!");
    }
}
