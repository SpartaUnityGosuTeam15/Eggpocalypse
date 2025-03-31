using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //public MonsterDataLoader monsterDataLoader; // JSON에서 로드된 데이터
    public Transform[] spawnPoints;
    public float waveDuration = 300f;   // 시간 : 5분
    public float spawnInterval = 0.5f;  // 스폰 주기
    public int initialSpawnCount = 3;   // 한 번에 소환할 몬스터 수
    public int spawnIncreaseRate = 1;   // 20초마다 스폰 수 증가

    private float elapsedTime = 0f;
    private Dictionary<int, MonsterData> monsterDict;

    private void Start()
    {
        DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;

        if (monsterDict == null || monsterDict.Count == 0)
        {
            Debug.LogError("몬스터 데이터가 없습니다.");
            return;
        }

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int currentSpawnCount = initialSpawnCount;
        float spawnCountIncreaseTimer = 0f;

        while (elapsedTime < waveDuration)
        {
            for (int i = 0; i < currentSpawnCount; i++)
            {
                SpawnMonster();
            }

            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
            spawnCountIncreaseTimer += spawnInterval;

            // 20초마다 스폰 수 증가
            if (spawnCountIncreaseTimer >= 20f)
            {
                currentSpawnCount += spawnIncreaseRate;
                spawnCountIncreaseTimer = 0f;
            }
        }
    }

    void SpawnMonster()
    {
        int randomId = RandomMonsterId();
        MonsterData monsterData = monsterDict[randomId];

        GameObject monsterPrefab = Resources.Load<GameObject>($"Prefabs/Monsters/{monsterData.id}");
        if (monsterPrefab == null)
        {
            Debug.LogWarning($"프리팹을 찾을 수 없음: 이름 {monsterData.name}, id {monsterData.id}");
            return;
        }

        // 스폰 위치는 랜덤
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject monsterObj = Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
        Monster monster = monsterObj.GetComponent<Monster>();
        if (monster != null)
        {
            monster.id = randomId;
        }
    }

    int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
