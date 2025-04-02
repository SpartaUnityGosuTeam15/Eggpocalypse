using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public int gold;
    [SerializeField] private IntEventChannel OnGoldChanged;

    protected override void Awake()
    {
        base.Awake();

        OnGoldChanged = Resources.Load<IntEventChannel>($"ScriptableObjects/{nameof(OnGoldChanged)}");
    }

    public void GainGold(int amount)
    {
        gold += amount;
        OnGoldChanged.RaiseEvent(gold);
    }

    public void LoadScene(string sceneName)
    {
        UIManager.Instance.Clear();
        PoolManager.Instance.Clear();

        if(SceneManager.GetActiveScene().name == "Stage")
        {
            SaveManager.Instance.saveData.gold = gold;
            gold = 0;
        }

        SceneManager.LoadScene(sceneName);
    }
}
