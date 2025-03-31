using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : ObjectSpawner
{
    private Transform player;
    private Dictionary<int, MonsterData> monsterDict;

    protected override void Awake()
    {
        base.Awake();
        TryFindPlayer();
    }

    private void TryFindPlayer()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null)
            player = pc.transform;
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
        if (player == null) return;

        const int maxAttempts = 10;
        float minDistance = 3f;
        float areaSize = 15f;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-areaSize, areaSize), 0, Random.Range(-areaSize, areaSize));
            Vector3 pos = player.position + offset;

            if (Vector3.Distance(player.position, pos) < minDistance)
                continue;

            if (Physics.Raycast(pos + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f, LayerMask.GetMask("Ground")))
            {
                int id = RandomMonsterId();
                if (prefabDict.TryGetValue(id, out GameObject prefab))
                {
                    Poolable pooled = PoolManager.Instance.Get(prefab);
                    pooled.transform.position = hit.point;
                    pooled.transform.rotation = Quaternion.identity;
                }
                return;
            }
        }

        Debug.LogWarning("���� ���� ����: ���ǿ� �´� ��ġ�� ã�� ����");
    }

    private int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
