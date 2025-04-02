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
        TryFindPlayer(); // ���� �ٲܿ���
        //DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;
    }

    protected override void LoadPrefabs()
    {
        for (int id = 1; id <= 4; id++)
        {
            var prefab = Resources.Load<GameObject>($"Prefabs/Monsters/{id}");
            if (prefab != null) prefabDict[id] = prefab;
        }

        // ���� ������ �����
        //var boss = Resources.Load<GameObject>("Monsters/�������;��̵�");
        //if (boss != null) prefabDict[�������;��̵�] = boss;
    }

    private void TryFindPlayer()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null) player = pc.transform;
        else Debug.LogWarning("PlayerController�� ã�� ���߽��ϴ�.");
    }

    //public void SetPlayer(Transform target)
    //{
    //    player = target;
    //}

    protected override void SpawnObject()
    {
        if (player == null) return;

        float minute = Mathf.Floor(elapsedTime / 60f);

        // �׻� 1�ʸ��� 1,2���� �� ���� 3���� ����
        SpawnMultipleRandom(3, new int[] { 1, 2 });

        // 25% Ȯ��, 2�ʸ��� 3���� �߰� ����
        if (Mathf.Approximately(elapsedTime % 2f, 0f) && Random.value < 0.25f)
            SpawnMultipleRandom(3, new int[] { 1, 2 });

        // 10% Ȯ��, 7�ʸ���, 4�� ����(1������ 6��������ü) ����
        if (Mathf.Approximately(elapsedTime % 7f, 0f) && Random.value < 0.1f)
            SpawnMultiple(3, 1);

        // 10�ʸ��� ���ӵ���� ����
        if (Mathf.Approximately(elapsedTime % 10f, 0f))
            SpawnSingle(3);

        // 5�и��� ���� ���� 
        //if (Mathf.Approximately(elapsedTime % 300f, 0f) && prefabDict.ContainsKey(�������;��̵�))
        //  SpawnSingle(�������;��̵�);

        // ���� ��ȭ (1�и��� ���ݷ�, ü�� 10% ����)
        ApplyMonsterScaling(minute);
    }

    private void SpawnMultipleRandom(int count, int[] ids) //���� ���� �������� ��ȯ �� ��
    {
        for (int i = 0; i < count; i++)
        {
            int id = ids[Random.Range(0, ids.Length)];
            SpawnSingle(id);
        }
    }

    private void SpawnMultiple(int count, int id) //1�� ���� �������� ��ȯ �� ��
    {
        for (int i = 0; i < count; i++)
            SpawnSingle(id);
    }

    private void SpawnSingle(int id) //1���� ��ȯ �� ��
    {
        if (!prefabDict.ContainsKey(id)) return;

        const int maxAttempts = 10;
        float minDistance = 3f;
        float areaSize = 15f;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 offset = new Vector3(Random.Range(-areaSize, areaSize), 0, Random.Range(-areaSize, areaSize));
            Vector3 pos = player.position + offset;

            if (Vector3.Distance(player.position, pos) < minDistance)
                continue;

            if (Physics.Raycast(pos + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f, LayerMask.GetMask("Ground")))
            {
                var pooled = PoolManager.Instance.Get(prefabDict[id]);
                pooled.transform.position = hit.point;
                pooled.transform.rotation = Quaternion.identity;
                return;
            }
        }
    }

    private void ApplyMonsterScaling(float minute) //���� ��ȭ 
    {
        foreach (var monster in monsterDict.Values)
        {
            // 10% �� ������
            monster.attack = Mathf.FloorToInt(monster.attack * (1 + 0.1f * minute));
            monster.health = Mathf.FloorToInt(monster.health * (1 + 0.1f * minute));
        }
    }
}
