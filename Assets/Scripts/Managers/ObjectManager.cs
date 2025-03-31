using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public List<Monster> monsters = new();
    private Transform _player;

    [SerializeField] private Rect quadTreeBounds = new Rect(-10, -10, 20, 20);
    private QuadTree<Monster> monsterQuadTree;

    protected override void Awake()
    {
        isGlobal = false;

        base.Awake();
    }

    private void Start()
    {
        _player = GameManager.Instance.player.transform;   
    }

    void RebuildeTree()
    {
        monsterQuadTree = new QuadTree<Monster>(quadTreeBounds);
        foreach(Monster monster in monsters)
        {
            monsterQuadTree.Insert(monster);
        } 
    }

    public Monster GetNearestMonster(float radius)
    {
        RebuildeTree();

        Vector2 playerPos = new Vector2(_player.position.x, _player.position.z);
        return monsterQuadTree.FindNearestMonster(playerPos, radius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position;

        Vector3 center = new Vector3(position.x + quadTreeBounds.x + quadTreeBounds.width / 2,
                                     position.y,
                                     position.z + quadTreeBounds.y + quadTreeBounds.height / 2);
        Vector3 size = new Vector3(quadTreeBounds.width, 0.01f, quadTreeBounds.height); // 얇은 두께 추가

        Gizmos.DrawCube(center, size);
    }
}
