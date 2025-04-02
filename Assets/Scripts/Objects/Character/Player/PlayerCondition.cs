using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private Player _player;

    public Stat Health = new Stat(100, 100);
    public int attack = 10;
    public int level = 0;
    public Stat Exp = new Stat(0, 10);
    public int meat = 0;

    [SerializeField] private StatEventChannel OnHealthStatChanged;
    [SerializeField] private StatEventChannel OnExpStatChanged;

    [SerializeField] private IntEventChannel OnLevelChanged;
    [SerializeField] private IntEventChannel OnMeatChanged;

    private void Awake()
    {
        _player = GetComponent<Player>();

        attack += SaveManager.Instance.saveData.GetAttack();
        int newHealth = (int)(Health.MaxValue) + SaveManager.Instance.saveData.GetHealth();
        Health = new Stat(newHealth, newHealth);
        level = 0;
    }

    private void Start()
    {
        InvokeRepeating(nameof(RegenHealth), 1, 1);
    }

    void RegenHealth()
    {
        Heal(1);
    }

    public void TakeDamage(int damage)
    {
        Health.Subtract(damage);
        OnHealthStatChanged?.RaiseEvent(Health);

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
        OnExpStatChanged?.RaiseEvent(Exp);
    }

    public void LevelUp()
    {
        level++;
        OnLevelChanged?.RaiseEvent(level);
        //스킬 선택 메서드
        UIManager.Instance.ShowUI<UI_SelectSkill>().Init(_player.attackSkills, _player.statSkills);
        //일시정지
        Time.timeScale = 0f;
    }

    public void GainMeat(int amount)
    {
        meat += amount;
        OnMeatChanged?.RaiseEvent(meat);
    }

    public bool UseMeat(int amount)
    {
        if (meat < amount) return false;
        meat -= amount;
        OnMeatChanged?.RaiseEvent(meat);

        return true;
    }

    void Dead()
    {
        Debug.Log("죽었다!");
        UIManager.Instance.ShowUI<GameOver>();
    }
}
