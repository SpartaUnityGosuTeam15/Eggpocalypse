using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float waveDuration = 300f;   // �ð� : 5��
    public float spawnInterval = 0.5f;  // ���� �ֱ�
    public int initialSpawnCount = 3;   // �� ���� ��ȯ�� ���� ��
    public int spawnIncreaseRate = 1;   // 20�ʸ��� ���� �� ����

    private float elapsedTime = 0f;
    private Dictionary<int, MonsterData> monsterDict;
    private Dictionary<int, GameObject> monsterPrefabs = new(); // �̸� �ε�� ������ ����

    private void Awake()
    {
        // ������ �ε�
        DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;

        if (monsterDict == null || monsterDict.Count == 0)
        {
            Debug.LogError("���� �����Ͱ� �����ϴ�.");
            return;
        }

        // ������ �̸� �ε�
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

            // 20�ʸ��� ���� �� ����
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
            Debug.LogWarning($"�������� �̸� �ε�Ǿ� ���� ����: id {randomId}");
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
