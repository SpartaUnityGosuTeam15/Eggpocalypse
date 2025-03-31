using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    //[SerializeField]
    protected float waveDuration = 300f; // ���� �ð�
    protected float spawnInterval = 5f;  // ���� ���� 

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

    IEnumerator SpawnRoutine()
    {
        while (elapsedTime < waveDuration)
        {
            SpawnObject(); // �� ���� ȣ�� / ���ο��� �ð� üũ�ؼ� �ʿ��� �ָ� ��ȯ
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }
}

