using UnityEngine;

public class Node
{
    public GameObject data;

    public Node next;
    public Node(GameObject i)
    {
        data = i;
        next = null;
    }
    public void Print()
    {
        Debug.Log("|" + data + "|->");
        if (next != null)
        {
            next.Print();
        }
    }
    public void AddToEnd(GameObject data)
    {
        if (next == null)
        {
            next = new Node(data);
        }
        else
        {
            next.AddToEnd(data);
        }
    }
}
public class LinkedList
{

}