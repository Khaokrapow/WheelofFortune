using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string nameItem ="Unknow";
    public GameObject itemObject;

    public string getNameItem() 
    { 
        return nameItem; 
    }

    public GameObject getObjectItem() 
    { 
        return itemObject; 
    }
    

}
