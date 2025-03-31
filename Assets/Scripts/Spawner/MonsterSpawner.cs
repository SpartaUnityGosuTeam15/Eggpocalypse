using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float waveDuration = 300f;   // �ð� : 5��
    public float spawnInterval = 0.5f;  // ���� �ֱ�
    public int initialSpawnCount = 3;   // �� ���� ��ȯ�� ���� ��
    public int spawnIncreaseRate = 1;   // 20�ʸ��� ���� �� ����

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
            Debug.LogError("���� �����Ͱ� �����ϴ�.");
            return;
        }

        // ���� ������ �̸� �ε�
        foreach (var pair in monsterDict)
        {
            int id = pair.Key;
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Monsters/{id}");

            if (prefab == null)
            {
                Debug.LogWarning($"������ �ε� ����: id {id}, name {pair.Value.name}");
                continue;
            }

            monsterPrefabs[id] = prefab;
        }
    }

    private void Start()
    {
        if (monsterPrefabs.Count == 0)
        {
            Debug.LogError("�������� �ϳ��� �ε����� �ʾҽ��ϴ�.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("�÷��̾ �������� �ʾҽ��ϴ�.");
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
            Debug.LogWarning("PlayerController�� ���� �÷��̾ ���� �ڵ� �Ҵ翡 ����.");
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
                    Debug.LogWarning($"�������� �̸� �ε�Ǿ� ���� ����: id {randomId}");
                    return;
                }

                Poolable pooled = PoolManager.Instance.Get(prefab);
                pooled.transform.position = hit.point;
                pooled.transform.rotation = Quaternion.identity;
                return;
            }
        }

        Debug.LogWarning("������ �����ϴ� ��ġ�� ã�� ���� ���� ���� (10ȸ �õ� �ʰ�)");
    }

    int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
