using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterDataLoader monsterDataLoader; // JSON���� �ε�� ������
    public Transform[] spawnPoints;
    public float waveDuration = 300f;   // �ð� : 5��
    public float spawnInterval = 0.5f;  // ���� �ֱ�
    public int initialSpawnCount = 3;   // �� ���� ��ȯ�� ���� ��
    public int spawnIncreaseRate = 1;   // 20�ʸ��� ���� �� ����

    private float elapsedTime = 0f;
    private Dictionary<int, MonsterData> monsterDict;

    private void Start()
    {
        monsterDict = monsterDataLoader.MakeDict();

        if (monsterDict == null || monsterDict.Count == 0)
        {
            Debug.LogError("���� �����Ͱ� �����ϴ�.");
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
        MonsterData monsterData = monsterDict[randomId];

        GameObject monsterPrefab = Resources.Load<GameObject>($"Prefabs/Monsters/{monsterData.name}");
        if (monsterPrefab == null)
        {
            Debug.LogWarning($"�������� ã�� �� ����: {monsterData.name}");
            return;
        }

        // ���� ��ġ�� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
