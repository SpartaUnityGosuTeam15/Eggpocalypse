using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuadTreeNode<T> where T : HasPosition
{
    private const int Capacity = 4;
    private Rect _bounds;
    private List<T> _objects;
    private bool _divided = false;
    private QuadTreeNode<T> _first;
    private QuadTreeNode<T> _second;
    private QuadTreeNode<T> _third;
    private QuadTreeNode<T> _fourth;

    public QuadTreeNode(Rect bounds)
    {
        _bounds = bounds;
        _objects = new();
    }

    public bool Insert(T obj)
    {
        if(!_bounds.Contains(obj.Position)) return false;

        if (_objects.Count < Capacity)
        {
            _objects.Add(obj);
            return true;
        }
        else
        {
            if (!_divided) SubDivide();

            if (_first.Insert(obj)) return true;
            if (_second.Insert(obj)) return true;
            if (_third.Insert(obj)) return true;
            if (_fourth.Insert(obj)) return true;
        }

        return false;
    }

    private void SubDivide()
    {
        float x = _bounds.x;
        float y = _bounds.y;
        float w = _bounds.width / 2f;
        float h = _bounds.height / 2f;

        Rect firstRect = new Rect(x + w, y, w, h);
        Rect secondRect = new Rect(x, y, w, h);
        Rect thirdRect = new Rect(x, y + h, w, h);
        Rect fourthRect = new Rect(x + w, y + h, w, h);

        _first = new QuadTreeNode<T>(firstRect);
        _second = new QuadTreeNode<T>(secondRect);
        _third = new QuadTreeNode<T>(thirdRect);
        _fourth = new QuadTreeNode<T>(fourthRect);
        _divided = true;
    }

    public void CollectObjectsInCircle(Vector2 center, float radius, List<T> found)
    {
        if(!RectIntersectsCircle(_bounds, center, radius)) return;
        if(RectFullyInsideCircle(_bounds, center, radius))
        {
            GetAllObjects(found);
            return;
        }

        foreach(var obj in _objects)
        {
            if((obj.Position - center).sqrMagnitude <= radius * radius) found.Add(obj);
        }

        if (_divided)
        {
            _first.CollectObjectsInCircle(center, radius, found);
            _second.CollectObjectsInCircle(center, radius, found);
            _third.CollectObjectsInCircle(center, radius, found);
            _fourth.CollectObjectsInCircle(center, radius, found);
        }
    }

    public void BestFirstSearch(Vector2 point, ref T best, ref float bestDistSqr)
    {
        float distToNodeSqr = Util.SqrDistancePointToRect(point, _bounds);
        if (distToNodeSqr > bestDistSqr) return;

        foreach (T obj in _objects)
        {
            float distance = (obj.Position - point).sqrMagnitude;
            if (distance < bestDistSqr)
            {
                bestDistSqr = distance;
                best = obj;
            }
        }

        if (_divided)
        {
            List<QuadTreeNode<T>> children = new List<QuadTreeNode<T>> { _first, _second, _third, _fourth };
            var sortedChildren = children.OrderBy(child => Util.SqrDistancePointToRect(point, child._bounds));
            foreach (var child in sortedChildren)
            {
                child.BestFirstSearch(point, ref best, ref bestDistSqr);
            }
        }
    }

    public void GetAllObjects(List<T> result)
    {
        result.AddRange(_objects);
        if (_divided)
        {
            _first.GetAllObjects(result);
            _second.GetAllObjects(result);
            _third.GetAllObjects(result);
            _fourth.GetAllObjects(result);
        }
    }

    public static bool RectIntersectsCircle(Rect rect, Vector2 center, float radius)
    {//Rect가 일부라도 원과 겹치면 true
        float dx = Mathf.Max(rect.xMin - center.x, 0, center.x - rect.xMax);
        float dy = Mathf.Max(rect.yMin - center.y, 0, center.y - rect.yMax);
        return (dx * dx + dy * dy) <= (radius * radius);
    }

    public static bool RectFullyInsideCircle(Rect rect, Vector2 center, float radius)
    {//Rect가 원 내부에 완전히 포함되면 true
        Vector2[] corners = new Vector2[]
        {
            new Vector2(rect.xMin, rect.yMin),
            new Vector2(rect.xMin, rect.yMax),
            new Vector2(rect.xMax, rect.yMin),
            new Vector2(rect.xMax, rect.yMax)
        };

        return corners.All(corner => (corner - center).sqrMagnitude <= radius * radius);
    }
}
