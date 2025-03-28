using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterDataLoader monsterDataLoader; // JSON���κ��� �ҷ��� ������
    public Transform[] spawnPoints;             // ���Ͱ� ������ ��ġ��
    public float waveDuration = 300f;           // ���̺� �ð�
    public float spawnInterval = 5f;            // ���� ���� ����
    private float elapsedTime = 0f;

    private Dictionary<int, MonsterData> monsterDict;

    private void Start()
    {
        // �ε�� ���� ������ ��������
        monsterDict = monsterDataLoader.MakeDict();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (elapsedTime < waveDuration)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }

    void SpawnMonster()
    {
        // ���� ���� ����
        int randomId = RandomMonsterId();
        MonsterData monsterData = monsterDict[randomId];

        // �������� Resources �������� �ҷ�����
        GameObject monsterPrefab = Resources.Load<GameObject>($"Monsters/{monsterData.name}");
        if (monsterPrefab == null)
        {
            Debug.LogWarning($"���� �������� ã�� �� ����: {monsterData.name}");
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

