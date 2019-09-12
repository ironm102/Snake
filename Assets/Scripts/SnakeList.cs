using System;
using UnityEngine;

class SnakeList
{
    public Node headNode;

    public SnakeList()
    {
        headNode = null;
    }

    public void AddToEnd(GameObject data)
    {
        if (headNode == null)
        {
            headNode = new Node(data);
        }
        else
        {
            headNode.AddToEnd(data);
        }
    }

    public void AddToStart(GameObject data)
    {
        if (headNode == null)
        {
            headNode = new Node(data);
        }
        else
        {
            Node temp = new Node(data);
            temp.next = headNode;
            headNode = temp;
        }
    }

    public Node GetLast(Node n)
    {
        Node currentNode = n;
        while (currentNode.next != null)
        {
            currentNode = currentNode.next;
        }
        return currentNode;
    }

    public void RemoveAt(int position)
    {
        if (headNode == null)
        {
            return;
        }
        Node currentNode = headNode;

        if (position == 0)
        {
            headNode = currentNode.next;
            currentNode = null;
            return;
        }

        for (int i = 0; currentNode != null && i < position - 1; i++)
        {
            currentNode = currentNode.next;
        }
        if (currentNode == null || currentNode.next == null)
        {
            return;
        }
        headNode.next = currentNode.next.next;

        currentNode.next = null;
        currentNode.next = headNode.next;
    }

    public void Remove(GameObject key)
    {
        if (headNode == null) { throw new NullReferenceException(); }
        if (headNode.data.Equals(key))
        {
            headNode = headNode.next;
            return;
        }
        Node previous = null;
        Node current = headNode;

        while (current != null && !current.data.Equals(key))
        {
            previous = current;
            current = current.next;
        }

        if (current == null) { throw new NullReferenceException(); }

        previous.next = current.next;
    }

    public int Count()
    {
        if (headNode == null)
        {
            return 0;
        }

        int count = 1;
        Node current = headNode;


        while (current.next != null)
        {
            current = current.next;
            count++;
        }
        return count;
    }
    public void Print()
    {
        if (headNode != null)
        {
            headNode.Print();
        }
    }
}
