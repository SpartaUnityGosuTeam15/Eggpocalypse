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

    public List<T> QueryRange(Rect range)
    {
        List<T> found = new();
        _root.Query(range, found);
        return found;
    }

    public T FindNearestMonster(Vector2 center, float radius)
    {
        Rect searchRect = new Rect(center.x - radius, center.y - radius, radius, radius);
        List<T> candidates  = QueryRange(searchRect);

        if (candidates.Count == 0) return null;

        T nearest = default(T);
        float minDistSqr = float.MaxValue;
        float radiusSqr = radius * radius;
        foreach (T obj in candidates)
        {
            float distSqr = (obj.Position - center).sqrMagnitude;
            if (distSqr < minDistSqr && distSqr < radiusSqr)
            {
                minDistSqr = distSqr;
                nearest = obj;
            }
        }

        return nearest;
    }

    public List<T> FindNearestMonsters(Vector2 center, float radius)
    {
        Rect searchRect = new Rect(center.x - radius, center.y - radius, radius, radius);
        List<T> candidates = QueryRange(searchRect);

        if (candidates.Count == 0) return null;

        List<T> found = new();
        float radiusSqr = radius * radius;
        foreach (T obj in candidates)
        {
            float distSqr = (obj.Position - center).sqrMagnitude;
            if (distSqr < radiusSqr)
            {
                found.Add(obj);
            }
        }

        return found;
    }
}
