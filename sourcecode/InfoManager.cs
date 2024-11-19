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

        playRoundText.text = "จำนวนรอบที่เล่น : " + playRound +" ครั้ง";
        playerwinRoundText.text = "จำนวนรอบที่ชนะ  : " + playerwinRound + " ครั้ง";
        wungwinText.text = "ผ่านด่านวังบาดาลใต้แม่น้ำคำ : " + wungwin + " ครั้ง";
        nakonwinText.text = "ผ่านด่านนครมอดไหม้  : " + nakonwin + " ครั้ง";
        danwinText.text = "ผ่านด่านแดนพิพากษา : " + danwin + " ครั้ง";
        pawinText.text = "ผ่านด่านป่าวงกต : " + pawin + " ครั้ง";

    }

}
