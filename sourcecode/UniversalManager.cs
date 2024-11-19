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
        nameMap.text = "�͵�͹�Ѻ����ҳ�ࢵ�ͧ " + map;
        int number = 0;

        if (map.Equals("ᴹ�Ծҡ��"))
        {
            number = 0;
        }
        else if (map.Equals("����ʹ����"))
        {
            number = 1;
        }
        else if (map.Equals("�ѧ�Ҵ��������Ӥ�"))
        {
            number = 2;
        }
        else if (map.Equals("���ǧ��")) { 
            number = 3;
        }
        background.sprite = backgrounds[number];

    }

    
}
