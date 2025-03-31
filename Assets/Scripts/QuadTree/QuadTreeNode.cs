using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class QuadTreeNode<T> where T : HasPosition
{
    private const int Capacity = 4;
    private Rect _bounds;
    private List<T> _objects;

    private bool _divided = false;
    private QuadTreeNode<T> northeast;
    private QuadTreeNode<T> northwest;
    private QuadTreeNode<T> southeast;
    private QuadTreeNode<T> southwest;

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

            if (northeast.Insert(obj)) return true;
            if (northwest.Insert(obj)) return true;
            if (southeast.Insert(obj)) return true;
            if (southwest.Insert(obj)) return true;
        }

        return false;
    }

    private void SubDivide()
    {
        float x = _bounds.xMin;
        float y = _bounds.yMin;
        float w = _bounds.width / 2f;
        float h = _bounds.height / 2f;

        Rect ne = new Rect(x + w, y + h, w, h);
        Rect nw = new Rect(x, y + h, w, h);
        Rect se = new Rect(x + w, y, w, h);
        Rect sw = new Rect(x, y, w, h);

        Debug.Log(ne);
        Debug.Log(nw);
        Debug.Log(se);
        Debug.Log(sw);

        northeast = new QuadTreeNode<T>(ne);
        northwest = new QuadTreeNode<T>(nw);
        southeast = new QuadTreeNode<T>(se);
        southwest = new QuadTreeNode<T>(sw);

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
            northeast.CollectObjectsInCircle(center, radius, found);
            northwest.CollectObjectsInCircle(center, radius, found);
            southeast.CollectObjectsInCircle(center, radius, found);
            southwest.CollectObjectsInCircle(center, radius, found);
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
            List<QuadTreeNode<T>> children = new List<QuadTreeNode<T>> { northeast, northwest, southeast, southwest };
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
            northeast.GetAllObjects(result);
            northwest.GetAllObjects(result);
            southeast.GetAllObjects(result);
            southwest.GetAllObjects(result);
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

    public void DrawNode()
    {
        Vector3 center = new Vector3(_bounds.center.x, 0, _bounds.center.y);
        Vector3 size = new Vector3(_bounds.width, 0, _bounds.height);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);

        if (_divided)
        {
            northeast.DrawNode();
            northwest.DrawNode();
            southeast.DrawNode();
            southwest.DrawNode();
        }
    }
}
