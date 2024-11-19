using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UniversalManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image background;
    public TMP_Text nameMap;

    // Start is called before the first frame update
    void Start()
    {
        string map = SelectMapPage.selectMap;
        //Debug.Log(map);
        nameMap.text = "ขอต้อนรับสู่อาณาเขตของ " + map;
        int number = 0;

        if (map.Equals("แดนพิพากษา"))
        {
            number = 0;
        }
        else if (map.Equals("นครมอดไหม้"))
        {
            number = 1;
        }
        else if (map.Equals("วังบาดาลใต้แม่น้ำคำ"))
        {
            number = 2;
        }
        else if (map.Equals("ป่าวงกต")) { 
            number = 3;
        }
        background.sprite = backgrounds[number];

    }

    
}
