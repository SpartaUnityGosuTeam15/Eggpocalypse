using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected float totalDuration = 900f; // �� ���� �ð� (15��)
    [SerializeField] protected float baseSpawnInterval = 1f; // �⺻ ���� ����

    protected float elapsedTime = 0f; // ��� �ð�
    protected Dictionary<int, GameObject> prefabDict = new();

    protected virtual void Awake()
    {
        LoadPrefabs();
    }

    protected virtual void Start()
    {
        if (prefabDict.Count == 0)
        {
            Debug.LogError("�������� �ε����� �ʾҽ��ϴ�.");
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    protected abstract void LoadPrefabs();
    protected abstract void SpawnObject();

    protected virtual IEnumerator SpawnRoutine()
    {
        while (elapsedTime < totalDuration)
        {
            SpawnObject();
            yield return new WaitForSeconds(baseSpawnInterval);
            elapsedTime += baseSpawnInterval;
        }
    }
}

