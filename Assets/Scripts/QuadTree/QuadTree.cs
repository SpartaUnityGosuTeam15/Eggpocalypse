using System.Collections.Generic;
using UnityEngine;

public class QuadTree<T> where T : HasPosition
{
    private QuadTreeNode<T> _root;

    public QuadTree(Rect bounds)
    {
        _root = new QuadTreeNode<T>(bounds);
    }

    public void Insert(T obj)
    {
        _root.Insert(obj);
    } 

    //public List<T> FindObjectsinArea(Rect range)
    //{
    //    List<T> found = new();
    //    _root.CollectObjectsInCircle(range, found);
    //    return found;
    //}

    public T FindNearestObject(Vector2 center, float radius)
    {//가장 가까운 몬스터 찾기
        T best = default(T);
        float bestDistSqr = float.MaxValue;
        _root.BestFirstSearch(center, ref best, ref bestDistSqr);
        
        if (best == null || bestDistSqr > radius * radius) return default(T);

        return best;
    }

    public List<T> FindNearestObjects(Vector2 center, float radius)
    {//일정 거리 내의 몬스터 그룹 찾기
        List<T> found = new();
        _root.CollectObjectsInCircle(center, radius, found);

        return found;
    }

    public void DrawNodes()
    {
        _root.DrawNode();
    }
}
