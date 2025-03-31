
using UnityEngine;


public class ItemDrop : Singleton<ItemDrop>
{
    private GameObject meatPrefab;
    private GameObject expPrefab;

    protected override void Awake()
    {
        meatPrefab = Resources.Load<GameObject>("Prefabs/MonsterDrop/Meat");
        expPrefab = Resources.Load<GameObject>("Prefabs/MonsterDrop/Exp");
    }

    public void DropMeat(Vector3 position)
    {
        Poolable meat = PoolManager.Instance.Get(meatPrefab);
        meat.transform.position = position;
        meat.gameObject.SetActive(true);
        meat.GetComponent<Item>().GainItem = () => PoolManager.Instance.Release(meat);
    }

    public void DropExp(Vector3 position, int expAmount)
    {
        for (int i = 0; i < expAmount / 10; i++)
        {
            Poolable exp = PoolManager.Instance.Get(expPrefab);

            Vector3 randomOffest = new Vector3(Random.Range(-1.5f, 1.5f), .3f, Random.Range(-1.5f, 1.5f));
            exp.transform.position = position + randomOffest;
            exp.gameObject.SetActive(true);

            exp.GetComponent<Item>().GainItem = () => PoolManager.Instance.Release(exp);
        }
    }

}
