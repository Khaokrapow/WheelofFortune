using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InfoManager : MonoBehaviour
{

    public TMP_Text playRoundText;
    public TMP_Text playerwinRoundText;
    public TMP_Text wungwinText;
    public TMP_Text nakonwinText;
    public TMP_Text danwinText;
    public TMP_Text pawinText;

    private int playRound;
    private int playerwinRound;
    private int wungwin;
    private int nakonwin;
    private int danwin;
    private int pawin;
    // Start is called before the first frame update
    void Start()
    {
        loadData();
        ShowData();
    }

    public void loadData() 
    {
        playRound = PlayerPrefs.GetInt("playRound", 0);
        playerwinRound = PlayerPrefs.GetInt("winRound", 0);
        wungwin = PlayerPrefs.GetInt("wungWinRound", 0);
        nakonwin = PlayerPrefs.GetInt("nakonWinRound", 0);
        danwin = PlayerPrefs.GetInt("danWinRound", 0);
        pawin = PlayerPrefs.GetInt("paWinRound", 0);
    }
    public void ShowData()
    {

        playRoundText.text = "�ӹǹ�ͺ������ : " + playRound +" ����";
        playerwinRoundText.text = "�ӹǹ�ͺ��誹�  : " + playerwinRound + " ����";
        wungwinText.text = "��ҹ��ҹ�ѧ�Ҵ��������Ӥ� : " + wungwin + " ����";
        nakonwinText.text = "��ҹ��ҹ����ʹ����  : " + nakonwin + " ����";
        danwinText.text = "��ҹ��ҹᴹ�Ծҡ�� : " + danwin + " ����";
        pawinText.text = "��ҹ��ҹ���ǧ�� : " + pawin + " ����";

    }

}
