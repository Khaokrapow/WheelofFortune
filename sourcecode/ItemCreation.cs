using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemCreation : ScriptableObject
{
    public List<Item> items;

    public int itemCount()
    {
        return items.Count;
    }
    public Item getItem(int index)
    {
        return items[index];
    }
}
