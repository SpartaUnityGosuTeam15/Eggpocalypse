using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    public void LoadScene(string sceneName)
    {
        UIManager.Instance.Clear();
        PoolManager.Instance.Clear();

        SceneManager.LoadScene(sceneName);
    }
    public BuildingData GetBuildingData(int id)
    {
        return DataManager.Instance.buildDict.TryGetValue(id, out BuildingData data) ? data : null;
    }
}
