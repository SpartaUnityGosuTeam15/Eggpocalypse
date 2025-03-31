using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode<T> where T : HasPosition
{
    private const int Capacity = 4;
    public Rect Bounds;
    private List<T> objects;
    private bool divided = false;
    private QuadTreeNode<T> first;
    private QuadTreeNode<T> second;
    private QuadTreeNode<T> third;
    private QuadTreeNode<T> fourth;

    public QuadTreeNode(Rect bounds)
    {
        Bounds = bounds;
        objects = new();
    }

    public bool Insert(T obj)
    {
        if(!Bounds.Contains(obj.Position)) return false;

        if (objects.Count < Capacity)
        {
            objects.Add(obj);
            return true;
        }
        else
        {
            if (!divided) SubDivide();

            if (first.Insert(obj)) return true;
            if (second.Insert(obj)) return true;
            if (third.Insert(obj)) return true;
            if (fourth.Insert(obj)) return true;
        }

        return false;
    }

    private void SubDivide()
    {
        float x = Bounds.x;
        float y = Bounds.y;
        float w = Bounds.width / 2f;
        float h = Bounds.height / 2f;

        Rect firstRect = new Rect(x + w, y, w, h);
        Rect secondRect = new Rect(x, y, w, h);
        Rect thirdRect = new Rect(x, y + h, w, h);
        Rect fourthRect = new Rect(x + w, y + h, w, h);

        first = new QuadTreeNode<T>(firstRect);
        second = new QuadTreeNode<T>(secondRect);
        third = new QuadTreeNode<T>(thirdRect);
        fourth = new QuadTreeNode<T>(fourthRect);
        divided = true;
    }

    public void Query(Rect range, List<T> found)
    {
        if(!Bounds.Overlaps(range)) return;

        foreach(var obj in objects)
        {
            if(range.Contains(obj.Position)) found.Add(obj);
        }

        if (divided)
        {
            first.Query(range, found);
            second.Query(range, found);
            third.Query(range, found);
            fourth.Query(range, found);
        }
    }

    public void GetAll(List<T> result)
    {
        result.AddRange(objects);
        if (divided)
        {
            first.GetAll(result);
            second.GetAll(result);
            third.GetAll(result);
            fourth.GetAll(result);
        }
    }
}
