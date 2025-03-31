using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float waveDuration = 300f;   // 시간 : 5분
    public float spawnInterval = 0.5f;  // 스폰 주기
    public int initialSpawnCount = 3;   // 한 번에 소환할 몬스터 수
    public int spawnIncreaseRate = 1;   // 20초마다 스폰 수 증가

    private float elapsedTime = 0f;

    private Transform player;
    private Dictionary<int, MonsterData> monsterDict;
    private Dictionary<int, GameObject> monsterPrefabs = new();

    private void Awake()
    {
        if (player == null)
            TryFindPlayer();

        DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;

        if (monsterDict == null || monsterDict.Count == 0)
        {
            Debug.LogError("몬스터 데이터가 없습니다.");
            return;
        }

        // 몬스터 프리팹 미리 로드
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

        if (player == null)
        {
            Debug.LogError("플레이어가 설정되지 않았습니다.");
            return;
        }

        StartCoroutine(SpawnWave());
    }

    private void TryFindPlayer()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null)
        {
            player = pc.transform;
        }
        else
        {
            Debug.LogWarning("PlayerController가 붙은 플레이어가 없어 자동 할당에 실패.");
        }
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

            if (spawnCountIncreaseTimer >= 20f)
            {
                currentSpawnCount += spawnIncreaseRate;
                spawnCountIncreaseTimer = 0f;
            }
        }
    }

    void SpawnMonster()
    {
        const int maxAttempts = 10;
        float minDistanceFromPlayer = 3f;
        float spawnAreaSize = 15f;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnAreaSize, spawnAreaSize),
                0,
                Random.Range(-spawnAreaSize, spawnAreaSize)
            );
            Vector3 potentialPosition = player.position + randomOffset;

            if (Vector3.Distance(player.position, potentialPosition) < minDistanceFromPlayer)
                continue;

            Vector3 rayOrigin = potentialPosition + Vector3.up * 10f;
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 20f, LayerMask.GetMask("Ground")))
            {
                int randomId = RandomMonsterId();
                if (!monsterPrefabs.TryGetValue(randomId, out GameObject prefab))
                {
                    Debug.LogWarning($"프리팹이 미리 로드되어 있지 않음: id {randomId}");
                    return;
                }

                Poolable pooled = PoolManager.Instance.Get(prefab);
                pooled.transform.position = hit.point;
                pooled.transform.rotation = Quaternion.identity;
                return;
            }
        }

        Debug.LogWarning("조건을 만족하는 위치를 찾지 못해 스폰 실패 (10회 시도 초과)");
    }

    int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
