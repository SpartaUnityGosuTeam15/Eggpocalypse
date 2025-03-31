using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float waveDuration = 300f;   // 시간 : 5분
    public float spawnInterval = 0.5f;  // 스폰 주기
    public int initialSpawnCount = 3;   // 한 번에 소환할 몬스터 수
    public int spawnIncreaseRate = 1;   // 20초마다 스폰 수 증가

    private float elapsedTime = 0f;
    private Dictionary<int, MonsterData> monsterDict;
    private Dictionary<int, GameObject> monsterPrefabs = new(); // 미리 로드된 프리팹 저장

    private void Awake()
    {
        // 데이터 로딩
        DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;

        if (monsterDict == null || monsterDict.Count == 0)
        {
            Debug.LogError("몬스터 데이터가 없습니다.");
            return;
        }

        // 프리팹 미리 로드
        foreach (var pair in monsterDict)
        {
            int id = pair.Key;
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Monsters/{id}");

            if (prefab == null)
            {
                Debug.LogWarning($"프리팹 로딩 실패: id {id}, name {pair.Value.name}");
                continue;
            }

            monsterPrefabs[id] = prefab;
        }
    }

    private void Start()
    {
        if (monsterPrefabs.Count == 0)
        {
            Debug.LogError("프리팹이 하나도 로딩되지 않았습니다.");
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
        if (!monsterPrefabs.TryGetValue(randomId, out GameObject prefab))
        {
            Debug.LogWarning($"프리팹이 미리 로드되어 있지 않음: id {randomId}");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Poolable pooled = PoolManager.Instance.Get(prefab);
        pooled.transform.position = spawnPoint.position;
        pooled.transform.rotation = spawnPoint.rotation;
    }

    int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
