using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    //public bool UseGold(int amount)
    //{
    //    if (amount > SaveManager.Instance.saveData.gold) return false;

    //    SaveManager.Instance.saveData.gold -= amount;

    //    return true;
    //}

    public void LoadScene(string sceneName)
    {
        UIManager.Instance.Clear();
        PoolManager.Instance.Clear();

        SceneManager.LoadScene(sceneName);
    }
}
