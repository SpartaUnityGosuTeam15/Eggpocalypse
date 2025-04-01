
using UnityEngine;
using UnityEngine.UIElements;


public class ItemDrop : Singleton<ItemDrop>
{
    public GameObject meatPrefab;
    public GameObject expPrefab;
    public GameObject goldPrefab;

    protected override void Awake()
    {
        meatPrefab = Resources.Load<GameObject>("Prefabs/MonsterDrop/Meat");
        expPrefab = Resources.Load<GameObject>("Prefabs/MonsterDrop/Exp");
        goldPrefab = Resources.Load<GameObject>("Prefabs/MonsterDrop/Gold");
    }

    public void DropItem(GameObject prefab, Vector3 position, int value)
    {
        for(int i = 0; i < value; i++)
        {
            Poolable poolable = PoolManager.Instance.Get(prefab);

            Vector3 randomOffest = new Vector3(Random.Range(-1.5f, 1.5f), .3f, Random.Range(-1.5f, 1.5f));
            poolable.transform.position = position + randomOffest;
            poolable.gameObject.SetActive(true);

            poolable.GetComponent<Item>().GainItem = () => PoolManager.Instance.Release(poolable);

        }
    }

}
