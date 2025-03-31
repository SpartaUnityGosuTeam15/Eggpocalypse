using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : ObjectSpawner
{
    private Transform player;
    private Dictionary<int, MonsterData> monsterDict;
    private List<SpawnData> spawnDict;
    private HashSet<int> spawnedWaves = new();

    protected override void Awake()
    {
        base.Awake();
        TryFindPlayer(); //�÷��̾� ã�� �� ��ȯ �� ����

        spawnDict = DataManager.Instance.spawnDict;
    }

    private void TryFindPlayer()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null) player = pc.transform;
        else
            Debug.LogWarning("PlayerController�� ã�� ���߽��ϴ�.");
    }

    protected override void LoadPrefabs()
    {
        DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;

        if (monsterDict == null)
        {
            Debug.LogError("���� �����Ͱ� �����ϴ�.");
            return;
        }

        foreach (var pair in monsterDict)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Monsters/{pair.Key}");
            if (prefab != null)
                prefabDict[pair.Key] = prefab;
        }
    }

    protected override void SpawnObject()
    {
        if (player == null || spawnDict == null) return;

        foreach (var data in spawnDict)
        {
            if (spawnedWaves.Contains(data.wave)) continue;

            if (elapsedTime >= data.startTime && elapsedTime < data.endTime)
            {
                if (!prefabDict.TryGetValue(data.monsterId, out GameObject prefab))
                {
                    Debug.LogWarning($"[��������] ������ ����: {data.monsterId}");
                    return;
                }

                for (int i = 0; i < data.count; i++)
                    SpawnPosition(prefab);

                spawnedWaves.Add(data.wave);
                Debug.Log($"[�����Ϸ�] Wave {data.wave}: ID {data.monsterId} x {data.count}");
                return;
            }
        }
    }
    private void SpawnPosition(GameObject prefab)
    {
        const int maxAttempts = 10; // ���� ���� ������
        float minDistance = 3f;
        float areaSize = 15f;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 offset = new Vector3(Random.Range(-areaSize, areaSize), 0, Random.Range(-areaSize, areaSize));
            Vector3 pos = player.position + offset;

            if (Vector3.Distance(player.position, pos) < minDistance)
                continue;

            if (Physics.Raycast(pos + Vector3.up * 1f, Vector3.down, out RaycastHit hit, 20f, LayerMask.GetMask("Ground")))
            {
                Poolable pooled = PoolManager.Instance.Get(prefab);
                pooled.transform.position = hit.point;
                pooled.transform.rotation = Quaternion.identity;
                return;
            }
        }

        Debug.LogWarning("���� ��ġ�� ã�� ���߽��ϴ�.");
    }

}
